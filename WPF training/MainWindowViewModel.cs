using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPF_training
{
    internal class MainWindowViewModel : PropertyChangedImplemented
    {
        private MainModel _model;
        private ArticleModel _newArtcile;
        private ArticleModel _selectedArticle;
        private ObservableCollection<int> _quantityRange;
        private int _selectQuantity;

        public ICommand ExitCommand { get; }
        public ICommand ReportCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddNewCommand { get; }
        public ICommand ClearInvoiceCommand {  get; }
        public ICommand PushInvoiceCommand {  get; }
        public ICommand AddToShipmentCommand { get; }
        public ICommand ResetShipmentCommand { get; }
        public ICommand MakeShipmentCommand { get; }
        public ICommand AboutWindowCommand { get; }


        public MainWindowViewModel()
        {
            _model = new MainModel();
            _newArtcile = new ArticleModel();
            _selectedArticle = new ArticleModel();

            //вкоманда на вихід
            ExitCommand = new RelayCommand(func => { Application.Current.Shutdown(); });
            
            //проведення інвентарізації
            ReportCommand = new RelayCommand(func => {
                string title = "Інвентарізація від -- " + DateTime.Now.ToString("dd-MM-yyyy");
                SaveInvoiceTofile(WareHouse, title);
            });
            
            //збереження стану складу в json файл
            SaveCommand = new RelayCommand(func => { _model.SaveToJson("data.json"); });
            
            //додавання товару в прибуткову накладну
            AddNewCommand = new RelayCommand(func => { 
                NewArticle.LastUpdating = DateTime.Now.ToString("dd-mm-yyyy HH:mm");
                NewInvoice.Add(NewArticle); 
                NewArticle = new ArticleModel(); 
            }, (obj)=>NewArticle.IsEmpty());

            //очистка вхідної накладної
            ClearInvoiceCommand = new RelayCommand(func => { NewInvoice.Clear(); }, (obj) => NewInvoice.Count > 0) ;
            
            //комнада прийому товару з прибуткової накладної
            PushInvoiceCommand = new RelayCommand(func => {
                _model.PushArticles(NewInvoice, ref _model._wareHouse);
                string title = "Прибуткова накладна від -- " + DateTime.Now.ToString("dd-MM-yyyy HH-mm");
                SaveInvoiceTofile(NewInvoice, title);
                _model.MakeCopyWareHouse();
                OnPropertyChanged(nameof(WareHouse));
                ClearInvoiceCommand.Execute(null);
                SaveCommand.Execute(null);
            }, (obj) => NewInvoice.Count > 0);


            //команда додавання товару в видаткову накладну
            AddToShipmentCommand = new RelayCommand(func =>
            {
                //додаємо товар в видатокву накладну
                var shipedArticle = new ArticleModel
                {
                    Name = SelectedArticle.Name,
                    Quantity = SelectQuantity,
                    MeasureUnit = SelectedArticle.MeasureUnit,
                    LastUpdating = SelectedArticle.LastUpdating,
                    Comment = SelectedArticle.Comment,
                    Price = SelectedArticle.Price,
                };
                NewShipment.Add(shipedArticle);

                //пошук цього товару в копії основного складу
                ArticleModel? _article = CopyWareHouse.FirstOrDefault(
                    article => article.Name == shipedArticle.Name && 
                    article.Price == shipedArticle.Price && 
                    article.MeasureUnit == shipedArticle.MeasureUnit);
                if (_article != null)
                {
                    //віднімаємо вказану кількість
                    _article.Quantity -= SelectQuantity;   
                }
                
            }, (obj)=>{
                if (SelectedArticle == null)
                    return false;
                return SelectedArticle.IsEmpty();
            });
            
            //скинути видатокову накладну
            ResetShipmentCommand = new RelayCommand(func =>
            {
                _model.MakeCopyWareHouse();
                NewShipment.Clear();
                SelectedArticle = new ArticleModel();
            }, (obj) => NewShipment.Count > 0);

            //відвантаження товару
            MakeShipmentCommand = new RelayCommand(func =>
            {
                _model.WareHouse = CopyWareHouse;
                //_model.MakeCopyWareHouse();
                OnPropertyChanged(nameof(WareHouse));
                string title = "Видаткова накладна від #" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                SaveInvoiceTofile(NewShipment, title);
                NewShipment.Clear();
                SaveCommand.Execute(null);
            }, (obj) => NewShipment.Count > 0);

            //вікно про програму
            AboutWindowCommand = new RelayCommand(func =>
            {
                AboutWindow aboutWindow = new AboutWindow();
                aboutWindow.ShowDialog();
            });

        }


        public int SelectQuantity
        {
            get => _selectQuantity;
            set => Set(ref _selectQuantity, value);
        }

        public ObservableCollection<ArticleModel> NewShipment 
        { 
            get => _model.NewShipment; 
        }

        public ObservableCollection<int> QuantityRange
        {
            get => _quantityRange;
            set=> Set(ref _quantityRange, value);
        }

        public ArticleModel SelectedArticle
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                if (_selectedArticle != null)
                {
                    QuantityRange = new ObservableCollection<int>(Enumerable.Range(1,SelectedArticle.Quantity));
                }
                OnPropertyChanged(nameof(SelectedArticle));
                OnPropertyChanged(nameof(QuantityRange));
            }
        }

        public ArticleModel NewArticle
        {
            get => _newArtcile;
            set => Set(ref _newArtcile, value);
        }

        public ObservableCollection<ArticleModel> WareHouse
        {
            get => _model.WareHouse;
            set => _model.WareHouse = value;
        }

        public ObservableCollection<ArticleModel> NewInvoice
        {
            get => _model.NewInvoice;
        }

        public ObservableCollection<ArticleModel> CopyWareHouse
        {
            get => _model.CopyWareHouse;
        }

        public void SaveInvoiceTofile(ObservableCollection<ArticleModel> articles, string title = "")
        {
            string line = "\n---------------------------------------------------------------------------------------------------\n";
            string text = title + line +$"{"|Назва товару",-25}{"|Кількіть",-10}{"|Одиниці",-10}{"|Ціна",-7}{"|Оновлення",-20}{"|Коментар              |"}" + line;
            foreach (var item in articles)
            {
                text += $"{item.Name,-25}{item.Quantity,-10}{item.MeasureUnit,-10}{item.Price,-7}{item.LastUpdating,-20}{item.Comment}\n";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = title;

            if (saveFileDialog.ShowDialog() ?? false)
            {
                File.WriteAllText(saveFileDialog.FileName, text);
                try
                {
                    Process.Start("notepad.exe", saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка відкриття блокнота: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
