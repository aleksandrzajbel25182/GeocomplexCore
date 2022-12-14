using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.DAL;
using GeocomplexCore.DAL.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.Model.Coordinat;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
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

        #region Функции/Fuction

        /// <summary>
        /// Форматирование координат
        /// </summary>
        private void FormatingCoord()
        {
            switch (FormatordSelected.Name)
            {
                case "Десятичная":
                    DPointX = "X";
                    DPointY = "Y";
                    DPointZ = "Z";
                    CollectionDataCoordinat = CollectionViewSource.GetDefaultView(LoadDistrcit());
                    break;

                case "Градусы,минуты,секунды":
                    converter = new ConverterCordinatsService(LoadDistrcit());
                    converter.ConverterDecimal();
                    _coordinatModels = new ObservableCollection<CoordinatModel>(converter.dataCoordinats);
                    CollectionDataCoordinat = CollectionViewSource.GetDefaultView(CooordinatModels);
                    DPointX = "Долгота";
                    DPointY = "Широта";
                    DPointZ = "Абс. отм";
                    break;
            }
        }

        /// <summary>
        /// Загрузка с база данных коориднаты участка
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<CoordinatModel> LoadDistrcit()
        {
            using (GeocomplexContext db = new())
            {
                var data = db.DistrictPoints.Where(r => r.IdDistrict == PassedParameter).ToList();
                _coordinatModels = new ObservableCollection<CoordinatModel>();
                foreach (var item in data)
                {
                    _coordinatModels.Add(new CoordinatModel
                    {
                        ID = item.IdDisctrictPoint,
                        PointX_Longitude = item.DisctrictPointX.ToString(),
                        PointY_Width = item.DisctrictPointY.ToString(),
                        PointZ = item.DisctrictPointZ
                    });
                }
                return _coordinatModels;
            }
        }

        /// <summary>
        /// Фильтрация 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterByName(object obj)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var objProject = obj as Route;
                return objProject != null && objProject.RouteName.Contains(TextToFilter);
            }
            return true;

        }

        #endregion




        #region Parametrs/Параметры

        private readonly NavigationManager _navigationmaneger;
        // Созадние экземлпяра класса сервиса-конвертера
        ConverterCordinatsService converter;


        /// <summary>
        /// Заголовок для таблицы, X
        /// </summary>
        private string _dPointX;
        public string DPointX
        {
            get { return _dPointX; }
            set => Set(ref _dPointX, value);
        }
        /// <summary>
        /// Заголовок для таблицы, Y
        /// </summary>
        private string _dPointY;
        public string DPointY
        {
            get { return _dPointY; }
            set => Set(ref _dPointY, value);
        }
        /// <summary>
        /// Заголовок для таблицы, Z
        /// </summary>
        private string _dPointZ;
        public string DPointZ
        {
            get { return _dPointZ; }
            set => Set(ref _dPointZ, value);
        }


        /// <summary>
        /// Коллецкция для координат
        /// </summary>
        private ICollectionView? _collectiondataCoordinat;
        public ICollectionView? CollectionDataCoordinat { get => _collectiondataCoordinat; set => Set(ref _collectiondataCoordinat, value); }

        /// <summary>
        /// Коллецкция маршрутов для фильтрации
        /// </summary>
        private ICollectionView? _collectiondata;
        public ICollectionView? CollectionData { get { _collectiondata = CollectionViewSource.GetDefaultView(DatacolRouDistcrit); return _collectiondata; } set => Set(ref _collectiondata, value); }

        /// <summary>
        /// Коллекция координат
        /// </summary>
        private ObservableCollection<CoordinatModel> _coordinatModels;
        public ObservableCollection<CoordinatModel> CooordinatModels { get => _coordinatModels; set => Set(ref _coordinatModels, value); }

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
        /// Формат координат десячитный формат/ градусы, минуты, секунды
        /// </summary>
        private ObservableCollection<FormatCoordinatModel> _formatCoordinat;
        public ObservableCollection<FormatCoordinatModel> FormatCoordinat { get => _formatCoordinat; set => Set(ref _formatCoordinat, value); }


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
                    GlobalSet.NameDis = PassedParameter;
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


        /// <summary>
        /// Поле для ввода текста фильтрации 
        /// </summary>
        private string? _textToFilter;
        public string? TextToFilter
        {
            get => _textToFilter;
            set
            {
                _textToFilter = value;
                OnPropertyChanged("TextToFilter");
                // Проводим фильтрацию
                CollectionData.Filter = FilterByName;
            }
        }

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
                new FormatCoordinatModel() { Name = "Градусы,минуты,секунды" }
            };

            DPointX = "Х";
            DPointY = "Y";
            DPointZ = "Z";

        }
    }
}
