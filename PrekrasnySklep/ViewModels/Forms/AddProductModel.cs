using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using PrekrasnySklep.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class AddProductModel : ViewModelBase
    {
        private string _name;
        private string _description;
        private Category _category;
        private double _price;
        private int _stock;
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
        public Category category
        {
            get => _category;
            set
            {
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

        }

        public void AddProduct(object sender)
        {
            product.ImagePath = null;
            product.CategoryId = 1;
            product.Name = Name;
            product.Price = Price;
            product.Description = Description;
            product.Category = AppState.SharedContext.Categories.First(); //TODO- podmiandka na to co z listy
            product.Price = Price;
            product.Stock = Stock;
            AppState.SharedContext.Products.Add(product);
            AppState.SharedContext.SaveChanges();
            //Application.Current.Windows.OfType<AddProduct>().FirstOrDefault()!.Close();

            /*product.Price = 10;
            product.Name = "pies";
            product.Category = AppState.SharedContext.Categories.First();
            AppState.SharedContext.Products.Add(product);//product
            AppState.SharedContext.SaveChanges();*/
        }

        private bool CanExecute(object sender)
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_description); //TODO- dopisac jeszcze pozostałe warunki
        }




    }



}
