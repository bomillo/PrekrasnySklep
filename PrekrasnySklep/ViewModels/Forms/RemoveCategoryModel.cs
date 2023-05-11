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

namespace PrekrasnySklep.ViewModels.Forms
{
    public class RemoveCategoryModel : ViewModelBase
    {
        private Category _category;
        private readonly CollectionView _categories;
        public Category Categories
        {
            get { return _category; }
            set
            {
                if (_category == value) return;
                var catId = AppState.SharedContext.Categories.FirstOrDefault(c => c.Name == _category.Name);
                _category = value;
                OnPropertyChanged("Categories");
            }
        }
        public RelayCommand RemoveCategoryCommand { get; }
        Category category = new Category();
        public RemoveCategoryModel()
        {
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
            category.Name = (string)sender;
            var result = AppState.SharedContext.Categories.Remove(category);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<RemoveCategory>().FirstOrDefault()!.Close();
            }
        }
        private bool CanExecute(object sender)
        {
            return false; //TODO- warunek na wybranie kategorii
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
