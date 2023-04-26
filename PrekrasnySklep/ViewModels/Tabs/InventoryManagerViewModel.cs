using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Tabs;

public class InventoryManagerViewModel : TabbedViewModel
{
    private readonly ProductService _productService;

    private ObservableCollection<Product> products;
    private Product? selectedProduct;

    public RelayCommand AddProductCommand { get; }
    public RelayCommand AddCategoryCommand { get; }
    public RelayCommand RemoveCategoryCommand { get; }
    public RelayCommand EditProductCommand { get; }

    public InventoryManagerViewModel() : base(title: "Inventory Manager") {
        _productService = new ProductService();


        AddProductCommand = new RelayCommand(AddProduct);
        AddCategoryCommand = new RelayCommand(AddCategory);
        RemoveCategoryCommand = new RelayCommand(RemoveCategory);
        EditProductCommand = new RelayCommand(Edit,CanEdit);

        SelectedProduct = null!;
        Products = new ObservableCollection<Product>(AppState.SharedContext.Products.Include(p => p.Category).ToList());
    }

    public Product SelectedProduct
    {
        get => selectedProduct!;
        set
        {
            selectedProduct = value;
            EditProductCommand.RaiseCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Product> Products
    {
        get => products;
        set
        {
            products = value;
            SelectedProduct = null!;
            EditProductCommand.RaiseCanExecuteChanged();
            OnPropertyChanged();
        }
    }

    private void AddProduct(object parameter)
    {

    }

    private void AddCategory(object parameter)
    {

    }

    private void RemoveCategory(object parameter)
    {

    }

    private void Edit(object parameter)
    {
        int a = 1;
    }

    private bool CanEdit(object parameter) 
    {
        return selectedProduct is not null;
    }

    public override void Sync()
    {
        EditProductCommand.RaiseCanExecuteChanged();
        base.Sync();
    }
}
