using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.DAL.Context;
using GeocomplexCore.Model;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.Views.Pages.Polevoi;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.PagesVM;
using GeocomplexCore.ViewsModel.PagesVM.PolevoiVM;
using System.Collections.Generic;
using System.Windows.Controls;

namespace GeocomplexCore.ViewsModel.WindowsVM
{

    internal class StartPageViewModel : ViewModel
    {
        #region Parametrs/Параметры
        NavigationManager _navigationmaneger;
        GeocomplexContext db;

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
      

        /// <summary>
        /// Контрол для отображения страниц
        /// </summary>
        private ContentControl _contentcontrol;

        public ContentControl ContentControl
        {
            get => _contentcontrol;
            set
            {
                _contentcontrol = value;
                OnPropertyChanged("ContentControl");


            }
        }

        #endregion


        public StartPageViewModel(GeocomplexContext _db)
        {

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
            ContentControl = new MenuView();
            _navigationmaneger = new NavigationManager(ContentControl);
            db = _db;
            ////Регистрация ключа (строки) с соответствующими View и ViewModel для него
            _navigationmaneger.Register<MenuView>("Menu", () => new MenuViewModel(_navigationmaneger));
            _navigationmaneger.Register<ProjectPageView>("ProjectPage", () => new ProjectViewModel(_navigationmaneger, db));
            _navigationmaneger.Register<InfoDistrisctView>("InfoDistrict", () => new InfoDistrisctViewModel(_navigationmaneger, db));
            _navigationmaneger.Register<InfoPointObservationView>("InfoPointObservation", () => new InfoPointObservationViewModel(_navigationmaneger, db));


            //Отображение стартового UI
            _navigationmaneger.Navigate("Menu");
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
                    LocatorStatic.Data.TextMainHeader = "ГЛАВНОЕ МЕНЮ";
                    LocatorStatic.Data.PageHeader = "";
                    break;
                case "ПОЛЕВОЙ ДНЕВНИК":
                    _navigationmaneger.Navigate("ProjectPage");
                    LocatorStatic.Data.TextMainHeader = "ПОЛЕВОЙ ДНЕВНИК";
                    LocatorStatic.Data.PageHeader = "Проекты";
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
