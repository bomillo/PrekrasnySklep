using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.CashRegister
{
    internal class AddToCartViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products 
        { 
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        } 
        public RelayCommand AddToCart { get; }
        public RelayCommand PreviousView { get; }
        

        private readonly ProductService _productService;
        private readonly BasketService _basketService;
        private readonly CashRegisterViewModel _parent;
        private readonly CashButtonsPanelViewModel _previous;
        public AddToCartViewModel(CashRegisterViewModel parent, CashButtonsPanelViewModel previous)
        {
            _previous = previous;
            _parent = parent;
            _basketService = new BasketService();
            _productService = new ProductService();
            Products = new ObservableCollection<Product>(_productService.GetAvailableProducts());
            AddToCart = new RelayCommand(AddProductToCart, CanExecute);
            PreviousView = new RelayCommand(NavigateBack);
        }
        private void NavigateBack(object sender)
        {
            _parent.CurrentPanel = _previous;
        }
        private void AddProductToCart(object sender)
        {
            
            _basketService.AddToBasket(SelectedProduct!, Quantity);
            _parent.Cart = _basketService.GetCurrentBasket();
            _parent.CurrentPanel = _previous;
        }

        private bool CanExecute(object sender)
        {
            if(SelectedProduct is not null)
            {
                var product = AppState.SharedContext.Products.First(x => x.Id == SelectedProduct.Id);

                var cartProduct = _parent.Cart.FirstOrDefault(p => p.Product.Id == product.Id);

                return (product is not null && Quantity > 0 && Quantity <= product.Stock && (cartProduct is null || cartProduct.Quantity + Quantity <= product.Stock));
            }
            return false;
        }

        
    }
}
