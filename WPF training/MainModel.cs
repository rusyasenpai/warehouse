using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WPF_training
{
    internal class MainModel
    {
        ObservableCollection<ArticleModel> _newInvoice;
        ObservableCollection<ArticleModel> _newShipment;
        public ObservableCollection<ArticleModel> _wareHouse;
        public ObservableCollection<ArticleModel> _copyWareHouse;


        public MainModel()
        {
            _newShipment = new ObservableCollection<ArticleModel>();
            _newInvoice = new ObservableCollection<ArticleModel>();
            _wareHouse = new ObservableCollection<ArticleModel>();
            _copyWareHouse = new ObservableCollection<ArticleModel>();

            bool success = true;
            string path = "data.json";

            if (File.Exists(path))
            {
                try {LoadFromJson(path);}
                catch {success = false;}
            }
            else success = false;
            
            if(!success) _wareHouse.Add(new ArticleModel("Тестовий товар", "кг", 175, 5, "20.11.2023","Тестовий товар"));

            MakeCopyWareHouse();
        }

        public ObservableCollection<ArticleModel> NewShipment
        {
            get => _newShipment;
            set => _newShipment = value;
        }

        public ObservableCollection<ArticleModel> CopyWareHouse
        {
            get => _copyWareHouse;
            set => _copyWareHouse = value;
        }

        public ObservableCollection<ArticleModel> WareHouse 
        { 
            get => _wareHouse;
            set => _wareHouse = value;
        }

        public ObservableCollection<ArticleModel> NewInvoice
        {
            get => _newInvoice;
        }

        public void MakeCopyWareHouse()
        {
            _copyWareHouse.Clear();
            foreach (var item in _wareHouse)
            {
                _copyWareHouse.Add(new ArticleModel(item));
            }
        }

        public void PushArticles(ObservableCollection<ArticleModel> articles, ref ObservableCollection<ArticleModel> destination)
        {
            var newCollection = destination.Concat(articles)
                .GroupBy(item=>new { item.Name, item.Price, item.MeasureUnit})
                .Select(group=>new ArticleModel
                {
                    Name = group.Key.Name,
                    Quantity = group.Sum(item=>item.Quantity),
                    Price = group.Key.Price,
                    MeasureUnit = group.Key.MeasureUnit,
                    LastUpdating = group.Last().LastUpdating,
                    Comment = group.Last().Comment,
                });
            destination = new ObservableCollection<ArticleModel>(newCollection);
        }

        public void LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            _wareHouse = JsonConvert.DeserializeObject<ObservableCollection<ArticleModel>>(json);
        }
        public void SaveToJson(string path)
        {
            string json = JsonConvert.SerializeObject(WareHouse, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
