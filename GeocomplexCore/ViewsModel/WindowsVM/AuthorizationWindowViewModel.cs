using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.Views;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.Views.Pages.Polevoi;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.PagesVM;
using GeocomplexCore.ViewsModel.PagesVM.PolevoiVM;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.WindowsVM
{
    internal class AuthorizationWindowViewModel : ViewModel
    {
        #region Параметры / Parametrs
        NavigationManager _navigationmaneger;

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
        private NavigationManager navigationManager;

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
                    //Autrorization();
                    _navigationmaneger.Navigate("StartPage");
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

        /// <summary>
        /// Подлючение к базе, проверка в базе логина и пароля. Занесение в глабальную переменную его ID если логин и пароль верны.
        /// </summary>
        /// <returns></returns>
        /// 
        public bool Autrorization()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {
                var autoriz = db.UserData.FirstOrDefault(u => u.UserLogin == Login && u.UserPassword == Password);

                if (autoriz!=null)
                {
                    GlobalSet.staticUserID = autoriz.UserId.ToString();
                    return true;
                }
                else
                    return false;
            }
        }

        //public async void Autrorization()
        //{
        //    using (GeocomplexContext db = new GeocomplexContext())
        //    {

        //        var autoriz = await db.UserData.ToListAsync();
        //        foreach (var item in autoriz)
        //        {
        //           if(item.UserLogin == Login && item.UserPassword == Password)
        //            {
        //                GlobalSet.staticUserID = item.UserId.ToString();

        //            }
        //        }
        //        autoriz.Clear();

        //    }
        //}


        /*-------------------------------------------------------------------------------------------------------------------------------------------*/


        public AuthorizationWindowViewModel(NavigationManager navigationManager)
        {
            _navigationmaneger = navigationManager;
            ConnectionCommand = new LamdaCommand(OnConnectionCommandExcuted, CanConnectionCommandExecute);
        }
    }
}
