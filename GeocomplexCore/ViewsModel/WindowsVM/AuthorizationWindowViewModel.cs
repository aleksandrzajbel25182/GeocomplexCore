﻿using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.Views.Pages.Polevoi;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.PagesVM;
using GeocomplexCore.ViewsModel.PagesVM.PolevoiVM;
using System.Linq;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.WindowsVM
{
    internal class AuthorizationWindowViewModel : ViewModel
    {
        #region Параметры / Parametrs

        #region Логин
        /// <summary>
        /// Логин пользователя
        /// </summary>
        private string login;

        public string Login { get => login; set => Set(ref login, value); }
        #endregion

        #region Пароль
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        private string password;

        public string Password { get => password; set => Set(ref password, value); }
        #endregion

        #endregion
        /*-------------------------------------------------------------------------------------------------------------------------------------------*/


        #region Команды


        #region Команда Подключения и проверки логина с паролем пользователя
        /// <summary>
        ///  Команда Подключения и проверки логина с паролем пользователя
        /// </summary>
        public ICommand ConnectionCommand { get; } // Сама команда

        private bool CanConnectionCommandExecute(object p) => true;

        // Действия которые должна выполнить команда
        private void OnConnectionCommandExcuted(object p)
        {
            try
            {
                if (Autrorization())
                {
                    var mainWindow = new MainWindow();
                    
                    var navigationManager = new NavigationManager(mainWindow.Cont);

                    mainWindow.DataContext = new MainWindowViewModel(navigationManager);
                    //2. Определите правила навигации: зарегистрируйте ключ (строку) с соответствующими View и ViewModel для него

                    navigationManager.Register<MenuView>("Menu", () => new MenuViewModel(navigationManager));

                    navigationManager.Register<ProjectPageView>("ProjectPage", () => new ProjectViewModel(navigationManager));

                    //3. Отобразите стартовый UI
                    navigationManager.Navigate("Menu");
                   
                    mainWindow.Show();
                 
                }
                else
                {
                    MessageService.ShowMessage("Неправильно введен логин или пароль!");
                }

            }
            catch (System.Exception e)
            {
                MessageService.ShowMessage(e.Message);
            }
           


        }

        #endregion


        #endregion

        public bool Autrorization()
        {
            using(GeocomplexContext db = new GeocomplexContext())
            {
                var autoriz = db.UserData.Where(u => u.UserLogin == Login && u.UserPassword == Password).ToArray();

                if (autoriz.Length > 0)
                {
                    GlobalSet.staticUserID = autoriz[0].UserId.ToString(); 
                    return true;
                }                    
                else
                    return false;
            }            
        } 



        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        public AuthorizationWindowViewModel()
        {
            ConnectionCommand = new LamdaCommand(OnConnectionCommandExcuted, CanConnectionCommandExecute);

        }



    }
}
