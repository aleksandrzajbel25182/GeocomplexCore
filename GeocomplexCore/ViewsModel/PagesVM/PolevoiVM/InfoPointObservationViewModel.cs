using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.DAL;
using GeocomplexCore.DAL.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
/* 
 В комментариях есть сокращение ТН - точка наблюдения; 
                                БД - база данных
 */
namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class InfoPointObservationViewModel : ViewModel, INavigatedToAware
    {
        #region Параметры/Parametrs

        private NavigationManager navigationmaneger;
        private ConverterCordinatsService converter;
        private GeocomplexContext db;
        private Ground? ground;
        private Egp? egp;
        private Watchpoint? watchpoint;
        private Techobject? techobject;
        private Plant? plant;
        private Surfacewater? surfacewater;


        #region Свойства для видимости элементов


        /// <summary>
        /// Видимость градусы минуты секунды
        /// </summary>
        private bool _showCoordinatDegMinsSek;
        public bool ShowCoordinatDegMinsSek
        {
            get { return _showCoordinatDegMinsSek; }
            set => Set(ref _showCoordinatDegMinsSek, value);
        }

        /// <summary>
        /// Видимость градусы минуты секунды
        /// </summary>
        private bool _showCoordinatDeсimal;
        public bool ShowCoordinatDeсimal
        {
            get { return _showCoordinatDeсimal; }
            set => Set(ref _showCoordinatDeсimal, value);
        }

        #endregion

        /// <summary>
        /// Переменная хранимая данные переданные из другой страницы
        /// </summary>
        private int _passedParameter;
        public int PassedParameter
        {
            get => _passedParameter;
            set => Set(ref _passedParameter, value);
        }

        #region Точка наблюдения/Основная информация


        /// <summary>
        /// Маршрут по которому делалась ТН
        /// </summary>
        private string _wRoute;
        public string WRoute
        {
            get
            {
                if (watchpoint.Route.RouteName is not null)
                {
                    _wRoute = watchpoint.Route.RouteName;
                    return _wRoute;
                }
                return _wRoute;
            }
            set => Set(ref _wRoute, value);
        }

        /// <summary>
        /// Номер точки наблюдения
        /// </summary>
        private string _wnumber;
        public string WNumber
        {
            get
            {
                if (watchpoint.WpointNumber is not null)
                {
                    _wnumber = watchpoint.WpointNumber;
                    return _wnumber;
                }
                return _wnumber;
            }
            set => Set(ref _wnumber, value);
        }

        /// <summary>
        /// Дата добавления ТН
        /// </summary>
        private DateTime? _wDateStart;
        public DateTime? WDateStart
        {
            get
            {
                if (watchpoint.WpointDateAdd is not null)
                {
                    _wDateStart = new DateTime(watchpoint.WpointDateAdd.Value.Year, watchpoint.WpointDateAdd.Value.Month, watchpoint.WpointDateAdd.Value.Day);
                    return _wDateStart;
                }
                return _wDateStart;
            }
            set => Set(ref _wDateStart, value);
        }

        /// <summary>
        /// ПРимечания/Описание
        /// </summary>
        private string _wnote;
        public string Wnote
        {
            get
            {
                if (watchpoint.WpointNote is not null)
                {
                    _wnote = watchpoint.WpointNote;
                    return _wnote;
                }
                return _wnote;
            }
            set => Set(ref _wnote, value);
        }

        /// <summary>
        /// Местоположения ТН
        /// </summary>
        private string _wlocation;
        public string Wlocation
        {
            get
            {
                if (watchpoint.WpointLocation is not null)
                {
                    _wlocation = watchpoint.WpointLocation;
                    return _wlocation;
                }
                return "Нет сведений";
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

        //Кординыты X
        private double? _wpointX;
        public double? WpointX
        {
            get
            {
                if (watchpoint.WpointCoordinates is not null)
                {
                    foreach (var item in watchpoint.WpointCoordinates)
                    {
                        _wpointX = item.WpCoordinatesX;
                        return _wpointX;
                    }
                }
                return _wpointX;
            }
            set { _wpointX = value; }
        }
        //Кординыты Y
        private double? _wpointY;
        public double? WpointY
        {
            get
            {
                if (watchpoint.WpointLocation is not null)
                {
                    foreach (var item in watchpoint.WpointCoordinates)
                    {
                        _wpointY = item.WpCoordinatesY;
                    }
                    return _wpointY;
                }
                return _wpointY;
            }
            set { _wpointY = value; }
        }

        // Z точка или абс отметка
        private double? _pointZ;
        public double? PointZ
        {
            get
            {
                if (watchpoint.WpointLocation is not null)
                {
                    foreach (var item in watchpoint.WpointCoordinates)
                    {
                        _pointZ = item.WpCoordinatesZ;
                    }
                    return _pointZ;
                }
                return _pointZ;
            }
            set { _pointZ = value; }
        }

        #region Коллекции "Геоморфологическая колонка" 

        #region Список Форма рельефа
        private ObservableCollection<GuideFormareliefa> _formareliefa = new ObservableCollection<GuideFormareliefa>();
        /// <summary>
        /// Форма рельефа
        /// </summary>
        public ObservableCollection<GuideFormareliefa> Formareliefa
        {
            get => _formareliefa;
            set { _formareliefa = value; }
        }
        /// <summary>
        /// Выбранная Форма рельефа
        /// </summary>
        public GuideFormareliefa SelectedFormarelirfa { get; set; }
        #endregion

        #region Cписок Тип рельефа 
        private ObservableCollection<GuideTypereliefa> _typereliefa = new ObservableCollection<GuideTypereliefa>();
        /// <summary>
        /// Список рельефа
        /// </summary>
        public ObservableCollection<GuideTypereliefa> Typereliefa
        {
            get => _typereliefa;
            set { _typereliefa = value; }
        }
        /// <summary>
        /// Выбранный Список рельефа
        /// </summary>
        public GuideTypereliefa SelectedTypereliefa { get; set; }
        #endregion

        #region Список Подтип рельефа
        private ObservableCollection<GuideSubtypereliefa> _sybtypereliefa = new ObservableCollection<GuideSubtypereliefa>();
        /// <summary>
        /// Подтип рельефа
        /// </summary>
        public ObservableCollection<GuideSubtypereliefa> Sybtypereliefa
        {
            get => _sybtypereliefa;
            set { _sybtypereliefa = value; }
        }
        /// <summary>
        /// Выбранный Подтип рельефа
        /// </summary>
        public GuideSubtypereliefa SelectedSybtypereliefa { get; set; }
        #endregion

        #region Список Высотность рельефа
        private ObservableCollection<GuideHeightreliefa> _heightreliefa = new ObservableCollection<GuideHeightreliefa>();
        /// <summary>
        /// Высотность рельефа
        /// </summary>
        public ObservableCollection<GuideHeightreliefa> Heightreliefa
        {
            get => _heightreliefa;
            set { _ = value; }
        }
        /// <summary>
        /// Выбранная Высотность рельефа
        /// </summary>
        public GuideHeightreliefa SelectedHeightreliefa { get; set; }
        #endregion

        #region Список Экспозиция
        private ObservableCollection<GuideSprexposition> _exposition = new ObservableCollection<GuideSprexposition>();
        /// <summary>
        /// Экспозиция
        /// </summary>
        public ObservableCollection<GuideSprexposition> Exposition
        {
            get => _exposition;
            set { _exposition = value; }
        }
        /// <summary>
        /// Выбранная Экспозиция
        /// </summary>
        public GuideSprexposition SelectedExposition { get; set; }
        #endregion

        #region Список Крутизна склона
        private ObservableCollection<GuideSlope> _slope = new ObservableCollection<GuideSlope>();
        /// <summary>
        /// Крутизна склона
        /// </summary>
        public ObservableCollection<GuideSlope> Slope
        {
            get => _slope;
            set { _slope = value; }
        }
        /// <summary>
        /// Выбранная Крутизна склона
        /// </summary>
        public GuideSlope SelectedSlope { get; set; }
        #endregion

        #region Список Форма речной долины
        private ObservableCollection<GuideFormariver> _formariver = new ObservableCollection<GuideFormariver>();
        /// <summary>
        /// Форма речной долины
        /// </summary>
        public ObservableCollection<GuideFormariver> Formariver
        {
            get => _formariver;
            set { _formariver = value; }
        }
        /// <summary>
        /// Выбранная Форма речной долины
        /// </summary>
        public GuideFormariver SelectedFormariver { get; set; }
        #endregion
        #endregion

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
        #endregion

        #region Свойства ПОРОДА и почва


        #region Список Породы
        private ObservableCollection<GuideBreed> _groundBreed = new ObservableCollection<GuideBreed>();
        /// <summary>
        /// Список породы
        /// </summary>
        public ObservableCollection<GuideBreed> GroundBreed
        {
            get => _groundBreed;

            set { _groundBreed = value; }
        }
        /// <summary>
        /// Выбранный список породы
        /// </summary>
        public GuideBreed SelectedGroundBreed { get; set; }
        #endregion

        #region Список Оттенка
        private ObservableCollection<GuideColor> _groundDopcolor = new ObservableCollection<GuideColor>();
        public ObservableCollection<GuideColor> GroundDopcolor
        {
            get => _groundDopcolor;
            set { _groundDopcolor = value; }
        }
        /// <summary>
        /// Выбранный Оттенок
        /// </summary>
        public GuideColor SelectedGroundDopcolor { get; set; }
        #endregion


        #region Список Цвета
        private ObservableCollection<GuideColor> _groundColor = new ObservableCollection<GuideColor>();
        /// <summary>
        /// Список цвета
        /// </summary>
        public ObservableCollection<GuideColor> GroundColor
        {
            get => _groundColor;
            set { _groundColor = value; }
        }
        public GuideColor SelectedGroundColor { get; set; }
        #endregion

        #region Порода Дата
        /// <summary>
        /// Порода Дата
        /// </summary>
        private DateTime? _groundData;
        public DateTime? GroundData
        {
            get
            {
                if (ground?.DataGround is not null)
                {
                    _groundData = new DateTime(ground.DataGround.Value.Year, ground.DataGround.Value.Month, ground.DataGround.Value.Day);
                    return _groundData;
                }
                return _groundData;
            }
            set => Set(ref _groundData, value);
        }
        #endregion


        #region Почва пользователь
        /// <summary>
        /// Почва пользователь
        /// </summary>
        private string _groundUserName;
        public string GroundUserName
        {
            get
            {
                if (ground?.FUser.UserName is not null)
                {
                    _groundUserName = ground.FUser.UserName;
                    return _groundUserName;
                }
                return _groundUserName;
            }
            set => Set(ref _groundUserName, value);
        }
        #endregion

        #region Почва Описание
        /// <summary>
        /// Почва Описание
        /// </summary>
        private string _groundDescription;
        public string GroundDescription
        {
            get
            {
                if (ground?.DescriptionGround is not null)
                {
                    _groundDescription = ground.DescriptionGround;
                    return _groundDescription;
                }
                return _groundDescription;
            }
            set => Set(ref _groundDescription, value);
        }
        #endregion

        #region Интервал ОТ
        /// <summary>
        /// Интервал ОТ
        /// </summary>
        private double? _groundFrom;
        public double? GroundFrom
        {
            get
            {
                if (ground?.FromGround is not null)
                {
                    _groundFrom = ground.FromGround;
                    return _groundFrom;
                }
                return _groundFrom;

            }
            set => Set(ref _groundFrom, value);
        }
        #endregion

        #region Интервал ДО
        /// <summary>
        /// Интервал ДО
        /// </summary>
        private double? _groundTo;
        public double? GroundTo
        {
            get
            {
                if (ground?.ToGround is not null)
                {
                    _groundTo = ground.ToGround;
                    return _groundTo;
                }
                return _groundTo;
            }
            set => Set(ref _groundTo, value);
        }
        #endregion


        #endregion

        #region ЭГП


        private ObservableCollection<GuideGroupprocce> _egpgroupprocess = new ObservableCollection<GuideGroupprocce>();
        /// <summary>
        /// /Группа процессов
        /// </summary>
        public ObservableCollection<GuideGroupprocce> Egpgroupprocess
        {
            get => _egpgroupprocess;
            set { _egpgroupprocess = value; }
        }
        /// <summary>
        /// Выбранная группа процесса эгп
        /// </summary>
        public GuideGroupprocce SelectedGroupprocce { get; set; }


        private ObservableCollection<GuideTypeprocess> _egptypeprocess = new ObservableCollection<GuideTypeprocess>();
        /// <summary>
        /// ЭГП тип Процесса
        /// </summary>
        public ObservableCollection<GuideTypeprocess> Egptypeprocess
        {
            get => _egptypeprocess;
            set { _egptypeprocess = value; }
        }
        /// <summary>
        /// Выбранный тип процесса
        /// </summary>
        public GuideTypeprocess SelectedTypeprocess { get; set; }


        private ObservableCollection<GuideEgpelement> _egpElement = new ObservableCollection<GuideEgpelement>();
        /// <summary>
        /// Вторичный элемент ЭГП
        /// </summary>
        public ObservableCollection<GuideEgpelement> EgpElement
        {
            get => _egpElement;
            set { _egpElement = value; }
        }
        /// <summary>
        /// Выбранный вторичный элемент
        /// </summary>
        public GuideEgpelement SelectedEgpElement { get; set; }

        /// <summary>
        /// Дата ЭГП
        /// </summary>
        private DateTime? _egpDate;
        public DateTime? Egpdate
        {
            get
            {
                if (egp?.DataEgp is not null)
                {
                    _egpDate = new DateTime(egp.DataEgp.Value.Year, egp.DataEgp.Value.Month, egp.DataEgp.Value.Day);
                    return _egpDate;
                }
                return _egpDate;
            }
            set => Set(ref _egpDate, value);
        }

        private string _egpUserName;
        public string EgpUserName
        {
            get
            {
                if (egp?.FUser.UserName is not null)
                {
                    _egpUserName = egp.FUser.UserName;
                    return _egpUserName;
                }
                return _egpUserName;
            }
            set => Set(ref _egpUserName, value);

        }

        /// <summary>
        /// Глубина ЭГП
        /// </summary>
        private double? _egpDeep;
        public double? EgpDeep
        {
            get
            {
                if (egp?.EgpDeep is not null)
                {
                    _egpDeep = egp.EgpDeep;
                    return _egpDeep;
                }
                return _egpDeep;
            }
            set { _egpDeep = value; }
        }

        /// <summary>
        /// Ширина ЭГП
        /// </summary>
        private double? _egpWidth;
        public double? EgpWidth
        {
            get
            {
                if (egp?.EgpWidth is not null)
                {
                    _egpWidth = egp.EgpWidth;
                    return _egpWidth;
                }
                return _egpWidth;
            }
            set { _egpWidth = value; }
        }

        /// <summary>
        /// Протяженность
        /// </summary>
        private double? _egpLength;
        public double? EgpLength
        {
            get
            {
                if (egp?.EgpLength is not null)
                {
                    _egpLength = egp.EgpLength;
                    return _egpLength;
                }
                return _egpLength;
            }
            set { _egpLength = value; }
        }

        /// <summary>
        /// Объем
        /// </summary>
        private double? _egpVolume;
        public double? EgpVolume
        {
            get
            {
                if (egp?.EgpVolume is not null)
                {
                    _egpVolume = egp.EgpVolume;
                    return _egpVolume;
                }
                return _egpVolume;
            }
            set { _egpVolume = value; }
        }

        /// <summary>
        /// Площадь ЭГП
        /// </summary>
        private double? _egpArea;
        public double? EgpArea
        {
            get
            {
                if (egp?.EgpArea is not null)
                {
                    _egpArea = egp.EgpArea;
                    return _egpArea;
                }
                return _egpArea;
            }
            set { _egpArea = value; }
        }

        /// <summary>
        /// Скорость эгп
        /// </summary>
        private double? _egpSpeed;
        public double? EgpSpeed
        {
            get
            {
                if (egp?.EgpSpeed is not null)
                {
                    _egpSpeed = egp.EgpSpeed;
                    return _egpSpeed;
                }
                return _egpSpeed;
            }
            set { _egpSpeed = value; }
        }

        /// <summary>
        /// Дополнительные сведения/Описание ЭГП
        /// </summary>
        private string? _egpDescription;
        public string? EgpDescription
        {
            get
            {
                if (egp?.EgpDescription is not null)
                {
                    _egpDescription = egp.EgpDescription;
                    return _egpDescription;
                }
                return _egpDescription;
            }
            set { _egpDescription = value; }
        }


        #endregion

        #region Техногенные объекты


        /// <summary>
        /// Техногенный объект: Наименование
        /// </summary>
        private string? _techogjName;
        public string? TechogjName
        {
            get
            {
                if (techobject?.TechobjName is not null)
                {
                    _techogjName = techobject.TechobjName;
                    return _techogjName;
                }
                return _techogjName;
            }
            set => Set(ref _techogjName, value);
        }

        /// <summary>
        /// Техногенный объект: Источник техногенного воздействия
        /// </summary>
        private string? _techobjSource;
        public string? TechobjSource
        {
            get
            {
                if (techobject?.TechobjSource is not null)
                {
                    _techobjSource = techobject.TechobjSource;
                    return _techobjSource;
                }
                return _techobjSource;
            }
            set => Set(ref _techobjSource, value);
        }

        /// <summary>
        /// Техногенный объект: Лицензия
        /// </summary>
        private string? _techobjLicense;
        public string? TechobjLicense
        {
            get
            {
                if (techobject?.TechobjLicense is not null)
                {
                    _techobjLicense = techobject.TechobjLicense;
                    return _techobjLicense;
                }
                return _techobjLicense;
            }
            set => Set(ref _techobjLicense, value);
        }

        /// <summary>
        /// Техногенный объект: Наличие программы мониторинга качества воды
        /// </summary>
        private int? _techobjMonitoring;
        public int? TechobjMonitoring
        {
            get
            {
                if (techobject?.TechobjMonitoring is not null)
                {
                    _techobjMonitoring = techobject.TechobjMonitoring;
                    return _techobjMonitoring;
                }
                return _techobjMonitoring;
            }
            set => Set(ref _techobjMonitoring, value);
        }

        /// <summary>
        /// Техногенный объект: Наличие эксплуатационных скважин, родников
        /// </summary>
        private int? _techobjProducthole;
        public int? TechobjProducthole
        {
            get
            {
                if (techobject?.TechobjProducthole is not null)
                {
                    _techobjProducthole = techobject.TechobjProducthole;
                    return _techobjProducthole;
                }
                return _techobjProducthole;
            }
            set => Set(ref _techobjProducthole, value);
        }
        /// <summary>
        /// Техногенный объект: Наличие наблюдательных скважин
        /// </summary>
        private int? _techobjWatchhole;
        public int? TechobjWatchhole
        {
            get
            {
                if (techobject?.TechobjWatchhole is not null)
                {
                    _techobjWatchhole = techobject.TechobjWatchhole;
                    return _techobjWatchhole;
                }
                return _techobjWatchhole;
            }
            set => Set(ref _techobjWatchhole, value);
        }

        /// <summary>
        /// Техногенный объект: Примечание
        /// </summary>
        private string? _techobjDescription;
        public string? TechobjDescription
        {
            get
            {
                if (techobject?.TechobjDescription is not null)
                {
                    _techobjDescription = techobject.TechobjDescription;
                    return _techobjDescription;
                }
                return _techobjDescription;
            }
            set => Set(ref _techobjDescription, value);
        }
        #endregion

        #region Растительность


        private ObservableCollection<GuideForestDensity> _plantforestdensities = new ObservableCollection<GuideForestDensity>();
        /// <summary>
        /// Справочник Густота леса
        /// </summary>
        public ObservableCollection<GuideForestDensity> PlantForestDensities
        {
            get => _plantforestdensities;
            set { _plantforestdensities = value; }
        }
        /// <summary>
        /// Выбранная группа Густота леса
        /// </summary>
        public GuideForestDensity SelectedPlantForestDensities { get; set; }



        private ObservableCollection<GuideHeightUndergrowth> _plantheightundergrowt = new ObservableCollection<GuideHeightUndergrowth>();
        /// <summary>
        /// Справочник Высоты подроста
        /// </summary>
        public ObservableCollection<GuideHeightUndergrowth> PlantHeightUndergrowt
        {
            get => _plantheightundergrowt;
            set { _plantheightundergrowt = value; }
        }
        /// <summary>
        /// Выбранная группа Высоты подроста
        /// </summary>
        public GuideHeightUndergrowth SelectedPlantHeightUndergrowt { get; set; }




        private ObservableCollection<GuideProjcoverUndergrowth> _plantprojcoverundergrowth = new ObservableCollection<GuideProjcoverUndergrowth>();
        /// <summary>
        /// Справочник проективное покрытие подроста
        /// </summary>
        public ObservableCollection<GuideProjcoverUndergrowth> PlantProjcoverUndergrowth
        {
            get => _plantprojcoverundergrowth;
            set { _plantprojcoverundergrowth = value; }
        }
        /// <summary>
        /// Выбранная группа проективное покрытие подроста
        /// </summary>
        public GuideProjcoverUndergrowth SelectedPlantProjcoverUndergrowth { get; set; }



        private ObservableCollection<GuideDensityBush> _plantdensitybush = new ObservableCollection<GuideDensityBush>();
        /// <summary>
        /// Справочник Густота кустарников
        /// </summary>
        public ObservableCollection<GuideDensityBush> PlantDensityBush
        {
            get => _plantdensitybush;
            set { _plantdensitybush = value; }
        }
        /// <summary>
        /// Выбранная группа Густота кустарников
        /// </summary>
        public GuideDensityBush SelectedPlantDensityBush { get; set; }


        private ObservableCollection<Node> _pBush = new ObservableCollection<Node>();
        /// <summary>
        /// Кустарник
        /// </summary>
        public ObservableCollection<Node> PBush
        {
            get => _pBush;
            set => Set(ref _pBush, value);
        }


        private ObservableCollection<Node> _pStands = new ObservableCollection<Node>();
        /// <summary>
        /// Древостой
        /// </summary>
        public ObservableCollection<Node> PStands
        {
            get => _pStands;
            set => Set(ref _pStands, value);
        }

        private ObservableCollection<Node> _pSmallbush = new ObservableCollection<Node>();
        /// <summary>
        /// Кустарничек
        /// </summary>
        public ObservableCollection<Node> PSmallbush
        {
            get => _pSmallbush;
            set => Set(ref _pSmallbush, value);
        }


        private ObservableCollection<Node> _pUndergrowth = new ObservableCollection<Node>();
        /// <summary>
        /// Подрост
        /// </summary>
        public ObservableCollection<Node> PUndergrowth
        {
            get => _pUndergrowth;
            set => Set(ref _pUndergrowth, value);
        }

        private ObservableCollection<Node> _pGroundcoverh = new ObservableCollection<Node>();
        /// <summary>
        /// Напочвенный покров
        /// </summary>
        public ObservableCollection<Node> PGroundcoverh
        {
            get => _pGroundcoverh;
            set => Set(ref _pGroundcoverh, value);
        }

        /// <summary>
        /// Санитарное состояние
        /// </summary>
        private ObservableCollection<Node> _pSanitar = new ObservableCollection<Node>();
        public ObservableCollection<Node> PSanitar
        {
            get => _pSanitar;
            set => Set(ref _pSanitar, value);
        }



        private ObservableCollection<Node> _pHumanimpact = new ObservableCollection<Node>();
        /// <summary>
        /// Антропогенное воздействие
        /// </summary>
        public ObservableCollection<Node> PHumanimpact
        {
            get => _pHumanimpact;
            set => Set(ref _pHumanimpact, value);
        }

        private ObservableCollection<GuideProjcoverGroundcover> _prjGround = new ObservableCollection<GuideProjcoverGroundcover>();
        /// <summary>
        /// Проективное покрытие Напочвенного покрова
        /// </summary>
        public ObservableCollection<GuideProjcoverGroundcover> PrjGround
        {
            get => _prjGround;
            set { _prjGround = value; }
        }
        /// <summary>
        /// Выбранная группа Проективное покрытие Напочвенного покрова
        /// </summary>
        public GuideProjcoverGroundcover SelectedPlantPrjGround { get; set; }



        #endregion

        #region Поверехностные воды

        private string _swName;
        /// <summary>
        /// Поверхностные воды. Название 
        /// </summary>
        public string SwName
        {
            get
            {
                if (surfacewater?.SwName is not null)
                {
                    _swName = surfacewater.SwName;
                    return _swName;
                }
                return _swName;
            }
            set => Set(ref _swName, value);
        }

        private double? _swWidth;
        /// <summary>
        /// Поверхностные воды. Ширина русла
        /// </summary>
        public double? SwWidth
        {
            get
            {
                if (surfacewater?.SwWidth is not null)
                {
                    _swWidth = surfacewater.SwWidth;
                    return _swWidth;
                }
                return _swWidth;
            }
            set => Set(ref _swWidth, value);
        }

        private double? _swSpeedWater;
        /// <summary>
        /// Поверхностные воды. Скорость течения
        /// </summary>
        public double? SwSpeedWater
        {
            get
            {
                if (surfacewater?.SwSpeedwater is not null)
                {
                    _swSpeedWater = surfacewater.SwSpeedwater;
                    return _swSpeedWater;
                }
                return _swSpeedWater;
            }
            set => Set(ref _swSpeedWater, value);
        }

        private double? _swWaterTemp;
        /// <summary>
        /// Поверхностные воды. Температура воды
        /// </summary>
        public double? SwWaterTemp
        {
            get
            {
                if (surfacewater?.SwWatertemp is not null)
                {
                    _swWaterTemp = surfacewater.SwWatertemp;
                    return _swWaterTemp;
                }
                return _swWaterTemp;
            }
            set => Set(ref _swWaterTemp, value);
        }

        private double? _swAirTemp;
        /// <summary>
        /// Поверхностные воды. Температура воздуха
        /// </summary>
        public double? SwAirTemp
        {
            get
            {
                if (surfacewater?.SwAirtemp is not null)
                {
                    _swAirTemp = surfacewater.SwAirtemp;
                    return _swAirTemp;
                }
                return _swAirTemp;
            }
            set => Set(ref _swAirTemp, value);
        }

        private string? _swBloom;
        /// <summary>
        /// Поверхностные воды. Дополнительные параметры
        /// </summary>
        public string? SwBloom
        {
            get
            {
                if (surfacewater?.SwBloom is not null)
                {
                    _swBloom = surfacewater.SwBloom;
                    return _swBloom;
                }
                return _swBloom;
            }
            set => Set(ref _swBloom, value);
        }

        private double? _swWaterFlowRate;
        /// <summary>
        /// Поверхностные воды Расход потока м3/с
        /// </summary>
        public double? SwWaterFlowRate
        {
            get
            {
                if (surfacewater?.SwWaterFlowRate is not null)
                {
                    _swWaterFlowRate = surfacewater.SwWaterFlowRate;
                    return surfacewater.SwWaterFlowRate;
                }
                return _swWaterFlowRate;
            }
            set => Set(ref _swWaterFlowRate, value);
        }


        private ObservableCollection<GuideTypebottom> _swTypeBottom= new ObservableCollection<GuideTypebottom>();
        /// <summary>
        /// Справочник Тип дна
        /// </summary>
        public ObservableCollection<GuideTypebottom> SwTypeBottom
        {
            get => _swTypeBottom;
            set => Set(ref _swTypeBottom , value); 
        }
        /// <summary>
        /// Выбранная группа Типа дна
        /// </summary>
        public GuideTypebottom SelectedSurwaterTypebottom { get; set; }

        private ObservableCollection<GuideTastewater> _swTasteWater= new ObservableCollection<GuideTastewater>();
        /// <summary>
        /// Справочник Вкус воды
        /// </summary>
        public ObservableCollection<GuideTastewater> SwTasteWater
        {
            get => _swTasteWater;
            set => Set(ref _swTasteWater, value);
        }
        /// <summary>
        /// Выбранная группа Вкуса Воды
        /// </summary>
        public GuideTastewater SelectedSurwaterTastewater { get; set; }

        private ObservableCollection<GuideSmellwater> _swSmellWater = new ObservableCollection<GuideSmellwater>();
        /// <summary>
        /// Справочник Запахи воды
        /// </summary>
        public ObservableCollection<GuideSmellwater> SwSmellWater
        {
            get => _swSmellWater;
            set => Set(ref _swSmellWater, value);
        }
        /// <summary>
        /// Выбранная группа Запаха воды
        /// </summary>
        public GuideSmellwater SelectedSurwaterSmellwater { get; set; }


        private ObservableCollection<GuideClaritywater> _swClarityWater = new ObservableCollection<GuideClaritywater>();
        /// <summary>
        /// Справочник Прозрачность
        /// </summary>
        public ObservableCollection<GuideClaritywater> SwClarityWater
        {
            get => _swClarityWater;
            set => Set(ref _swClarityWater, value);
        }
        /// <summary>
        /// Выбранная группа Прозрачности
        /// </summary>
        public GuideClaritywater SelectedSurwaterClaritywater { get; set; }

        private ObservableCollection<GyideTypewatercourse> _swTypeWaterCourse = new ObservableCollection<GyideTypewatercourse>();
        /// <summary>
        /// Справочник Тип Водотока
        /// </summary>
        public ObservableCollection<GyideTypewatercourse> SwTypeWaterCourse
        {
            get => _swTypeWaterCourse;
            set => Set(ref _swTypeWaterCourse, value);
        }
        /// <summary>
        /// Выбранная группа Типа Водотока
        /// </summary>
        public GyideTypewatercourse SelectedSurwaterTypeWateCourse { get; set; }

        private ObservableCollection<GuideTypeusewater> _swTypeUseWater = new ObservableCollection<GuideTypeusewater>();
        /// <summary>
        /// Справочник Тип использования
        /// </summary>
        public ObservableCollection<GuideTypeusewater> SwTypeUseWater
        {
            get => _swTypeUseWater;
            set => Set(ref _swTypeUseWater, value);
        }
        /// <summary>
        /// Выбранная группа Типа использования
        /// </summary>
        public GuideTypeusewater SelectedSurwaterTypeUseWater { get; set; }

        private ObservableCollection<GuideTuperaid> _swTyperaid = new ObservableCollection<GuideTuperaid>();
        /// <summary>
        /// Справочник Тип налетов
        /// </summary>
        public ObservableCollection<GuideTuperaid> SwTyperaid
        {
            get => _swTyperaid;
            set => Set(ref _swTyperaid, value);
        }
        /// <summary>
        /// Выбранная группа Типа налетов
        /// </summary>
        public GuideTuperaid SelectedSurwaterTyperaid { get; set; }

        private ObservableCollection<GuideColor> _swColorPrimary;
        /// <summary>
        /// Справочник цвета
        /// </summary>
        public ObservableCollection<GuideColor> SwColorPrimary
        {
            get => _swColorPrimary; 
            set => Set (ref _swColorPrimary, value);
        }
        /// <summary>
        /// Выбранная группа Основного цвета
        /// </summary>
        public GuideColor SelectedSurwaterColorPrimary { get; set; }


        private ObservableCollection<Node> dopColor = new ObservableCollection<Node>();
        /// <summary>
        /// Антропогенное воздействие
        /// </summary>
        public ObservableCollection<Node> DopColor
        {
            get => dopColor;
            set => Set(ref dopColor, value);
        }

        private string swTextDopColor;

        public string SwTextDopColor
        {
            get => swTextDopColor; 
            set=>Set(ref swTextDopColor , value); 
        }

        #endregion

        #endregion
        // ---------------------------------------------------------------------------------------------------------------------
        #region Команды//Commands

        #region Команда НАЗАД
        /// <summary>
        /// Команда НАЗАД
        /// </summary>
        public ICommand BackNavigateCommand { get; }
        private bool BackNavigateCommandExecute(object p) => true;
        private void OnBackNavigateCommandExcuted(object p)
        {
            LocatorStatic.Data.PageHeader = $"Участок";
            this.navigationmaneger.Navigate("InfoDistrict", GlobalSet.NameDis);
        }
        #endregion

        #endregion
        // ---------------------------------------------------------------------------------------------------------------------

        #region Функции/Function

        public void FormatingCoord()
        {
            switch (FormatordSelected.Name)
            {
                case "Десятичная":
                    ShowCoordinatDegMinsSek = false;
                    ShowCoordinatDeсimal = true;
                    break;

                case "Градусы,минуты,секунды":
                    ShowCoordinatDegMinsSek = true;
                    ShowCoordinatDeсimal = false;
                    converter = new ConverterCordinatsService();
                    string l = "", w = "";
                    foreach (var item in watchpoint.WpointCoordinates)
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

        /// <summary>
        /// Принимаем переменную переданную из другой страницы
        /// </summary>
        /// <param name="arg"></param>
        public void OnNavigatedTo(object arg)
        {
            if (!(arg is int))
                return;

            PassedParameter = (int)arg;
            QueryDataBaseWatpoints();
            QueryDataBaseGround();
            QueryDataBaseGuideGround();
            QueryDataBaseGuideGeomorColumn();
            QueryDataBaseEgp();
            QueryDataBaseGuideEgp();
            QueryDataBaseTechobject();
            QueryDataBasePlant();
            QueryDataBaseGuidePlant();
            QueryDataBaseSurfacewater();
            QueryDataBaseGuideSurwater();
            LocatorStatic.Data.PageHeader += $" Точка наблюдения: {watchpoint.WpointId}";
        }

        /// <summary>
        /// Чтение из базы данных по точке наблюдения 
        /// </summary>
        /// <returns> Возвращаем класс Watchpoint </returns>
        private Watchpoint QueryDataBaseWatpoints()
        {
            var data = db.Watchpoints
                .Where(w => w.WpointId == PassedParameter)
                .Include(c => c.WpointCoordinates)
                .Include(r => r.Route)
                .ToList();

            foreach (var item in data)
            {
                watchpoint = new Watchpoint
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
                    IdExposition = item.IdExposition,
                    IdFormareliefa = item.IdFormareliefa,
                    IdFormariver = item.IdFormariver,
                    IdHeightreliefa = item.IdHeightreliefa,
                    IdSlope = item.IdSlope,
                    IdSubtypereliefa = item.IdSubtypereliefa,
                    IdTypereliefa = item.IdTypereliefa

                };
            }
            data.Clear();
            return watchpoint;
        }
        /// <summary>
        /// Чтение из базы данных по почве и грунту  
        /// </summary>
        /// <returns> Возвращаем класс Ground </returns>
        private Ground QueryDataBaseGround()
        {
            var data = db.Grounds
                     .Where(w => w.FWpointId == watchpoint.WpointId)
                     .Include(f => f.FColorNavigation)
                     .Include(us => us.FUser)
                     .ToList();

            foreach (var item in data)
            {
                ground = new Ground
                {
                    IdGround = item.IdGround,
                    FromGround = item.FromGround,
                    ToGround = item.ToGround,
                    FColorNavigation = item.FColorNavigation,
                    FDopcolor = item.FDopcolor,
                    FColor = item.FColor,
                    DataGround = item.DataGround,
                    DescriptionGround = item.DescriptionGround,
                    FUserId = item.FUserId,
                    FUser = item.FUser,
                    FBreed = item.FBreed,
                    FBreedId = item.FBreedId

                };
            }
            data.Clear();
            return ground;
        }
        /// <summary>
        /// Чтение из базы данных по ЭГП
        /// </summary>
        /// <returns> Возвращаем класс Egp </returns>
        private Egp QueryDataBaseEgp()
        {
            var data = db.Egps
               .Where(w => w.FWpointId == watchpoint.WpointId)
               .Include(fv => fv.FVidprocessNavigation)
               .Include(us => us.FUser)
               .ToList();
            foreach (var item in data)
            {
                egp = new Egp
                {
                    EgpId = item.EgpId,
                    FWpointId = item.FWpointId,
                    EgpSpeed = item.EgpSpeed,
                    EgpArea = item.EgpArea,
                    EgpDeep = item.EgpDeep,
                    EgpDescription = item.EgpDescription,
                    EgpLength = item.EgpLength,
                    EgpVolume = item.EgpVolume,
                    EgpWidth = item.EgpWidth,
                    DataEgp = item.DataEgp,
                    FEgpelement = item.FEgpelement,
                    FGroupprocess = item.FGroupprocess,
                    FTypeprocess = item.FTypeprocess,
                    FVidprocessNavigation = item.FVidprocessNavigation,
                    FUser = item.FUser

                };
            }
            data.Clear();
            return egp;
        }
        /// <summary>
        /// Чтение из базы данных по Техногенным объектам
        /// </summary>
        /// <returns>Возвращаем класс Techobject</returns>
        private Techobject QueryDataBaseTechobject()
        {
            var data = db.Techobjects.Where(w => w.FWpointId == watchpoint.WpointId).ToList();
            foreach (var item in data)
            {
                techobject = new Techobject
                {
                    TechobjName = item.TechobjName,
                    TechobjSource = item.TechobjSource,
                    TechobjLicense = item.TechobjLicense,
                    TechobjDescription = item.TechobjDescription,
                    TechobjMonitoring = item.TechobjMonitoring,
                    TechobjProducthole = item.TechobjWatchhole,
                    TechobjWatchhole = item.TechobjWatchhole
                };
            }
            data.Clear();
            return techobject;
        }
        /// <summary>
        /// Чтение из базы данных по Растительности
        /// </summary>
        /// <returns>Возвращаем класс Plant </returns>
        private Plant QueryDataBasePlant()
        {
            var data = db.Plants.Where(w => w.FWatchpoint == watchpoint.WpointId).ToList();
            foreach (var item in data)
            {
                plant = new Plant()
                {
                    PlantForestDensity = item.PlantForestDensity,
                    PlantHeightUndergrowth = item.PlantHeightUndergrowth,
                    PlantProjcoverUndergrowth = item.PlantProjcoverUndergrowth,
                    FUsrAdd = item.FUsrAdd,
                    PlantData = item.PlantData,
                    PlantStands = item.PlantStands,
                    PlantUndergrowth = item.PlantUndergrowth,
                    PlantBush = item.PlantBush,
                    PlantGroundcover = item.PlantGroundcover,
                    PlantSanitarycondition = item.PlantSanitarycondition,
                    PlantHumanimpact = item.PlantHumanimpact,
                    PlantSmallbush = item.PlantSmallbush,
                    PlantProjcoverGroundcover = item.PlantProjcoverGroundcover,
                    PlantDensityBush = item.PlantDensityBush
                };

            }
            data.Clear();
            return plant;
        }

        /// <summary>
        /// Чтение из базы данных Поверхностных вод
        /// </summary>
        /// <returns></returns>
        private Surfacewater QueryDataBaseSurfacewater()
        {
            var data = db.Surfacewaters.Where(w => w.FWpointId == watchpoint.WpointId).ToList();
            foreach (var item in data)
            {
                surfacewater = new Surfacewater()
                {
                    SwId = item.SwId,
                    FWpoint = item.FWpoint,
                    SwTypewatercourseId = item.SwTypewatercourseId,
                    SwWidth = item.SwWidth,
                    SwSpeedwater = item.SwSpeedwater,
                    SwTypebottomId = item.SwTypebottomId,
                    SwWatertemp = item.SwWatertemp,
                    SwAirtemp = item.SwAirtemp,
                    SwColorId = item.SwColorId,
                    SwOdorwaterId= item.SwOdorwaterId,
                    UserAddId = item.UserAddId,
                    SwDateAdd = item.SwDateAdd,
                    SwName = item.SwName,
                    SwBloom = item.SwBloom,
                    SwClaritywaterId = item.SwClaritywaterId,
                    SColorSecondary = item.SColorSecondary,
                    SwWaterFlowRate = item.SwWaterFlowRate,
                    SwTastewaterId = item.SwTastewaterId
                };
            }
            data.Clear();
            return surfacewater;
        }

        #region Справочники

        /// <summary>
        /// Читаем из базы данных все справочники по грунту и почве
        /// </summary>
        private void QueryDataBaseGuideGround()
        {
            if (ground is not null)
            {
                GroundBreed = db.GuideBreeds.AsNoTracking().ToObservableCollection();
                SelectedGroundBreed = GroundBreed.FirstOrDefault(g => g.IdBreed == ground.FBreedId && ground.FBreedId is not null);

                var data = db.GuideColors.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    if (item.BreedColor == 1 || item.PrimaryColor == 1)
                    {
                        _groundColor.Add(new GuideColor
                        {
                            IdColor = item.IdColor,
                            NameColor = item.NameColor
                        });
                    }
                }
                for (int i = 0; i < _groundColor.Count; i++)
                {
                    if (_groundColor[i].IdColor == ground.FColor && ground.FColor is not null)
                        SelectedGroundColor = _groundColor[i];
                }
                data.Clear();

                //Выбранный список оттенка
                if (ground.FDopcolor is not null)
                {
                    int dpc = Convert.ToInt32(ground.FDopcolor.Replace(";", ""));
                    SelectedGroundDopcolor = _groundDopcolor.FirstOrDefault(dp => dp.IdColor == dpc);
                }
            }
        }
        /// <summary>
        /// Читаем из базы данных все справочники по Геоморфологичесмкой колонке
        /// </summary>
        private void QueryDataBaseGuideGeomorColumn()
        {
            Formareliefa = db.GuideFormareliefas.AsNoTracking().ToObservableCollection();
            SelectedFormarelirfa = Formareliefa.FirstOrDefault(f => f.IdFormareliefa == watchpoint.IdFormareliefa && watchpoint.IdFormareliefa is not null);

            Typereliefa = db.GuideTypereliefas.AsNoTracking().ToObservableCollection();
            SelectedTypereliefa = Typereliefa.FirstOrDefault(t => t.IdTypereliefa == watchpoint.IdTypereliefa && watchpoint.IdTypereliefa is not null);

            Heightreliefa = db.GuideHeightreliefas.AsNoTracking().ToObservableCollection();
            SelectedHeightreliefa = Heightreliefa.FirstOrDefault(h => h.IdHeightreliefa == watchpoint.IdHeightreliefa && watchpoint.IdHeightreliefa is not null);

            Sybtypereliefa = db.GuideSubtypereliefas.AsNoTracking().ToObservableCollection();
            SelectedSybtypereliefa = Sybtypereliefa.FirstOrDefault(s => s.IdSubtypereliefa == watchpoint.IdSubtypereliefa && watchpoint.IdSubtypereliefa is not null);

            Exposition = db.GuideSprexpositions.AsNoTracking().ToObservableCollection();
            SelectedExposition = Exposition.FirstOrDefault(e => e.IdSprexposition == watchpoint.IdExposition && watchpoint.IdExposition is not null);

            Slope = db.GuideSlopes.AsNoTracking().ToObservableCollection();
            SelectedSlope = Slope.FirstOrDefault(sl => sl.IdSlope == watchpoint.IdSlope && watchpoint.IdSlope is not null);

            Formariver = db.GuideFormarivers.AsNoTracking().ToObservableCollection();
            SelectedFormariver = Formariver.FirstOrDefault(f => f.IdFormariver == watchpoint.IdFormariver && watchpoint.IdFormariver is not null);
        }

        /// <summary>
        /// Читаем из базы данных все справочники по ЭГП 
        /// </summary>
        private void QueryDataBaseGuideEgp()
        {
            if (egp is not null)
            {
                Egpgroupprocess = db.GuideGroupprocces.AsNoTracking().ToObservableCollection();
                SelectedGroupprocce = Egpgroupprocess.FirstOrDefault(e => e.IdGroupprocces == egp.FGroupprocess && egp.FGroupprocess is not null);

                Egptypeprocess = db.GuideTypeprocesses.AsNoTracking().ToObservableCollection();
                SelectedTypeprocess = Egptypeprocess.FirstOrDefault(e => e.IdTypeprocess == egp.FTypeprocess && egp.FTypeprocess is not null);

                EgpElement = db.GuideEgpelements.AsNoTracking().ToObservableCollection();
                SelectedEgpElement = EgpElement.FirstOrDefault(e => e.IdEgpelement == egp.FEgpelement && egp.FEgpelement is not null);
            }

        }

        /// <summary>
        /// Читаем из базы данных все справочники по Растительности
        /// </summary>
        private void QueryDataBaseGuidePlant()
        {
            if (plant is not null)
            {
                // Получаем данные по справочнику Густоты леса
                PlantForestDensities = db.GuideForestDensities.AsNoTracking().ToObservableCollection();
                // Выводим выбранный элемент густоты леса
                SelectedPlantForestDensities = PlantForestDensities.FirstOrDefault(p => p.IdDforest == plant.PlantForestDensity);

                // Получаем данные по справочнику Высоты подроста
                PlantHeightUndergrowt = db.GuideHeightUndergrowths.AsNoTracking().ToObservableCollection();
                // Выводим выбранный элемент Высоты подроста
                SelectedPlantHeightUndergrowt = PlantHeightUndergrowt.FirstOrDefault(p => p.IdHeight == plant.PlantHeightUndergrowth);

                // Получаем данные по справочнику проективное покрытие подроста
                PlantProjcoverUndergrowth = db.GuideProjcoverUndergrowths.AsNoTracking().ToObservableCollection();

                // Выводим выбранный элемент проективное покрытие подроста
                SelectedPlantProjcoverUndergrowth = PlantProjcoverUndergrowth.FirstOrDefault(p => p.IdPrjUnder == plant.PlantProjcoverUndergrowth && plant.PlantProjcoverUndergrowth is not null);

                // Получаем данные по справочнику Густота кустарника
                PlantDensityBush = db.GuideDensityBushes.AsNoTracking().ToObservableCollection();
                // Выводим выбранный элемент ГУстота кустарника
                SelectedPlantDensityBush = PlantDensityBush.FirstOrDefault(p => p.IdDbush == plant.PlantDensityBush && plant.PlantDensityBush is not null);

                //Проективное покрытие напочвенного покрова
                PrjGround = db.GuideProjcoverGroundcovers.AsNoTracking().ToObservableCollection();
                SelectedPlantPrjGround = PrjGround.FirstOrDefault(p => p.IdPrjGround == plant.PlantProjcoverGroundcover);

                // санитрное состояние
                PSanitar = AddGuideExpander(plant.PlantSanitarycondition, PSanitar, "Санитарное");
                //Антропогенное воздействие
                PHumanimpact = AddGuideExpander(plant.PlantHumanimpact, PHumanimpact, "Антропогоенное");
                //Кустарник
                PBush = AddGuideExpander(plant.PlantBush, PBush, 2);
                //Древостой
                PStands = AddGuideExpander(plant.PlantStands, PStands, 1);
                //Кустарничек
                PSmallbush = AddGuideExpander(plant.PlantSmallbush, PSmallbush, 4);
                //Подрост
                PUndergrowth = AddGuideExpander(plant.PlantUndergrowth, PUndergrowth, 1);
                //Напочвенный покров
                PGroundcoverh = AddGuideExpander(plant.PlantGroundcover, PGroundcoverh, 3);
            }
            else
            {
                PlantForestDensities = db.GuideForestDensities.AsNoTracking().ToObservableCollection();
                PlantHeightUndergrowt = db.GuideHeightUndergrowths.AsNoTracking().ToObservableCollection();
                PlantProjcoverUndergrowth = db.GuideProjcoverUndergrowths.AsNoTracking().ToObservableCollection();
                PlantDensityBush = db.GuideDensityBushes.AsNoTracking().ToObservableCollection();
                PrjGround = db.GuideProjcoverGroundcovers.AsNoTracking().ToObservableCollection();
            }
        }

        private void QueryDataBaseGuideSurwater()
        {
            if(surfacewater is not null)
            {   //Тип дна
                SwTypeBottom = db.GuideTypebottoms.AsNoTracking().ToObservableCollection();
                SelectedSurwaterTypebottom = SwTypeBottom.FirstOrDefault(s => s.IdTypebottom == surfacewater?.SwTypebottomId && surfacewater.SwTypebottomId is not null);
                //Вкус Воды
                SwTasteWater = db.GuideTastewaters.AsNoTracking().ToObservableCollection();
                SelectedSurwaterTastewater = SwTasteWater.FirstOrDefault(s => s.IdTestwater == surfacewater?.SwTastewaterId && surfacewater.SwTastewaterId is not null);
                //Запахи воды
                SwSmellWater = db.GuideSmellwaters.AsNoTracking().ToObservableCollection();
                SelectedSurwaterSmellwater = SwSmellWater.FirstOrDefault(s => s.IdSmellwater == surfacewater?.SwOdorwaterId && surfacewater.SwOdorwaterId is not null);
                // Прозрачность
                SwClarityWater = db.GuideClaritywaters.AsNoTracking().ToObservableCollection();
                SelectedSurwaterClaritywater = SwClarityWater.FirstOrDefault(s => s.IdClaritywater == surfacewater?.SwClaritywaterId && surfacewater.SwClaritywaterId is not null);
                // Тип водотока
                SwTypeWaterCourse = db.GyideTypewatercourses.AsNoTracking().ToObservableCollection();
                SelectedSurwaterTypeWateCourse = SwTypeWaterCourse.FirstOrDefault(s => s.IdTypewatercourse == surfacewater?.SwTypewatercourseId && surfacewater.SwTypewatercourseId is not null);

                // РАЗОБРАТЬСЯ ГДЕ ИСПОЛЬЗУЕТСЯ!!!!!
                //SwTypeUseWater = db.GuideTypeusewaters.AsNoTracking().ToObservableCollection();
                //SelectedSurwaterTypeUseWater = SwTypeUseWater.FirstOrDefault(s=>s.IdTypeusewater == surfacewater?.sw)

                //SwTyperaid = db.GuideTuperaids.AsNoTracking().ToObservableCollection();
                //SelectedSurwaterTyperaid = SwTyperaid.FirstOrDefault(s=>s.IdTuperaid == surfacewater.typeraid)

                SwColorPrimary = db.GuideColors.AsNoTracking().Where(w=>w.WaterColor == 1 && w.PrimaryColor == 1).ToObservableCollection();
                SelectedSurwaterColorPrimary = SwColorPrimary.FirstOrDefault(s => s.IdColor == surfacewater?.SwColorId && surfacewater.SwColorId is not null);
                var id = surfacewater.SwColorId;

                var str = surfacewater.SColorSecondary;
                str.TrimEnd(';');
                var array = str.Split(';');
                var collection = db.GuideColors.AsNoTracking().Where(w => w.WaterColor == 1 && w.SecondaryColor == 1).ToList();
                array = array.Where(val => val != "").ToArray();
                DopColor.Clear();
                
                foreach (var item in collection)
                {
                    
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (Convert.ToInt32(array[i]) == item.IdColor)
                        {
                            DopColor.Add(new Node(item.NameColor, true));
                            SwTextDopColor += item.NameColor +";";  
                        }
                        else
                        {
                            DopColor.Add(new Node(item.NameColor, false));
                        }
                    }                   

                }
                

                
                //DopColor 






            }
        }
        #endregion
        /// <summary>
        /// Заполнение коллекции типа ObservableCollection<Node> из справочника GuidePlants(Растительность)
        /// </summary>
        /// <param name="str">Строка, в которой лежит несколько элементов в виде ID объекта через  ';' </param>
        /// <param name="nodecoll">Заполняемая коллекция</param>
        /// <param name="typePlant">Тип раститительности</param>
        /// <returns>Возвращение заполнениной коллекции</returns>
        private ObservableCollection<Node> AddGuideExpander(string str, ObservableCollection<Node> nodecoll, int typePlant)
        {
            var gplants = db.GuidePlants.AsNoTracking().ToList();
            nodecoll.Clear();
            if (!string.IsNullOrEmpty(str))
            {
                str.TrimEnd(';');
                var array = str.Split(';');
                foreach (var item in gplants)
                {
                    if (typePlant == item.FTypePlant)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] != "")
                            {
                                if (Convert.ToInt32(array[i]) == item.IdPlant)
                                {
                                    nodecoll.Add(new Node(item.NamePlant, true));
                                }
                            }
                        }
                        var b = nodecoll.Contains(t => t.Title == item.NamePlant);
                        if (!b)
                            nodecoll.Add(new Node(item.NamePlant, false));
                    }
                }
            }
            else
            {
                foreach (var item in gplants)
                {
                    if (typePlant == item.FTypePlant)
                    {
                        nodecoll.Add(new Node(item.NamePlant, false));
                    }
                }
            }
            gplants.Clear();
            return nodecoll;
        }

        /// <summary>
        /// Заполнение коллекции типа ObservableCollection<Node> данными из справочника GuideSanitaryconditions (санитарное состояние) и GuideHumanimpacts(Антропогоенное воздействие)
        /// </summary>
        /// <param name="str"> Строка, в которой лежит несколько элементов в виде ID объекта через ';' </param>
        /// <param name="nodecoll"> Заполняемая коллекция</param>     
        /// <returns>Возвращение заполнениной коллекции</returns>
        private ObservableCollection<Node> AddGuideExpander(string str, ObservableCollection<Node> nodecoll, string guide)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str.TrimEnd(';');
                var array = str.Split(';');
                switch (guide)
                {
                    case "Санитарное":
                        var gsanitar = db.GuideSanitaryconditions.AsNoTracking().ToList();
                        foreach (var item in gsanitar)
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i] != "")
                                {


                                    if (Convert.ToInt32(array[i]) == item.IdSanitar)
                                    {
                                        nodecoll.Add(new Node(item.NameSanitar, true));
                                    }
                                }

                            }
                            var b = nodecoll.Contains(t => t.Title == item.NameSanitar);
                            if (!b)
                                nodecoll.Add(new Node(item.NameSanitar, false));
                        }
                        break;
                    case "Антропогоенное":
                        var ghum = db.GuideHumanimpacts.AsNoTracking().ToList();
                        foreach (var item in ghum)
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i] != "")
                                {
                                    if (Convert.ToInt32(array[i]) == item.IdHumanimpact)
                                    {
                                        nodecoll.Add(new Node(item.NameHumanimpact, true));
                                    }
                                }

                            }
                            var b = nodecoll.Contains(t => t.Title == item.NameHumanimpact);
                            if (!b)
                                nodecoll.Add(new Node(item.NameHumanimpact, false));
                        }
                        break;

                }

            }
            return nodecoll;

        }

        #endregion


        // ---------------------------------------------------------------------------------------------------------------------

        public InfoPointObservationViewModel(NavigationManager navigationmaneger, GeocomplexContext _db)
        {
            this.navigationmaneger = navigationmaneger;
            db = _db;
            // Создание данных для выпадающего списка по формату координат
            FormatCoordinat = new ObservableCollection<FormatCoordinatModel>()
            {
                new FormatCoordinatModel() { Name = "Десятичная" },
                new FormatCoordinatModel() { Name = "Градусы,минуты,секунды" }
            };
            BackNavigateCommand = new LamdaCommand(OnBackNavigateCommandExcuted, BackNavigateCommandExecute);
            ShowCoordinatDegMinsSek = false;
            ShowCoordinatDeсimal = true;
        }
    }
    /// <summary>
    /// Класс предназначенный для списков отображающие чекбоксами элементы 
    /// </summary>
    class Node : ViewModel
    {
        public Node(string title, bool selected)
        {
            Title = title;
            Selected = selected;
        }
        private string _title;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private bool _isSelected;

        public bool Selected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }
    }
}
