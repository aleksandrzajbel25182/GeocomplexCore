using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.Model;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.PagesVM;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using GeocomplexCore.Service;

namespace GeocomplexCore.ViewsModel.WindowsVM
{
    internal class MainWindowViewModel: ViewModel
    {
        NavigationManager _navigationmaneger;


        /// <summary>
        /// Список элементов меню
        /// </summary>
        public List<MenuModel> MenuItem { get; set; }

        /// <summary>
        /// Выбранный элемент из меню
        /// </summary>
        private MenuModel _selectedMenuItem;

        public MenuModel SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged("SelectedModule");
                GoNext();

            }
        }


        public MainWindowViewModel(NavigationManager navigationmaneger)
        {
            _navigationmaneger = navigationmaneger;

            MenuItem = new List<MenuModel>()
            {
                new MenuModel("ДОМОЙ", $"/Resources/Icons/HamburgerMenuIcons/Домой.ico"),
                new MenuModel("ПОЛЕВОЙ ДНЕВНИК", $"/Resources/Icons/HamburgerMenuIcons/Полевой-дневник.ico"),
                new MenuModel("КАТАЛОГ", $"/Resources/Icons/HamburgerMenuIcons/Каталог.ico"),
                new MenuModel("ЛАБАРАТОРНЫЕ АНАЛИЗЫ", $"/Resources/Icons/HamburgerMenuIcons/Лабараторные-анализы.ico"),
                new MenuModel("КАРТА", $"/Resources/Icons/HamburgerMenuIcons/Карта.ico"),
                new MenuModel("ФОНДОВЫЕ", $"/Resources/Icons/HamburgerMenuIcons/Фондовые.ico"),
                new MenuModel("АРХИВ", $"/Resources/Icons/HamburgerMenuIcons/Архив.ico"),              

            };
        }

        /// <summary>
        /// Переходы по боковому меню (HamburgerMenu)
        /// </summary>
        public void GoNext()
        {
            switch (SelectedMenuItem.Name)
            {
                case "ДОМОЙ":
                    _navigationmaneger.Navigate("Menu");
                    break;
                case "ПОЛЕВОЙ ДНЕВНИК":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
                case "КАТАЛОГ":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
                case "ЛАБАРАТОРНЫЕ АНАЛИЗЫ":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
                case "КАРТА":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
                case "ФОНДОВЫЕ":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
                case "АРХИВ":
                    MessageService.ShowMessage("Данный пункт в разработке");
                    break;
            }
        }


    }
}
