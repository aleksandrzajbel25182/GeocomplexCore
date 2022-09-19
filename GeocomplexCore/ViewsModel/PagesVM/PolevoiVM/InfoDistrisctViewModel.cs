using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
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

        #region Параметры



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





        public ICommand LoadCommand { get; }

        private bool CanLoadCommandExecute(object p) => true;

        private void OnLoadCommandExcuted(object p)
        {

            using (GeocomplexContext db = new GeocomplexContext())
            {

                //_datacolRouDistcrit = db.Routes.Where(u => u.IdDistrictNavigation.IdDistrict == PassedParameter).ToObservableCollection();

                DatacolRouDistcrit = db.Routes.Where(u => u.IdDistrictNavigation.IdDistrict == Convert.ToInt32(PassedParameter)).ToObservableCollection();



            }
        }


        public InfoDistrisctViewModel(NavigationManager navigationmaneger)
        {
            _navigationmaneger = navigationmaneger;

            LoadCommand = new LamdaCommand(OnLoadCommandExcuted, CanLoadCommandExecute);
            LocatorStatic.Data.PageHeader = $"Участок: {_namedistrict}";

        }
    }
}
