using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.DAL.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.ViewsModel.Base;
using System.Linq;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.WindowsVM
{
    internal class AuthorizationWindowViewModel : ViewModel
    {
        #region Параметры / Parametrs
        NavigationManager _navigationmaneger;
        GeocomplexContext db;        

        #region Логин
        
        private string login;
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get => login; set => Set(ref login, value); }
        #endregion

        #region Пароль
       
        private string password;
        /// <summary>
        /// Пароль пользователя
        /// </summary>
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
            var autoriz = db.UserData.FirstOrDefault(u => u.UserLogin == Login && u.UserPassword == Password);
            if (autoriz != null)
            {
                GlobalSet.staticUserID = autoriz.UserId.ToString();
                return true;
            }
            else
                return false;
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------*/

        public AuthorizationWindowViewModel(NavigationManager navigationManager , GeocomplexContext _db)
        {
            _navigationmaneger = navigationManager;
            db = _db;
            ConnectionCommand = new LamdaCommand(OnConnectionCommandExcuted, CanConnectionCommandExecute);
        }
    }
}
