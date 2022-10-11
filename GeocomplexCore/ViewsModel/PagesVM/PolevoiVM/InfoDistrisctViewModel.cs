using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class InfoDistrisctViewModel : ViewModel, INavigatedToAware
    {
        private readonly NavigationManager _navigationmaneger;

        private ObservableCollection<FormatCoordinatModel> _formatCoordinat;
        public ObservableCollection<FormatCoordinatModel> FormatCoordinat { get => _formatCoordinat; set => Set(ref _formatCoordinat, value); }

        #region Parametrs/Параметры
        /// <summary>
        /// Коллеция "ТОЧКИ НАБЛЮДЕНИЯ"
        /// </summary>
        private ObservableCollection<Watchpoint> _dataWatchpoint = new ObservableCollection<Watchpoint>();
        public ObservableCollection<Watchpoint> DataWatchpoint
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.Watchpoints.Where(r => r.Route.IdDistrictNavigation.IdDistrict == PassedParameter)
                        .Include(us => us.FUser)
                        .ThenInclude(rout => rout.Routes).ToList();

                    foreach (var item in data)
                    {
                        _dataWatchpoint.Add(new Watchpoint
                        {
                            WpointId = item.WpointId,
                            Route = item.Route,
                            WpointType = item.WpointType,
                            WpointNumber = item.WpointNumber,
                            WpointLocation = item.WpointLocation,
                            WpointDateAdd = item.WpointDateAdd,
                            WpointNote = item.WpointNote,
                            FUser = item.FUser,
                            WpointIndLandscape = item.WpointIndLandscape
                        });
                    }
                    return _dataWatchpoint;
                }
            }
            set { _dataWatchpoint = value; }
        }

        /// <summary>
        /// Выбранный элемент в коллекиции "ТОЧКИ НАБЛЮДЕНИЯ"
        /// </summary>
        private Watchpoint? _selecteditem;
        public Watchpoint? SelecetedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                OnPropertyChanged("SelecetedItem");
            }

        }



        /// <summary>
        /// Наименование участка
        /// </summary>
        private string _namedistrict;

        public string NameDiscrict
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    _namedistrict = db.Districts
                       .FirstOrDefault(r => r.IdDistrict == PassedParameter).NameDistrict.ToString();

                    LocatorStatic.Data.PageHeader = $"Участок: {_namedistrict}";
                    return _namedistrict;


                }
            }
            set => Set(ref _namedistrict, value);
        }




        /// <summary>
        /// Колекция маршрутов
        /// </summary>
        private ObservableCollection<Route> _datacolRouDistcrit = new ObservableCollection<Route>();
        public ObservableCollection<Route> DatacolRouDistcrit
        {

            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.Routes.Where(r => r.IdDistrictNavigation.IdDistrict == PassedParameter).Include(us => us.User).ToList();

                    foreach (var item in data)
                    {
                        _datacolRouDistcrit.Add(new Route
                        {
                            RouteId = item.RouteId,
                            RouteName = item.RouteName,
                            User = item.User,
                            RouteData = item.RouteData

                        });
                    }
                    return _datacolRouDistcrit;

                }
            }
            set
            {
                _datacolRouDistcrit = value;
                OnPropertyChanged("DatacolRouDistcrit");
            }
        }

        /// <summary>
        /// Координаты участка
        /// </summary>
        private ObservableCollection<DistrictPoint> _dataDistrictPoint = new();
        public ObservableCollection<DistrictPoint> DataDistrictPoint
        {
            get
            {
                using (GeocomplexContext db = new())
                {
                    var data = db.DistrictPoints.Where(r => r.IdDistrict == PassedParameter).ToList();

                    foreach (var item in data)
                    {
                        _dataDistrictPoint.Add(new DistrictPoint
                        {
                            IdDisctrictPoint = item.IdDisctrictPoint,
                            DisctrictPointX = item.DisctrictPointX,
                            DisctrictPointY = item.DisctrictPointY,
                            DisctrictPointZ = item.DisctrictPointZ

                        });
                    }

                    return _dataDistrictPoint;
                }
            }
            set
            {
                _dataDistrictPoint = value;
                OnPropertyChanged("DataDistrictPoint");
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

        //private ICollectionView? _collectiondata;
        //public ICollectionView? CollectionData { get => _collectiondata; set => Set(ref _collectiondata, value); }


        #endregion




        #region Commands/Команды



        #region Команда НАЗАД
        /// <summary>
        /// Команда НАЗАД
        /// </summary>
        public ICommand BackNavigateCommand { get; }
        private bool BackNavigateCommandExecute(object p) => true;
        private void OnBackNavigateCommandExcuted(object p)
        {
            LocatorStatic.Data.PageHeader = $"Проекты";
            _navigationmaneger.Navigate("ProjectPage");
        }
        #endregion

        public ICommand GoInfoPointOBCommand { get; }

        private bool CanGoInfoPointOBCommandExecute(object p) => true;

        private void OnGoInfoPointOBCommandExcuted(object p)
        {
            _navigationmaneger.Navigate("InfoPointObservation", SelecetedItem.WpointId);

        }



        #endregion



        public InfoDistrisctViewModel(NavigationManager navigationmaneger)
        {
            _navigationmaneger = navigationmaneger;

            BackNavigateCommand = new LamdaCommand(OnBackNavigateCommandExcuted, BackNavigateCommandExecute);
            GoInfoPointOBCommand = new LamdaCommand(OnGoInfoPointOBCommandExcuted, CanGoInfoPointOBCommandExecute);

            FormatCoordinat = new ObservableCollection<FormatCoordinatModel>()
            {
                new FormatCoordinatModel() { Name = "Десятичная" },
                new FormatCoordinatModel() { Name = "Градусы,минуты,секнды" }
            };




        }
    }
}
