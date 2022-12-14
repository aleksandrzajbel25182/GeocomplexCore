using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
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
        private Ground ground;
        private Egp egp;
        private Watchpoint Watchpoints;
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
                GetWatchpoints();
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
                GetWatchpoints();
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
                GetWatchpoints();
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
                GetWatchpoints();
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
                GetWatchpoints();
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

        //Кординыты X
        private double? _wpointX;
        public double? WpointX
        {
            get
            {
                GetWatchpoints();

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
                GetWatchpoints();

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
                GetWatchpoints();

                return _pointZ;
            }
            set { _pointZ = value; }
        }
    
        #region Коллекции "Геоморфологическая колонка" 

        #region Список Форма рельефа
        private ObservableCollection<GuideFormareliefa> _formareliefa = new ObservableCollection<GuideFormareliefa>();
        public ObservableCollection<GuideFormareliefa> Formareliefa
        {
            get
            {
                var data = db.GuideFormareliefas.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _formareliefa.Add(new GuideFormareliefa
                    {
                        IdFormareliefa = item.IdFormareliefa,
                        NameFormareliefa = item.NameFormareliefa
                    });

                }
                GetWatchpoints();
                return _formareliefa;
            }
            set { _formareliefa = value; }
        }
        public GuideFormareliefa SelectedFormarelirfa { get; set; }
        #endregion

        #region Cписок Тип рельефа 
        private ObservableCollection<GuideTypereliefa> _typereliefa = new ObservableCollection<GuideTypereliefa>();
        public ObservableCollection<GuideTypereliefa> Typereliefa
        {
            get
            {
                var data = db.GuideTypereliefas.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _typereliefa.Add(new GuideTypereliefa
                    {
                        IdTypereliefa = item.IdTypereliefa,
                        NameTypereliefa = item.NameTypereliefa
                    });

                }
                GetWatchpoints();
                return _typereliefa;
            }
            set { _typereliefa = value; }
        }
        public GuideTypereliefa SelectedTypereliefa { get; set; }
        #endregion

        #region Список Подтип рельефа
        private ObservableCollection<GuideSubtypereliefa> _sybtypereliefa = new ObservableCollection<GuideSubtypereliefa>();
        public ObservableCollection<GuideSubtypereliefa> Sybtypereliefa
        {
            get
            {
                var data = db.GuideSubtypereliefas.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _sybtypereliefa.Add(new GuideSubtypereliefa
                    {
                        IdSubtypereliefa = item.IdSubtypereliefa,
                        NameSubtypereliefa = item.NameSubtypereliefa
                    });
                }
                GetWatchpoints();
                return _sybtypereliefa;
            }
            set { _sybtypereliefa = value; }
        }
        public GuideSubtypereliefa SelectedSybtypereliefa { get; set; }
        #endregion

        #region Список Высотность рельефа
        private ObservableCollection<GuideHeightreliefa> _heightreliefa = new ObservableCollection<GuideHeightreliefa>();
        public ObservableCollection<GuideHeightreliefa> Heightreliefa
        {
            get
            {
                var data = db.GuideHeightreliefas.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _heightreliefa.Add(new GuideHeightreliefa
                    {
                        IdHeightreliefa = item.IdHeightreliefa,
                        NameHeightreliefa = item.NameHeightreliefa
                    });

                }
                GetWatchpoints();
                return _heightreliefa;
            }
            set { _ = value; }
        }
        public GuideHeightreliefa SelectedHeightreliefa { get; set; }
        #endregion

        #region Список Экспозиция
        private ObservableCollection<GuideSprexposition> _exposition = new ObservableCollection<GuideSprexposition>();
        public ObservableCollection<GuideSprexposition> Exposition
        {
            get
            {
                var data = db.GuideSprexpositions.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _exposition.Add(new GuideSprexposition
                    {
                        IdSprexposition = item.IdSprexposition,
                        NameSprexposition = item.NameSprexposition
                    });

                }
                GetWatchpoints();
                return _exposition;

            }
            set { _exposition = value; }
        }
        public GuideSprexposition SelectedExposition { get; set; }
        #endregion

        #region Список Крутизна склона
        private ObservableCollection<GuideSlope> _slope = new ObservableCollection<GuideSlope>();
        public ObservableCollection<GuideSlope> Slope
        {
            get
            {
                var data = db.GuideSlopes.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _slope.Add(new GuideSlope
                    {
                        IdSlope = item.IdSlope,
                        NameSlope = item.NameSlope
                    });

                }
                GetWatchpoints();
                return _slope;

            }
            set { _slope = value; }
        }
        public GuideSlope SelectedSlope { get; set; }
        #endregion

        #region Список Форма речной долины
        private ObservableCollection<GuideFormariver> _formariver = new ObservableCollection<GuideFormariver>();
        public ObservableCollection<GuideFormariver> Formariver
        {
            get
            {
                var data = db.GuideFormarivers.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _formariver.Add(new GuideFormariver
                    {
                        IdFormariver = item.IdFormariver,
                        NameFormariver = item.NameFormariver
                    });

                }
                GetWatchpoints();
                return _formariver;
            }
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
            get
            {
                var data = db.GuideBreeds.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _groundBreed.Add(new GuideBreed
                    {
                        IdBreed = item.IdBreed,
                        NameBreed = item.NameBreed,
                        NamersBred = item.NamersBred
                    });
                }
                data.Clear();
                GetGround();
                return _groundBreed;
            }

            set { _groundBreed = value; }
        }
        public GuideBreed SelectedGroundBreed { get; set; }
        #endregion

        #region Список Оттенка
        private ObservableCollection<GuideColor> _groundDopcolor = new ObservableCollection<GuideColor>();
        public ObservableCollection<GuideColor> GroundDopcolor
        {
            get
            {
                var data = db.GuideColors.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    if (item.BreedColor == 1 || item.PrimaryColor == 1)
                    {
                        _groundDopcolor.Add(new GuideColor
                        {
                            IdColor = item.IdColor,
                            NameColor = item.NameColor
                        });
                    }
                }
                data.Clear();
                GetGround();
                return _groundDopcolor;

            }
            set { _groundDopcolor = value; }
        }
        public GuideColor SelectedGroundDopcolor { get; set; }
        #endregion


        #region Список Цвета
        private ObservableCollection<GuideColor> _groundColor = new ObservableCollection<GuideColor>();
        public ObservableCollection<GuideColor> GroundColor
        {
            get
            {
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
                data.Clear();
                GetGround();
                return _groundColor;
            }
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
                GetGround();
                if (_groundData == null)
                {
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
                GetGround();
                if (_groundUserName is null)
                    return _groundUserName;
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
                GetGround();
                if (_groundDescription is null)
                    return _groundDescription;
                return _groundDescription;

            }
            set => Set(ref _groundDescription, value);
        }
        #endregion

        #region Интервал ОТ
        /// <summary>
        /// Интервал ОТ
        /// </summary>
        private string _groundFrom;
        public string GroundFrom
        {
            get
            {
                GetGround();
                if (_groundFrom is null)
                    return _groundFrom;
                return _groundFrom;

            }
            set => Set(ref _groundFrom, value);
        }
        #endregion

        #region Интервал ДО
        /// <summary>
        /// Интервал ДО
        /// </summary>
        private string _groundTo;
        public string GroundTo
        {
            get
            {
                GetGround();
                if (_groundTo is null)
                    return _groundTo;
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
            get
            {

                var data = db.GuideGroupprocces.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _egpgroupprocess.Add(new GuideGroupprocce
                    {
                        IdGroupprocces = item.IdGroupprocces,
                        NameGroupprocess = item.NameGroupprocess
                    });
                }
                data.Clear();

                GetEgp();

                return _egpgroupprocess;

            }
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
            get
            {
                var data = db.GuideTypeprocesses.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _egptypeprocess.Add(new GuideTypeprocess
                    {
                        IdTypeprocess = item.IdTypeprocess,
                        NameTypeprocess = item.NameTypeprocess

                    });
                }
                data.Clear();
                GetEgp();

                return _egptypeprocess;

            }
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
            get
            {
                var data = db.GuideEgpelements.AsNoTracking().ToList();
                foreach (var item in data)
                {
                    _egpElement.Add(new GuideEgpelement
                    {
                        IdEgpelement = item.IdEgpelement,
                        NameEgpelement = item.NameEgpelement

                    });
                }
                data.Clear();
                GetEgp();
                return _egpElement;
            }
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
                GetEgp();
                if (_egpDate is null)
                    return _egpDate;
                return _egpDate;
            }
            set => Set(ref _egpDate, value);
        }


        private string _egpUserName;
        public string EgpUserName
        {
            get
            {
                GetEgp();
                if (_egpUserName is null)
                    return _egpUserName;
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
                GetEgp();
                if (_egpDeep is null)
                    return _egpDeep;
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
                GetEgp();
                if (_egpWidth is null)
                    return _egpWidth;
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
                GetEgp();
                if (_egpLength is null)
                    return _egpLength;
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
                GetEgp();
                if (_egpVolume is null)
                    return _egpVolume;
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
                GetEgp();
                if (_egpArea is null)
                    return _egpArea;
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
                GetEgp();
                if (_egpSpeed is null)
                    return _egpSpeed;
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
                GetEgp();
                if (_egpDescription is null)
                    return _egpDescription;
                return _egpDescription;
            }
            set { _egpDescription = value; }
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

        /// <summary>
        /// Читаем данные из базы для Почвы и грунта и заносим их в свойства
        /// </summary>
        private void GetGround()
        {

            var data = db.Grounds
                     .Where(w => w.FWpointId == Watchpoints.WpointId)
                     .Include(f => f.FColorNavigation)
                     .Include(us => us.FUser)
                     .ToList();

            if (data.Count != 0)
            {
                //Создаем экземпляр класса Ground
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

                _groundData = new DateTime(ground.DataGround.Value.Year, ground.DataGround.Value.Month, ground.DataGround.Value.Day);
                // Выбраанный Список Породы
                for (int i = 0; i < _groundBreed.Count; i++)
                {
                    if (_groundBreed[i].IdBreed == ground.FBreedId)
                        SelectedGroundBreed = _groundBreed[i];
                }

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


                //Выбранный список цвета
                for (int i = 0; i < _groundColor.Count; i++)
                {
                    if (_groundColor[i].IdColor == ground.FColor)
                        SelectedGroundColor = _groundColor[i];
                }

                _groundUserName = ground.FUser.UserName;
                _groundDescription = ground.DescriptionGround;
                _groundFrom = ground.FromGround.ToString();
                _groundTo = ground.ToGround.ToString();
            }

        }

        /// <summary>
        /// Читаем данные из базы для ЭГП и заносим их в свойства
        /// </summary>
        private void GetEgp()
        {
            var data = db.Egps
                .Where(w => w.FWpointId == Watchpoints.WpointId)
                .Include(f => f.FEgpelementNavigation)
                .Include(fg => fg.FGroupprocessNavigation)
                .Include(ft => ft.FTypeprocessNavigation)
                .Include(fv => fv.FVidprocessNavigation)
                .Include(us => us.FUser)
                .ToList();

            if (data.Count != 0)
            {


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
                _egpDate = new DateTime(egp.DataEgp.Value.Year, egp.DataEgp.Value.Month, egp.DataEgp.Value.Day);
                _egpUserName = egp.FUser.UserName;
                _egpDeep = egp.EgpDeep;
                _egpWidth = egp.EgpWidth;
                _egpLength = egp.EgpLength;
                _egpVolume = egp.EgpVolume;
                _egpArea = egp.EgpArea;
                _egpSpeed = egp.EgpSpeed;
                _egpDescription = egp.EgpDescription;
                for (int i = 0; i < _egpgroupprocess.Count; i++)
                {
                    if (_egpgroupprocess[i].IdGroupprocces == egp.FGroupprocess)
                        SelectedGroupprocce = _egpgroupprocess[i];
                }
                for (int i = 0; i < _egptypeprocess.Count; i++)
                {
                    if (_egptypeprocess[i].IdTypeprocess == egp.FTypeprocess)
                        SelectedTypeprocess = _egptypeprocess[i];
                }
                for (int i = 0; i < _egpElement.Count; i++)
                {
                    if (_egpElement[i].IdEgpelement == egp.FEgpelement)
                        SelectedEgpElement = _egpElement[i];
                }
            }
            data.Clear();

        }


        /// <summary>
        /// Читаем данные из базы для Точки наблюдения и заносим их в свойства
        /// </summary>
        private void GetWatchpoints()
        {

            var data = db.Watchpoints
                .Where(w => w.WpointId == PassedParameter)
                .Include(c => c.WpointCoordinates)
                .Include(r => r.Route)
                .ToList();

            foreach (var item in data)
            {
                Watchpoints = new Watchpoint
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
            _wnote = Watchpoints.WpointNote;
            if(Watchpoints.WpointDateAdd is not null)
                _wDateStart = new DateTime(Watchpoints.WpointDateAdd.Value.Year, Watchpoints.WpointDateAdd.Value.Month, Watchpoints.WpointDateAdd.Value.Day);
            _wRoute = Watchpoints.Route.RouteName;
            _wnumber = Watchpoints.WpointNumber.ToString();

            if (Watchpoints.WpointLocation == null)
                _wlocation = "Нет сведений";
            _wlocation = Watchpoints.WpointLocation;

            foreach (var item in Watchpoints.WpointCoordinates)
            {
                _wpointX = item.WpCoordinatesX;
            }

            foreach (var item in Watchpoints.WpointCoordinates)
            {
                _wpointY = item.WpCoordinatesY;
            }

            foreach (var item in Watchpoints.WpointCoordinates)
            {
                _pointZ = item.WpCoordinatesZ;
            }


            for (int i = 0; i < _formareliefa.Count; i++)
            {
                if (_formareliefa[i].IdFormareliefa == Watchpoints.IdFormareliefa)
                    SelectedFormarelirfa = _formareliefa[i];
            }

            for (int i = 0; i < _typereliefa.Count; i++)
            {
                if (_typereliefa[i].IdTypereliefa == Watchpoints.IdTypereliefa)
                    SelectedTypereliefa = _typereliefa[i];
            }

            for (int i = 0; i < _sybtypereliefa.Count; i++)
            {
                if (_sybtypereliefa[i].IdSubtypereliefa == Watchpoints.IdSubtypereliefa)
                    SelectedSybtypereliefa = _sybtypereliefa[i];
            }

            for (int i = 0; i < _heightreliefa.Count; i++)
            {
                if (_heightreliefa[i].IdHeightreliefa == Watchpoints.IdHeightreliefa)
                    SelectedHeightreliefa = _heightreliefa[i];
            }
            for (int i = 0; i < _exposition.Count; i++)
            {
                if (_exposition[i].IdSprexposition == Watchpoints.IdExposition)
                    SelectedExposition = _exposition[i];
            }
            for (int i = 0; i < _slope.Count; i++)
            {
                if (_slope[i].IdSlope == Watchpoints.IdSlope)
                    SelectedSlope = _slope[i];
            }

            for (int i = 0; i < _formariver.Count; i++)
            {
                if (_formariver[i].IdFormariver == Watchpoints.IdFormariver)
                    SelectedFormariver = _formariver[i];
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
            GetWatchpoints();
            LocatorStatic.Data.PageHeader += $" Точка наблюдения: {Watchpoints.WpointId}";
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
