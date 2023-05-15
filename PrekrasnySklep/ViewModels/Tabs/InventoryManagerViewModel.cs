using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Forms;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    public RelayCommand DeleteProductCommand { get; }

    public InventoryManagerViewModel() : base(title: "Inventory Manager") {
        _productService = new ProductService();


        AddProductCommand = new RelayCommand(AddProduct);
        AddCategoryCommand = new RelayCommand(AddCategory);
        RemoveCategoryCommand = new RelayCommand(RemoveCategory);
        EditProductCommand = new RelayCommand(Edit,CanEdit);
        DeleteProductCommand = new RelayCommand(DeleteProduct, CanEdit);

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
        AddProductModel model = new AddProductModel();
        Window window = new AddProduct(model);
        window.DataContext = new AddProductModel();
        window.ShowDialog();
        Products = new ObservableCollection<Product>(AppState.SharedContext.Products.Include(p => p.Category).ToList());
        OnPropertyChanged();
    }

    private void AddCategory(object parameter)
    {
        AddCategoryModel model = new AddCategoryModel();
        Window window = new AddCategory(model);
        window.DataContext = new AddCategoryModel();
        window.ShowDialog();
    }

    private void RemoveCategory(object parameter)
    {
        RemoveCategoryModel model = new RemoveCategoryModel();
        Window window = new RemoveCategory(model);
        window.DataContext = new RemoveCategoryModel();
        window.ShowDialog();
    }

    private void Edit(object parameter)
    {
        EditProductModel model = new EditProductModel(SelectedProduct);
        Window window = new EditProduct(model);
        window.DataContext = model;
        window.ShowDialog();
        Products = new ObservableCollection<Product>(AppState.SharedContext.Products.Include(p => p.Category).ToList());
        OnPropertyChanged();

       
        
    }
    private void DeleteProduct(object parameter)
    {
        DeleteProductModel model = new DeleteProductModel(SelectedProduct);
        Window window = new DeleteProduct(model);
        window.DataContext = model;
        window.ShowDialog();
        Products = new ObservableCollection<Product>(AppState.SharedContext.Products.Include(p => p.Category).ToList());
        OnPropertyChanged();
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
