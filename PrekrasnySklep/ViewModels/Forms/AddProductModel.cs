using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using PrekrasnySklep.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class CategoryEntry
    {
        public string NameCategory { get; set; }

        public CategoryEntry(string name)
        {
            NameCategory = name;
        }

        public override string ToString()
        {
            return NameCategory;
        }
    }
    public class AddProductModel : ViewModelBase
    {
        private string _name { get; set; }
        private string _description;
        private int _category;
        private double _price;
        private int _stock;
        private readonly CollectionView _categories;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public int Categories
        {
            get { return _category; }
            set
            {
                if (_category == value) return;
                _category = value;
                OnPropertyChanged();
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public int Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand AddProductCommand { get; }
        Product product = new Product();

        public AddProductModel(){
            AddProductCommand = new RelayCommand(AddProduct, CanExecute);
            IList<Category> list = AppState.SharedContext.Categories.ToList();
            _categories = new CollectionView(list);
        }
        public CollectionView CategoryChoose
        {
            get { return _categories; }
        }
        public void AddProduct(object sender)
        {
            product.ImagePath = null;
            
            product.Name = Name;
            product.Price = Price;
            product.Description = Description;
            product.CategoryId = _category;
            product.Price = Price;
            product.Stock = Stock;
            var result = AppState.SharedContext.Products.Add(product);
            AppState.SharedContext.SaveChanges();
            if(result is not null)
            {
                Application.Current.Windows.OfType<AddProduct>().FirstOrDefault()!.Close();
            }
        }

        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_description) && _category != 0 ; //TODO- dopisac jeszcze pozostałe warunki
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;




    }



}
