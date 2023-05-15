using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Services;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class RemoveCategoryModel : ViewModelBase
    {
        private int _category;
        private readonly CollectionView _categories;
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
        public RelayCommand RemoveCategoryCommand { get; }
        Category category = new Category();
        private readonly CategoryService _categoryService;
        public RemoveCategoryModel()
        {
            _categoryService = new CategoryService();
            RemoveCategoryCommand = new RelayCommand(RemoveCategory, CanExecute);
            IList<Category> list = AppState.SharedContext.Categories.ToList();
            _categories = new CollectionView(list);
        }
        public CollectionView CategoryChoose
        {
            get { return _categories; }
        }
        public void RemoveCategory(object sender)
        {

            Category category = AppState.SharedContext.Categories.FirstOrDefault(c => c.Id == _category);
            if(_categoryService.RemoveCategory(category))
            {
                Application.Current.Windows.OfType<RemoveCategory>().FirstOrDefault()!.Close();
            }
            else
            {
                ErrorMessage = "Category has products assigned to it";
                OnPropertyChanged();
            }
            
            /*var products = AppState.SharedContext.Products.Where(p => p.CategoryId == _category).ToList();
            if (products is not null)
            {
                AppState.SharedContext.Products.RemoveRange(products);
            }
            var result = AppState.SharedContext.Categories.Remove(category);
            AppState.SharedContext.SaveChanges();
            
            if (result is not null)
            {
                Application.Current.Windows.OfType<RemoveCategory>().FirstOrDefault()!.Close();
            }
            */
            
        }
        private bool CanExecute(object sender)
        {
            return _category!=0 ; //TODO- warunek na wybranie kategorii
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
