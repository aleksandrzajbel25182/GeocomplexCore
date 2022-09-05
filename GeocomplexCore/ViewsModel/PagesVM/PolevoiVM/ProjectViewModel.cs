using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.WindowsVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class ProjectViewModel : ViewModel

    {
        #region Parametrs/ Параметры 


        private NavigationManager navigationManager;
        MainWindowViewModel mainWindowViewModel;

        //Временная переменная для хранения выделенного "Проекта"
        private int SelecetedID;
        //Данная переменная служит для того что бы знать какие данные загруженны на данный момент. 0- проекты 2 - участки
        private int Peeremen = 0;

        /// <summary>
        /// Загрузка данных из базы в колекцию по модели ProjectModel
        /// </summary>
        private ObservableCollection<ModelData> _datacol = new ObservableCollection<ModelData>();
        public ObservableCollection<ModelData> DataCol
        {

            get
            {
                return _datacol;
            }
            set
            {
                _datacol = value;
                OnPropertyChanged("DataCol");
            }
        }

        /// <summary>
        /// Выделенный объект
        /// </summary>
        private ModelData? _selecteditem;
        public ModelData? SelecetedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                OnPropertyChanged("SelecetedItem");
            }

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

        /// <summary>
        /// Выводим в Datagrid для просмотра и фильтрации 
        /// </summary>
        private ICollectionView? _collectiondata;
        public ICollectionView? CollectionData { get => _collectiondata; set => Set(ref _collectiondata, value); }


        #endregion



        #region Commands/Команды


        /// <summary>
        /// Команда открытия нового окна для добавления проекта 
        /// </summary>
        private ICommand _openChildWindow;

        // Свойства доступные только для чтения для обращения к командам и их инициализации
        public ICommand OpenChildWindow
        {
            get
            {
                if (_openChildWindow == null)
                {
                    _openChildWindow = new OpenChildWindowCommand(mainWindowViewModel);
                }
                return _openChildWindow;
            }
        }


        /// <summary>
        /// Команда замены таблицы проектов на учаcтки
        /// </summary>
        public ICommand GoDistrictPageCommand { get; }

        private bool CanGoDistrictPageCommandExecute(object p) => true;

        private void OnGoDistrictPageCommandExcuted(object p)
        {
            LocatorStatic.Data.PageHeader = $"Участки проекта: {SelecetedItem.Name}";
            SelecetedID = SelecetedItem.Id;
            DataCol.Clear();
            CollectionData = CollectionViewSource.GetDefaultView(DisttrictData());
        }



        public ICommand BackCommand { get; }

        private bool CanBackCommandExecute(object p) => true;

        private void OnBackCommandExcuted(object p)
        {
            switch (Peeremen)
            {
                case 0:
                    GoBackNavigate();
                    LocatorStatic.Data.PageHeader = "";
                    break;
                case 1:
                    GoBackNavigate();
                    LocatorStatic.Data.PageHeader = "";
                    break;
                case 2:
                    DataCol.Clear();
                    CollectionData = CollectionViewSource.GetDefaultView(ProjectData());
                    LocatorStatic.Data.PageHeader = "Проекты";
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Function/Функции


        /// <summary>
        /// Загрузка "Участков" из базы данных по выбранному "Проекту"
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModelData> DisttrictData()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {
                var dat = db.Districts.Where(r => r.PrgId == SelecetedID).Include(us => us.IdUserNavigation).ToList();
                foreach (var item in dat)
                {
                    _datacol.Add(new ModelData
                    {
                        Id = item.IdDistrict,
                        Name = item.NameDistrict,
                        UsName = item.IdUserNavigation.UserName,
                        DateAdd = item.DateAddDistrict

                    });
                }
                Peeremen = 2;
                return _datacol;

            }

        }

        /// <summary>
        /// Загрузка "Проектов" из базы данных
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ModelData> ProjectData()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {
                Peeremen = 1;
                return _datacol = db.Projects.Join(db.Organizations,
                                 p => p.PrgOrganization,
                                 o => o.OrgId,
                                 (p, o) => new ModelData // результат
                                 {
                                     Id = p.PrgId,
                                     Name = p.PrgName,
                                     UsName = o.OrgName,
                                     DateAdd = p.PrgDate

                                 }).ToObservableCollection();



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
                var objProject = obj as ModelData;
                return objProject != null && objProject.Name.Contains(TextToFilter);
            }
            return true;

        }

        /// <summary>
        /// Функция для перехода на другую страницу
        /// </summary>
        private void GoNextNavigate()
        {
            //navigationManager.Navigate("DistrictPage", SelecetedItem.Id);
        }
        private void GoBackNavigate()
        {
            navigationManager.Navigate("Menu");
        }


        #endregion




        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="navigationManager"></param>
        public ProjectViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;

            // Обворачиваем ObservableCollection в ICollectionView
            CollectionData = CollectionViewSource.GetDefaultView(ProjectData());
            GoDistrictPageCommand = new LamdaCommand(OnGoDistrictPageCommandExcuted, CanGoDistrictPageCommandExecute);
            BackCommand = new LamdaCommand(OnBackCommandExcuted, CanBackCommandExecute);
        }


    }
}
