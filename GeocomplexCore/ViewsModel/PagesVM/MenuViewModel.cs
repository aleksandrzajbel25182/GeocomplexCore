using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.PagesVM
{
    internal class MenuViewModel : ViewModel
    {
        private NavigationManager navigationManager;

        public MenuViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
            GoToPolevoiDnevnikComman = new LamdaCommand(OnGoToPolevoiDnevnikCommandExcuted, CanGoToPolevoiDnevnikCommandExecute);
        }

        #region Команды


        #region Команда Перехода в полевой дневник к ПРОЕКТАМ
        /// <summary>
        ///  Команда Перехода в полевой дневник к ПРОЕКТАМ
        /// </summary>
        public ICommand GoToPolevoiDnevnikComman { get; } // Сама команда

        private bool CanGoToPolevoiDnevnikCommandExecute(object p) => true;

        // Действия которые должна выполнить команда
        private void OnGoToPolevoiDnevnikCommandExcuted(object p)
        {

            GoPages();
        }

        #endregion

        private void GoPages()
        {
            navigationManager.Navigate("ProjectPage");
        }

        #endregion

    }
}
