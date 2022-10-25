using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Model;
using GeocomplexCore.Service;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class InfoPointObservationViewModel : ViewModel, INavigatedToAware
    {
        #region Параметры/Parametrs

        private NavigationManager navigationmaneger;
        private ConverterCordinatsService converter;


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
            get
            {
                _wDateStart = new DateTime(Watchpoints.WpointDateAdd.Value.Year, Watchpoints.WpointDateAdd.Value.Month, Watchpoints.WpointDateAdd.Value.Day);
                return _wDateStart;
            }
            set => Set(ref _wDateStart, value);
        }

        private string _wnote;

        public string Wnote
        {
            get
            {
                _wnote = Watchpoints.WpointNote;
                return _wnote;
            }
            set => Set(ref _wnote, value);
        }

        private string _wlocation;
        public string Wlocation
        {
            get
            {
                _wlocation = Watchpoints.WpointLocation;
                return _wlocation;
            }
            set => Set(ref _wlocation, value);
        }

        //долгота
        private string[] _longitude;
        public string[] Longitude
        {
            get { return _longitude; }
            set => Set(ref _longitude, value);
        }

        //Широта
        private string[] _width;
        public string[] Width
        {
            get { return _width; }
            set => Set(ref _width, value);

        }

        // Z точка или абс отметка
        private double? _pointZ;
        public double? PointZ
        {
            get {
                foreach (var item in Watchpoints.WpointCoordinates)
                {
                    _pointZ = item.WpCoordinatesZ;
                }
              
                return _pointZ; }
            set { _pointZ = value; }
        }


        private Watchpoint _watchpoints;
        private Watchpoint Watchpoints
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.Watchpoints
                        .Where(w => w.WpointId == PassedParameter)
                        .Include(c => c.WpointCoordinates).ToList();

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
                            WpointCoordinates = item.WpointCoordinates,


                        };
                    }
                    return _watchpoints;


                }
            }
            set => Set(ref _watchpoints, value);
        }

        /// <summary>
        /// Список формата
        /// </summary>
        private ObservableCollection<FormatCoordinatModel> _formatCoordinat;
        public ObservableCollection<FormatCoordinatModel> FormatCoordinat { get => _formatCoordinat; set => Set(ref _formatCoordinat, value); }

        /// <summary>
        /// Выбранный формат
        /// </summary>
        private FormatCoordinatModel _formatordSelected;
        public FormatCoordinatModel FormatordSelected
        {
            get { return _formatordSelected; }
            set
            {
                _formatordSelected = value;
                OnPropertyChanged("FormatordSelected");
                FormatingCoord();
            }
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

        public void FormatingCoord()
        {
            switch (FormatordSelected.Name)
            {
                case "Десятичная":

                    break;

                case "Градусы,минуты,секунды":

                    converter = new ConverterCordinatsService();
                    string l = "", w = "";
                    foreach (var item in Watchpoints.WpointCoordinates)
                    {
                        l = converter.FormatingDecimal(item.WpCoordinatesX);
                        w = converter.FormatingDecimal(item.WpCoordinatesY);
                    }

                    l = l.Replace("°", "").Replace("'", "");
                    w = w.Replace("°", "").Replace("'", "");
                    Longitude = l.Split(new char[] { ' ' });
                    Width = w.Split(new char[] { ' ' });

                    break;

            }
        }
        #endregion
        // ---------------------------------------------------------------------------------------------------------------------



        public InfoPointObservationViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;

            FormatCoordinat = new ObservableCollection<FormatCoordinatModel>()
            {
                new FormatCoordinatModel() { Name = "Десятичная" },
                new FormatCoordinatModel() { Name = "Градусы,минуты,секунды" }
            };

        }
    }
}
