using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class InfoPointObservationViewModel : ViewModel, INavigatedToAware
    {
        #region Параметры/Parametrs

        private NavigationManager navigationmaneger;

        /// <summary>
        /// Переменная хранимая данные переданные из другой страницы
        /// </summary>
        private int _passedParameter;
        public int PassedParameter
        {
            get => _passedParameter;
            set => Set(ref _passedParameter, value);
        }
        /// <summary>
        /// Принимаем переменную переданную из другой страницы
        /// </summary>
        /// <param name="arg"></param>
        public void OnNavigatedTo(object arg)
        {
            if (!(arg is int))
                return;

            PassedParameter = (int)arg;

        }


        #endregion
        // ---------------------------------------------------------------------------------------------------------------------
        #region Команды//Commands

        #endregion
        // ---------------------------------------------------------------------------------------------------------------------

        #region Функции/Function

        #endregion
        // ---------------------------------------------------------------------------------------------------------------------



        public InfoPointObservationViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
        }
    }
}
