using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.CashRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Tabs;

public class CashRegisterViewModel : TabbedViewModel
{
    private ViewModelBase _currentPanel;
    private List<BasketItem> _cart;
    public List<BasketItem> Cart 
    {
        get => _cart;
        set
        {
            _cart = value;
            OnPropertyChanged();
        }
    }
    private BasketItem? _selectedBasketItem;
    public BasketItem? SelectedBasketItem
    {
        get => _selectedBasketItem;
        set
        {
            _selectedBasketItem = value;
            OnPropertyChanged();
        }
    }
    public ViewModelBase CurrentPanel
    {
        get => _currentPanel;
        set
        {
            _currentPanel = value;
            OnPropertyChanged();
        }
    }
    private readonly BasketService _basketService;
    public CashRegisterViewModel() : base(title: "Cash Register")
    {
        _basketService = new BasketService();
        Cart = _basketService.GetCurrentBasket();
        CurrentPanel = new CashButtonsPanelViewModel(this);
        
    }

    public override void Sync()
    {

    }
}
