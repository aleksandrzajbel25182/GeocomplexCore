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
        private GeocomplexContext db = new GeocomplexContext();
        private Ground? ground;
        private Egp? egp;
        private Watchpoint? watchpoint;
        private Techobject? techobject;




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
        public ObservableCollection<GuideFormareliefa> Formareliefa
        {
            get => _formareliefa;
            set { _formareliefa = value; }
        }
        public GuideFormareliefa SelectedFormarelirfa { get; set; }
        #endregion

        #region Cписок Тип рельефа 
        private ObservableCollection<GuideTypereliefa> _typereliefa = new ObservableCollection<GuideTypereliefa>();
        public ObservableCollection<GuideTypereliefa> Typereliefa
        {
            get => _typereliefa;
            set { _typereliefa = value; }
        }
        public GuideTypereliefa SelectedTypereliefa { get; set; }
        #endregion

        #region Список Подтип рельефа
        private ObservableCollection<GuideSubtypereliefa> _sybtypereliefa = new ObservableCollection<GuideSubtypereliefa>();
        public ObservableCollection<GuideSubtypereliefa> Sybtypereliefa
        {
            get => _sybtypereliefa;
            set { _sybtypereliefa = value; }
        }
        public GuideSubtypereliefa SelectedSybtypereliefa { get; set; }
        #endregion

        #region Список Высотность рельефа
        private ObservableCollection<GuideHeightreliefa> _heightreliefa = new ObservableCollection<GuideHeightreliefa>();
        public ObservableCollection<GuideHeightreliefa> Heightreliefa
        {
            get => _heightreliefa;
            set { _ = value; }
        }
        public GuideHeightreliefa SelectedHeightreliefa { get; set; }
        #endregion

        #region Список Экспозиция
        private ObservableCollection<GuideSprexposition> _exposition = new ObservableCollection<GuideSprexposition>();
        public ObservableCollection<GuideSprexposition> Exposition
        {
            get => _exposition;
            set { _exposition = value; }
        }
        public GuideSprexposition SelectedExposition { get; set; }
        #endregion

        #region Список Крутизна склона
        private ObservableCollection<GuideSlope> _slope = new ObservableCollection<GuideSlope>();
        public ObservableCollection<GuideSlope> Slope
        {
            get => _slope;
            set { _slope = value; }
        }
        public GuideSlope SelectedSlope { get; set; }
        #endregion

        #region Список Форма речной долины
        private ObservableCollection<GuideFormariver> _formariver = new ObservableCollection<GuideFormariver>();
        public ObservableCollection<GuideFormariver> Formariver
        {
            get => _formariver;
            set { _formariver = value; }
        }
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
        public ObservableCollection<GuideBreed> GroundBreed
        {
            get => _groundBreed;

            set { _groundBreed = value; }
        }
        public GuideBreed SelectedGroundBreed { get; set; }
        #endregion

        #region Список Оттенка
        private ObservableCollection<GuideColor> _groundDopcolor = new ObservableCollection<GuideColor>();
        public ObservableCollection<GuideColor> GroundDopcolor
        {
            get => _groundDopcolor;
            set { _groundDopcolor = value; }
        }
        public GuideColor SelectedGroundDopcolor { get; set; }
        #endregion


        #region Список Цвета
        private ObservableCollection<GuideColor> _groundColor = new ObservableCollection<GuideColor>();
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

        /// <summary>
        /// /Группа процессов
        /// </summary>
        private ObservableCollection<GuideGroupprocce> _egpgroupprocess = new ObservableCollection<GuideGroupprocce>();
        public ObservableCollection<GuideGroupprocce> Egpgroupprocess
        {
            get => _egpgroupprocess;
            set { _egpgroupprocess = value; }
        }
        /// <summary>
        /// Выбранная группа процесса эгп
        /// </summary>
        public GuideGroupprocce SelectedGroupprocce { get; set; }

        /// <summary>
        /// ЭГП тип Процесса
        /// </summary>
        private ObservableCollection<GuideTypeprocess> _egptypeprocess = new ObservableCollection<GuideTypeprocess>();
        public ObservableCollection<GuideTypeprocess> Egptypeprocess
        {
            get => _egptypeprocess;
            set { _egptypeprocess = value; }
        }
        /// <summary>
        /// Выбранный тип процесса
        /// </summary>
        public GuideTypeprocess SelectedTypeprocess { get; set; }

        /// <summary>
        /// Вторичный элемент ЭГП
        /// </summary>
        private ObservableCollection<GuideEgpelement> _egpElement = new ObservableCollection<GuideEgpelement>();
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
               .Include(f => f.FEgpelementNavigation)
               .Include(fg => fg.FGroupprocessNavigation)
               .Include(ft => ft.FTypeprocessNavigation)
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
                    FEgpelementNavigation = item.FEgpelementNavigation,
                    FGroupprocessNavigation = item.FGroupprocessNavigation,
                    FTypeprocessNavigation = item.FTypeprocessNavigation,
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
        /// Читаем из базы данных все справочники по грунту и почве
        /// </summary>
        private void QueryDataBaseGuideGround()
        {
            if (ground is not null)
            {
                var gbreeds = db.GuideBreeds.AsNoTracking().ToList();
                foreach (var item in gbreeds)
                {
                    _groundBreed.Add(new GuideBreed
                    {
                        IdBreed = item.IdBreed,
                        NameBreed = item.NameBreed,
                        NamersBred = item.NamersBred
                    });
                }
                gbreeds.Clear();
                for (int i = 0; i < _groundBreed.Count; i++)
                {
                    if (_groundBreed[i].IdBreed == ground.FBreedId && ground.FBreedId is not null)
                        SelectedGroundBreed = _groundBreed[i];
                }

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
                    for (int i = 0; i < _groundDopcolor.Count; i++)
                    {
                        if (_groundDopcolor[i].IdColor == dpc)
                            SelectedGroundDopcolor = _groundDopcolor[i];
                    }
                }
            }
        }
        /// <summary>
        /// Читаем из базы данных все справочники по Геоморфологичесмкой колонке
        /// </summary>
        private void QueryDataBaseGuideGeomorColumn()
        {
            var gformrelief = db.GuideFormareliefas.AsNoTracking().ToList();
            foreach (var item in gformrelief)
            {
                _formareliefa.Add(new GuideFormareliefa
                {
                    IdFormareliefa = item.IdFormareliefa,
                    NameFormareliefa = item.NameFormareliefa
                });
            }
            gformrelief.Clear();
            for (int i = 0; i < _formareliefa.Count; i++)
            {
                if (_formareliefa[i].IdFormareliefa == watchpoint.IdFormareliefa && watchpoint.IdFormareliefa is not null)
                    SelectedFormarelirfa = _formareliefa[i];
            }

            var gtyperelief = db.GuideTypereliefas.AsNoTracking().ToList();
            foreach (var item in gtyperelief)
            {
                _typereliefa.Add(new GuideTypereliefa
                {
                    IdTypereliefa = item.IdTypereliefa,
                    NameTypereliefa = item.NameTypereliefa
                });
            }
            for (int i = 0; i < _typereliefa.Count; i++)
            {
                if (_typereliefa[i].IdTypereliefa == watchpoint.IdTypereliefa && watchpoint.IdTypereliefa is not null)
                    SelectedTypereliefa = _typereliefa[i];
            }
            gtyperelief.Clear();

            var gheihtrelief = db.GuideHeightreliefas.AsNoTracking().ToList();
            foreach (var item in gheihtrelief)
            {
                _heightreliefa.Add(new GuideHeightreliefa
                {
                    IdHeightreliefa = item.IdHeightreliefa,
                    NameHeightreliefa = item.NameHeightreliefa
                });
            }
            for (int i = 0; i < _heightreliefa.Count; i++)
            {
                if (_heightreliefa[i].IdHeightreliefa == watchpoint.IdHeightreliefa && watchpoint.IdHeightreliefa is not null)
                    SelectedHeightreliefa = _heightreliefa[i];
            }

            var gsubtyprerelief = db.GuideSubtypereliefas.AsNoTracking().ToList();
            foreach (var item in gsubtyprerelief)
            {
                _sybtypereliefa.Add(new GuideSubtypereliefa
                {
                    IdSubtypereliefa = item.IdSubtypereliefa,
                    NameSubtypereliefa = item.NameSubtypereliefa
                });
            }
            gsubtyprerelief.Clear();
            for (int i = 0; i < _sybtypereliefa.Count; i++)
            {
                if (_sybtypereliefa[i].IdSubtypereliefa == watchpoint.IdSubtypereliefa && watchpoint.IdSubtypereliefa is not null)
                    SelectedSybtypereliefa = _sybtypereliefa[i];
            }

            var gsprexposit = db.GuideSprexpositions.AsNoTracking().ToList();
            foreach (var item in gsprexposit)
            {
                _exposition.Add(new GuideSprexposition
                {
                    IdSprexposition = item.IdSprexposition,
                    NameSprexposition = item.NameSprexposition
                });
            }
            gsprexposit.Clear();
            for (int i = 0; i < _exposition.Count; i++)
            {
                if (_exposition[i].IdSprexposition == watchpoint.IdExposition && watchpoint.IdExposition is not null)
                    SelectedExposition = _exposition[i];
            }

            var gslpoes = db.GuideSlopes.AsNoTracking().ToList();
            foreach (var item in gslpoes)
            {
                _slope.Add(new GuideSlope
                {
                    IdSlope = item.IdSlope,
                    NameSlope = item.NameSlope
                });
            }
            gslpoes.Clear();
            for (int i = 0; i < _slope.Count; i++)
            {
                if (_slope[i].IdSlope == watchpoint.IdSlope && watchpoint.IdSlope is not null)
                    SelectedSlope = _slope[i];
            }

            var gformrivers = db.GuideFormarivers.AsNoTracking().ToList();
            foreach (var item in gformrivers)
            {
                _formariver.Add(new GuideFormariver
                {
                    IdFormariver = item.IdFormariver,
                    NameFormariver = item.NameFormariver
                });
            }
            gformrivers.Clear();
            for (int i = 0; i < _formariver.Count; i++)
            {
                if (_formariver[i].IdFormariver == watchpoint.IdFormariver && watchpoint.IdFormariver is not null)
                    SelectedFormariver = _formariver[i];
            }
        }
        /// <summary>
        /// Читаем из базы данных все справочники по ЭГП 
        /// </summary>
        private void QueryDataBaseGuideEgp()
        {
            if (egp is not null)
            {
                var ggroupprocces = db.GuideGroupprocces.AsNoTracking().ToList();
                foreach (var item in ggroupprocces)
                {
                    _egpgroupprocess.Add(new GuideGroupprocce
                    {
                        IdGroupprocces = item.IdGroupprocces,
                        NameGroupprocess = item.NameGroupprocess
                    });
                }
                ggroupprocces.Clear();
                for (int i = 0; i < _egpgroupprocess.Count; i++)
                {
                    if (_egpgroupprocess[i].IdGroupprocces == egp.FGroupprocess && egp.FGroupprocess is not null)
                        SelectedGroupprocce = _egpgroupprocess[i];
                }

                var gTypeprocesses = db.GuideTypeprocesses.AsNoTracking().ToList();
                foreach (var item in gTypeprocesses)
                {
                    _egptypeprocess.Add(new GuideTypeprocess
                    {
                        IdTypeprocess = item.IdTypeprocess,
                        NameTypeprocess = item.NameTypeprocess

                    });
                }
                gTypeprocesses.Clear();
                for (int i = 0; i < _egptypeprocess.Count; i++)
                {
                    if (_egptypeprocess[i].IdTypeprocess == egp.FTypeprocess && egp.FTypeprocess is not null)
                        SelectedTypeprocess = _egptypeprocess[i];
                }

                var gegpelements = db.GuideEgpelements.AsNoTracking().ToList();
                foreach (var item in gegpelements)
                {
                    _egpElement.Add(new GuideEgpelement
                    {
                        IdEgpelement = item.IdEgpelement,
                        NameEgpelement = item.NameEgpelement

                    });
                }
                gegpelements.Clear();
                for (int i = 0; i < _egpElement.Count; i++)
                {
                    if (_egpElement[i].IdEgpelement == egp.FEgpelement && egp.FTypeprocess is not null)
                        SelectedEgpElement = _egpElement[i];
                }
            }

        }
        #endregion
        // ---------------------------------------------------------------------------------------------------------------------

        public InfoPointObservationViewModel(NavigationManager navigationmaneger)
        {
            this.navigationmaneger = navigationmaneger;
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
}
