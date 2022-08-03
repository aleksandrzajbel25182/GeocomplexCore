using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.ViewsModel.Base;
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
            //connection = new NpgsqlConnection(connstring);
            //connection.Open();
            //string cmd = $"SELECT * FROM user_data WHERE user_login='{login}' AND user_password ='{password}'"; // Создаем запрос для вывода 
            //NpgsqlCommand createCommand = new NpgsqlCommand(cmd, connection); // ложим запрос в команду и подключение к бд
            //createCommand.ExecuteNonQuery();

            //NpgsqlDataAdapter dataAdp = new NpgsqlDataAdapter(createCommand);
            //DataTable dt = new DataTable("user_data"); // В скобках указываем название таблицы
            //dataAdp.Fill(dt);


            //if (dt.Rows.Count > 0) // если такая запись существует       
            //{
            //    connection.Close();

            //    var mainWindow = new MainWindow();

            //    var modules = ReflectionHelper.CreateAllInstancesOf<IModule>();

            //    var vm = new MainWindowViewModel(modules);
            //    mainWindow.DataContext = vm;
            //    mainWindow.Closing += (s, args) => vm.SelectedModule.Deactivate();
            //    mainWindow.Show();

            //}
            //else
            //{
            //    MessageService.ShowMessage("Неправильно введен пароль или логин!");
            //}
        }

        #endregion


        #endregion





        /*-------------------------------------------------------------------------------------------------------------------------------------------*/
        public AuthorizationWindowViewModel()
        {
            ConnectionCommand = new LamdaCommand(OnConnectionCommandExcuted, CanConnectionCommandExecute);

        }



    }
}
