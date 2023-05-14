using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class EditProductModel : ViewModelBase
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
        public Product product { get; set; }
        public RelayCommand EditProductCommand { get; }
        public EditProductModel(Product productToEdit)
        {
            product = productToEdit;
            EditProductCommand = new RelayCommand(EditProduct, CanExecute);
            IList<Category> list = AppState.SharedContext.Categories.ToList();
            _categories = new CollectionView(list);
        }
        public CollectionView CategoryChoose
        {
            get { return _categories; }
        }
        public void EditProduct(object sender)
        {
            product.ImagePath = null;
            product.CategoryId = _category;
            var result = AppState.SharedContext.Products.Update(product);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<EditProduct>().FirstOrDefault()!.Close();
            }
        }
        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(product.Name) && !string.IsNullOrWhiteSpace(product.Description) && _category!= 0; //TODO- dopisac jeszcze pozostałe warunki
        }
    }
}
