using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class InfoPointObservationViewModel : ViewModel, INavigatedToAware
    {
        #region Параметры/Parametrs

        private NavigationManager navigationmaneger;

        private string _Id;
        public string ID
        {
            get
            {
                _Id = Watchpoints.WpointId.ToString();
                return _Id;
            }
            set => Set(ref _Id, value);
        }

        /// <summary>
        /// Номер точки наблюдения
        /// </summary>
        private string _wnumber;
        public string WNumber
        {
            get
            {
                _wnumber = Watchpoints.WpointNumber.ToString();
                return _wnumber;
            }
            set => Set(ref _wnumber, value);
        }

        private DateTime? _wDateStart;
        public DateTime? WDateStart
        {
            get {
                _wDateStart = new DateTime(Watchpoints.WpointDateAdd.Value.Year, Watchpoints.WpointDateAdd.Value.Month, Watchpoints.WpointDateAdd.Value.Day);
                return _wDateStart; }
            set =>Set(ref _wDateStart, value); 
        }

        private string _wnote;

        public string Wnote
        {
            get {
                _wnote = Watchpoints.WpointNote;
                return _wnote; }
            set =>Set(ref _wnote , value); 
        }

        private string _wlocation;

        public string Wlocation
        {
            get {
                _wlocation = Watchpoints.WpointLocation;
                return _wlocation; }
            set=>Set(ref _wlocation ,value);
        }



        private Watchpoint _watchpoints;
        private Watchpoint Watchpoints
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.Watchpoints.Where(w => w.WpointId == PassedParameter).ToList();

                    foreach (var item in data)
                    {
                        _watchpoints = new Watchpoint
                        {
                            WpointId = item.WpointId,
                            Route = item.Route,
                            WpointType = item.WpointType,
                            WpointNumber = item.WpointNumber,
                            WpointLocation = item.WpointLocation,
                            WpointDateAdd = item.WpointDateAdd,
                            WpointNote = item.WpointNote,
                            FUser = item.FUser,
                            WpointIndLandscape = item.WpointIndLandscape,

                        };
                    }
                    return _watchpoints;


                }
            }
            set => Set(ref _watchpoints, value);
        }



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
