using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class AddCategoryModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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
        public RelayCommand AddCategoryCommand { get; }
        Category category = new Category();

        public AddCategoryModel()
        {
            AddCategoryCommand = new RelayCommand(AddCategory, CanEdit);

        }

        public void AddCategory(object sender)
        {
            category.Name = Name;
            var result = AppState.SharedContext.Categories.Add(category);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<AddCategory>().FirstOrDefault()!.Close();
            }
        }


        private bool CanEdit(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_name);
        }
    }
}
