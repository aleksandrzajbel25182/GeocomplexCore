using GeocomplexCore.Service;
using GeocomplexCore.Views.Windows;
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
            displayRootRegistry.RegisterWindowType<MainWindowViewModel, MainWindow>();
            displayRootRegistry.RegisterWindowType<AddWindowsViewModel, AddWindows>();

        }
        //protected override async void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    _authorizationWindowViewModel = new AuthorizationWindowViewModel();

        //    await displayRootRegistry.ShowModalPresentation(_authorizationWindowViewModel);

        //    Shutdown();
        //}
    }
}
