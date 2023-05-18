using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.CashRegister;
using PrekrasnySklep.ViewModels.Forms;
using System.Collections.Generic;
using System.Windows;

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

    public RelayCommand OrderCommand { get; }

    private readonly BasketService _basketService;
    public CashRegisterViewModel() : base(title: "Cash Register")
    {
        _basketService = new BasketService();
        Cart = _basketService.GetCurrentBasket();
        CurrentPanel = new CashButtonsPanelViewModel(this);
        OrderCommand = new RelayCommand(Order, _ => Cart.Count != 0);
    }

    public void Order(object sender)
    {
        OrderModel model = new OrderModel();
        Window window = new PrekrasnySklep.Views.Forms.Order(model);
        window.DataContext = model;
        window.ShowDialog();
        Cart = new List<BasketItem>();
    }

    public override void Sync() { }
}
