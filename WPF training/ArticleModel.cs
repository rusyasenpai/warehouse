namespace WPF_training
{
    internal class ArticleModel : PropertyChangedImplemented
    {
        string _name;
        string _measureUnit;
        string _lastUpdating;
        string _comment;
        double _price;
        int _quantity;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string MeasureUnit 
        { 
            get => _measureUnit; 
            set => Set(ref _measureUnit, value); 
        }
        
        public double Price 
        { 
            get => _price; 
            set => Set(ref _price, value);
        }

        public int Quantity {
            get => _quantity;
            set => Set(ref _quantity, value);
        }

        public string LastUpdating 
        {
            get => _lastUpdating;
            set => Set(ref _lastUpdating, value); 
        }
        public string Comment 
        {
            get => _comment;
            set => Set(ref _comment, value);
        } 

        public ArticleModel()
        {
            Name = string.Empty;
            MeasureUnit = string.Empty;
            Price = 0;
            Quantity = 0;
            LastUpdating = string.Empty;
            Comment = string.Empty;
        }

        public ArticleModel(string name, string measureUnit, double price, int quantity, string lastUpdating, string comment = "")
        {
            Name = name;
            MeasureUnit = measureUnit;
            Price = price;
            Quantity = quantity;
            LastUpdating = lastUpdating;
            Comment = comment;
        }

        public ArticleModel(ArticleModel other)
        {
            Name = other.Name;
            MeasureUnit = other.MeasureUnit;
            Price = other.Price;
            Quantity = other.Quantity;
            LastUpdating = other.LastUpdating;
            Comment = other.Comment;
        }


        public bool IsEmpty()
        {
            return !(MeasureUnit == string.Empty ||
                    Quantity == 0 || Name == string.Empty);
        }
    }
}
