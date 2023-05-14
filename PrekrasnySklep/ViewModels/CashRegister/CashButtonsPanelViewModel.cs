using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.CashRegister
{
    internal class CashButtonsPanelViewModel : ViewModelBase
    {
        public RelayCommand AddToCart { get; }
        public RelayCommand RemoveItemFromBasket { get; }
        public RelayCommand ClearBasketCommand { get; }
        public RelayCommand AddQuantityCommand { get; }
        public RelayCommand RemoveQuantityCommand { get; }
        private CashRegisterViewModel _parent;
        private readonly BasketService _basketService;
        public CashButtonsPanelViewModel(CashRegisterViewModel parent)
        {
            _parent = parent;
            _basketService = new BasketService();
            AddToCart = new RelayCommand(OpenProductList);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            RemoveItemFromBasket = new RelayCommand(RemoveItem, CanRemove);
            AddQuantityCommand = new RelayCommand((_) => EditQuantity(+1), (_) => CanEditQuantity(+1));
            RemoveQuantityCommand = new RelayCommand((_) => EditQuantity(-1),(_) => CanEditQuantity(-1));
        }

        public void OpenProductList(object sender)
        {
            _parent.CurrentPanel = new AddToCartViewModel(_parent, this);
        }

        private void RemoveItem(object sender)
        {
            if (_parent.SelectedBasketItem != null)
            {
                var selectedItem = _parent.SelectedBasketItem;
                _parent.SelectedBasketItem = null;
                _basketService.RemoveFromBasket(selectedItem.Product, selectedItem.Quantity);
                _parent.Cart = _basketService.GetCurrentBasket();
            }
        }
        private void ClearBasket(object sender) 
        {
            _parent.SelectedBasketItem = null;
            _basketService.ClearBasket();
            _parent.Cart = _basketService.GetCurrentBasket();
        }

        private bool CanRemove(object sender)
        {
            return _parent.SelectedBasketItem is not null;
        }

        private void EditQuantity(int quantity)
        {
            _basketService.AdjustBasketQuantity(_parent.SelectedBasketItem!.Product, quantity);
            _parent.Cart = _basketService.GetCurrentBasket();
        }

        private bool CanEditQuantity(int value)
        {
            return _parent.SelectedBasketItem is not null && _parent.SelectedBasketItem.Quantity + value <= _parent.SelectedBasketItem.Product.Stock;
        }
    }
}
