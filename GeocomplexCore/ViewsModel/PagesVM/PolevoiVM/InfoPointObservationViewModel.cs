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


        /// <summary>
        /// Маршрут по которому делалась ТН
        /// </summary>
        private string _wRoute;
        public string WRoute
        {
            get
            {
                _wRoute = Watchpoints.Route.RouteName;
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
                _wnumber = Watchpoints.WpointNumber.ToString();
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
                _wDateStart = new DateTime(Watchpoints.WpointDateAdd.Value.Year, Watchpoints.WpointDateAdd.Value.Month, Watchpoints.WpointDateAdd.Value.Day);
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
                _wnote = Watchpoints.WpointNote;
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
                if (Watchpoints.WpointLocation == null)
                    return _wlocation = "Нет сведений";
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

        //Кординыты X
        private double? _wpointX;
        public double? WpointX
        {
            get
            {
                foreach (var item in Watchpoints.WpointCoordinates)
                {
                    _wpointX = item.WpCoordinatesX;
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
                foreach (var item in Watchpoints.WpointCoordinates)
                {
                    _wpointY = item.WpCoordinatesY;
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
                foreach (var item in Watchpoints.WpointCoordinates)
                {
                    _pointZ = item.WpCoordinatesZ;
                }

                return _pointZ;
            }
            set { _pointZ = value; }
        }

        /// <summary>
        /// Данные из БД по ТН
        /// </summary>
        private Watchpoint _watchpoints;
        private Watchpoint Watchpoints
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.Watchpoints
                        .Where(w => w.WpointId == PassedParameter)
                        .Include(c => c.WpointCoordinates)
                        .Include(r => r.Route)
                        .ToList();

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
                            IdExposition = item.IdExposition,
                            IdFormareliefa = item.IdFormareliefa,
                            IdFormariver = item.IdFormariver,
                            IdHeightreliefa = item.IdHeightreliefa,
                            IdSlope = item.IdSlope,
                            IdSubtypereliefa = item.IdSubtypereliefa,
                            IdTypereliefa = item.IdTypereliefa

                        };
                    }
                    return _watchpoints;


                }
            }
            set => Set(ref _watchpoints, value);
        }


        #region Список Форма рельефа
        private ObservableCollection<GuideFormareliefa> _formareliefa = new ObservableCollection<GuideFormareliefa>();
        public ObservableCollection<GuideFormareliefa> Formareliefa
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideFormareliefas.ToList();
                    foreach (var item in data)
                    {
                        _formareliefa.Add(new GuideFormareliefa
                        {
                            IdFormareliefa = item.IdFormareliefa,
                            NameFormareliefa = item.NameFormareliefa
                        });

                    }
                    for (int i = 0; i < _formareliefa.Count; i++)
                    {
                        if (_formareliefa[i].IdFormareliefa == Watchpoints.IdFormareliefa)
                            SelectedFormarelirfa = _formareliefa[i];
                    }


                    return _formareliefa;
                }
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
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideTypereliefas.ToList();
                    foreach (var item in data)
                    {
                        _typereliefa.Add(new GuideTypereliefa
                        {
                            IdTypereliefa = item.IdTypereliefa,
                            NameTypereliefa = item.NameTypereliefa
                        });

                    }
                    for (int i = 0; i < _typereliefa.Count; i++)
                    {
                        if (_typereliefa[i].IdTypereliefa == Watchpoints.IdTypereliefa)
                            SelectedTypereliefa = _typereliefa[i];
                    }


                    return _typereliefa;
                }
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
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideSubtypereliefas.ToList();
                    foreach (var item in data)
                    {
                        _sybtypereliefa.Add(new GuideSubtypereliefa
                        {
                            IdSubtypereliefa = item.IdSubtypereliefa,
                            NameSubtypereliefa = item.NameSubtypereliefa
                        });

                    }
                    for (int i = 0; i < _sybtypereliefa.Count; i++)
                    {
                        if (_sybtypereliefa[i].IdSubtypereliefa == Watchpoints.IdSubtypereliefa)
                            SelectedSybtypereliefa = _sybtypereliefa[i];
                    }


                    return _sybtypereliefa;
                }
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
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideHeightreliefas.ToList();
                    foreach (var item in data)
                    {
                        _heightreliefa.Add(new GuideHeightreliefa
                        {
                            IdHeightreliefa = item.IdHeightreliefa,
                            NameHeightreliefa = item.NameHeightreliefa
                        });

                    }
                    for (int i = 0; i < _heightreliefa.Count; i++)
                    {
                        if (_heightreliefa[i].IdHeightreliefa == Watchpoints.IdHeightreliefa)
                            SelectedHeightreliefa = _heightreliefa[i];
                    }


                    return _heightreliefa;
                }
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
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideSprexpositions.ToList();
                    foreach (var item in data)
                    {
                        _exposition.Add(new GuideSprexposition
                        {
                            IdSprexposition = item.IdSprexposition,
                            NameSprexposition = item.NameSprexposition
                        });

                    }
                    for (int i = 0; i < _exposition.Count; i++)
                    {
                        if (_exposition[i].IdSprexposition == Watchpoints.IdExposition)
                            SelectedExposition = _exposition[i];
                    }


                    return _exposition;
                }
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
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideSlopes.ToList();
                    foreach (var item in data)
                    {
                        _slope.Add(new GuideSlope
                        {
                            IdSlope = item.IdSlope,
                            NameSlope = item.NameSlope
                        });

                    }
                    for (int i = 0; i < _slope.Count; i++)
                    {
                        if (_slope[i].IdSlope == Watchpoints.IdSlope)
                            SelectedSlope = _slope[i];
                    }


                    return _slope;
                }
            }
            set { _slope = value; }
        }
        public GuideSlope SelectedSlope { get; set; }
        #endregion

        private ObservableCollection<GuideFormariver> _formariver = new ObservableCollection<GuideFormariver>();
        public ObservableCollection<GuideFormariver> Formariver
        {
            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    var data = db.GuideFormarivers.ToList();
                    foreach (var item in data)
                    {
                        _formariver.Add(new GuideFormariver
                        {
                            IdFormariver = item.IdFormariver,
                            NameFormariver = item.NameFormariver
                        });

                    }
                    for (int i = 0; i < _formariver.Count; i++)
                    {
                        if (_formariver[i].IdFormariver == Watchpoints.IdFormariver)
                            SelectedFormariver = _formariver[i];
                    }


                    return _formariver;
                }
            }
            set { _formariver = value; }
        }
        public GuideFormariver SelectedFormariver { get; set; }


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
            LocatorStatic.Data.PageHeader += $" Точка наблюдения: {Watchpoints.WpointId}";
        }


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
        #endregion
        // ---------------------------------------------------------------------------------------------------------------------
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
