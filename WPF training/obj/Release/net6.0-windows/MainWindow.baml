   M S B A M L   `   `   `  ���� G  CWareHousePrj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WPF_trainingWPF_training  R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem +clr-namespace:WPF_trainingWPF_training      WPF_training.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:WPF_training     <system)clr-namespace:System;assembly=netstandard     )vmclr-namespace:WPF_training       � Title$  Склад товару��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=� � DataContext &   WPF_training.MainWindowViewModel  .��� .+�]� .+�o� $	��Top=�.@�m� $��
Склад��.@�m� $"��Інвентарізація�� m� Command �� *ReportCommand+�� m� $��
Вихід�� �� *ExitCommand+m� $��Довідка��.@�m� $��Про програму�� �� *AboutWindowCommand+�� $��Bottom=�$��25��.@��� .���� $����Не забувайте завершувати операції прийому та видачі! Якщо операція не потрібна то скасуйте її!��$��14(��� .@��� $��Наявний товар��.���  � RowDefinitions �� $��23 ��� $��* �.+�&   System.Windows.Controls.DataGrid  $��1��   AutoGenerateColumns$ False��   CanUserAddRows$ False��   CanUserDeleteRows$ False��$��14(����� *	WareHouse+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Назва��
   
IsReadOnly$

 True��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $	 Кількість��$

 True�� �� *
Quantity+  $!	 Одиниця виміру��$

 True�� �� *MeasureUnit+  $	 Ціна��$

 True�� �� *Price+  $'	 !Останнє оновлення��$

 True�� �� *LastUpdating+  $	 Коментарій��$
 False��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *  �� *	Comment+�� $��Прийом товару��.���  �� $
��auto ��� $��* �.+��� $��0�� �� Orientation$ Vertical=� �� *
NewArticle+.+��� $ 
Horizontal=�.+��� $��Назва товару��$��5q�$	��100���� $��5q�$	��150��$
��auto������ *Name+ �� UpdateSourceTrigger$ PropertyChanged=��� $ 
Horizontal=�.+��� $%��Кількість товару��$��5q�$	��100���� $��5q�$	��150��$
��auto������ *
Quantity+$ PropertyChanged=��� $ 
Horizontal=�.+��� $!��Одиниці виміру��$��5q�$	��100���� $��5q�$	��150��$
��auto������ *MeasureUnit+$ PropertyChanged=��� $ 
Horizontal=�.+��� $��Ціна��$��5q�$	��100���� $��5q�$	��150��$
��auto������ *Price+$ PropertyChanged=��� $ 
Horizontal=�.+��� $��Коментарій��$��5q�$	��100���� $��5q�$	��150��$
��auto������ *	Comment+$ PropertyChanged=��� $ 
Horizontal=�.+��� $&�� Додати в накладну��$	��200��$��30,10,10,15q����� *DataContext.AddNewCommand+ �� RelativeSource ��  �� AncestorType
 Window�� $)��#Скасувати накладну��$	��200��$��30,10,10,15q����� *!DataContext.ClearInvoiceCommand+ �� 
 Window�� $#��Внести накладну��$	��200��$��30,10,10,15q����� * DataContext.PushInvoiceCommand+ �� 
 Window�� $ Vertical=�$��1��.+�  $ False��$��14(�$ False��$
��auto������ *
NewInvoice+   $	 
Назва��$
 False�� �� *Name+  $	 Кількість��$
 False�� �� *
Quantity+  $!	 Одиниця виміру��$
 False�� �� *MeasureUnit+  $	 Ціна��$
 False�� �� *Price+  $	 Коментарій��$
 False��$ *  �� *	Comment+�� $��Видача товару��.���  �� $
��147* ��� $
��auto ��� $
��176* �.+�  $��0��$ False��$��14(�$ False��   
IsReadOnly$
 True��"   VerticalScrollBarVisibility$ Visible=����� *CopyWareHouse+ �� SelectedItem �� *SelectedArticle+$ PropertyChanged=�   $	 
Назва�� �� *Name+  $	 Кількість�� �� *
Quantity+  $!	 Одиниця виміру�� �� *MeasureUnit+  $	 Ціна�� �� *Price+  $'	 !Останнє оновлення�� �� *LastUpdating+  $	 Коментарій��$ *  �� *	Comment+�� $��1��$ Vertical=�$��Center=�.+��� $ 
Horizontal=� �� *SelectedArticle+.+��� $!��Вибраний товар��$��5q�$	��150���� $��5q�$	��150�� �� 
IsReadOnly$
 True������ *Name+�� $��Ціна��$��5q�$��75���� $��5q�$	��150��$
 True������ *Price+�� $ 
Horizontal=� �� *SelectedArticle+.+��� $+��%Одиниці виміровання��$��5q�$	��150���� $��5q�$	��150��$
 True������ *MeasureUnit+�� $��Кількість��$��5q�$��75����  �� SelectedIndex$ 0��$��5q�$	��150�� �� SelectedValue �� *DataContext.SelectQuantity+ �� 
 Window���� *DataContext.QuantityRange+ �� 
 Window�� $ 
Horizontal=�$��Center=�.+��� $&�� Додати в накладну��$��5q����� *AddToShipmentCommand+�� $)��#Скасувати накладну��$��5q����� *ResetShipmentCommand+�� $?��9Відвантажити товар з накладної��$��5q����� *MakeShipmentCommand+  $��2��$ False��$��14(�$ False��$
 True��$ Visible=����� *NewShipment+   $	 
Назва�� �� *Name+  $	 Кількість�� �� *
Quantity+  $!	 Одиниця виміру�� �� *MeasureUnit+  $	 Ціна�� �� *Price+  $'	 !Останнє оновлення�� �� *LastUpdating+  $	 Коментарій��$ *  �� *	Comment+