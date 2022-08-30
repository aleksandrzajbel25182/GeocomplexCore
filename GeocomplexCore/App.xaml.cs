using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.Service;
using GeocomplexCore.Views;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.Views.Pages.Polevoi;
using GeocomplexCore.Views.Windows;
using GeocomplexCore.ViewsModel.PagesVM;
using GeocomplexCore.ViewsModel.PagesVM.PolevoiVM;
using GeocomplexCore.ViewsModel.WindowsVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeocomplexCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
        //AuthorizationWindowViewModel _authorizationWindowViewModel;
        public App()
        {
            displayRootRegistry.RegisterWindowType<StartPageViewModel, MainWindow>();
            displayRootRegistry.RegisterWindowType<AddWindowsViewModel, AddWindows>();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            
            var mainWindow = new MainWindow();
            var navigationManager = new NavigationManager(mainWindow);
            mainWindow.DataContext = new MainWindowViewModel();
            //Регистрация ключа (строки) с соответствующими View и ViewModel для него
            navigationManager.Register<Autorization>("Autorization", () => new AuthorizationWindowViewModel(navigationManager));
            navigationManager.Register<StartPageView>("StartPage", () => new StartPageViewModel());

            //Отображение стартового UI
            navigationManager.Navigate("Autorization");

            mainWindow.Show();

            
        }

    }
}
