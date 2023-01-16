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
        #region Parametrs/Параметры

        private readonly NavigationManager _navigationmaneger;
        // Созадние экземлпяра класса сервиса-конвертера
        ConverterCordinatsService converter;
        GeocomplexContext db = new GeocomplexContext();

        #region Title таблиц точек координат участка      
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
        #endregion 

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


        private ObservableCollection<CoordinatModel> _coordinatModels;
        /// <summary>
        /// Коллекция координат
        /// </summary>
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
            get => _dataWatchpoint;
            set => Set(ref _dataWatchpoint, value);
        }

        private Watchpoint? _selecteditem;
        /// <summary>
        /// Выбранный элемент в коллекиции "ТОЧКИ НАБЛЮДЕНИЯ"
        /// </summary>
        public Watchpoint? SelecetedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                OnPropertyChanged("SelecetedItem");
            }

        }


        private string? _namedistrict;
        /// <summary>
        /// Наименование участка
        /// </summary>
        public string? NameDiscrict
        {
            get => _namedistrict;
            set => Set(ref _namedistrict, value);
        }

       
        private ObservableCollection<Route> _datacolRouDistcrit = new ObservableCollection<Route>();
        /// <summary>
        /// Колекция маршрутов
        /// </summary>
        public ObservableCollection<Route> DatacolRouDistcrit
        {
            get => _datacolRouDistcrit;
            set => Set(ref _datacolRouDistcrit, value);
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

        #region Функции/Fuction

        /// <summary>
        /// Принимаем переменную переданную из другой страницы
        /// </summary>
        /// <param name="arg"></param>
        public void OnNavigatedTo(object arg)
        {
            if (!(arg is int))
                return;

            PassedParameter = (int)arg;
            GetCoolectionWatchpoint();
            GetCoolectionRoute();

            //Название участка и необходимы параметры 
            NameDiscrict = db.Districts?.FirstOrDefault(r => r.IdDistrict == PassedParameter).NameDistrict;
            LocatorStatic.Data.PageHeader = $"Участок: {_namedistrict}";
            GlobalSet.NameDis = PassedParameter;
        }

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
            CooordinatModels = _coordinatModels;
            return CooordinatModels;

        }

        /// <summary>
        /// Функция предназаченная для чтения из базы данных коллекции Маршрутов 
        /// </summary>
        /// <returns>Возвращаем DatacolRouDistcrit коллекцию маршрутов </returns>
        private ObservableCollection<Route> GetCoolectionRoute()
        {
            DatacolRouDistcrit = db.Routes.Where(r => r.IdDistrictNavigation.IdDistrict == PassedParameter).Include(us => us.User).AsNoTracking().ToObservableCollection();
            return DatacolRouDistcrit;
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

        /// <summary>
        /// /Функция предназаченная для чтения из базы данных коллекции точек наблюдения 
        /// </summary>
        /// <returns>Возвращаем коллекцию ObservableCollection<Watchpoint> точек наблюдения </returns>
        private ObservableCollection<Watchpoint> GetCoolectionWatchpoint()
        {
            _dataWatchpoint = db.Watchpoints.Where(r => r.Route.IdDistrictNavigation.IdDistrict == PassedParameter)
                .Include(us => us.FUser)
                .ThenInclude(rout => rout.Routes).ToObservableCollection();
            DataWatchpoint = _dataWatchpoint;
            return DataWatchpoint;
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
