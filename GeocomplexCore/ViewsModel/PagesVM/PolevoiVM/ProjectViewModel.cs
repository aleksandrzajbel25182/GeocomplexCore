using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.DAL.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.WindowsVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class ProjectViewModel : ViewModel

    {
        #region Parametrs/ Параметры 


        private NavigationManager navigationManager;
        MainWindowViewModel mainWindowViewModel;

        //Временная переменная для хранения выделенного ID "Проекта" 
        private int _selectIDproject;
        //Временная переменная для хранения выделенного "Проекта"
        private string _selectNameproject;

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
                GlobalSet.IdProjectStatic = _selectIDproject;
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


        #region Открытие окна/Command open
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

        #endregion


        /// <summary>
        /// Команда замены таблицы проектов на учаcтки
        /// </summary>
        public ICommand GoDistrictPageCommand { get; }

        private bool CanGoDistrictPageCommandExecute(object p) => true;

        private void OnGoDistrictPageCommandExcuted(object p)
        {
            if (GlobalSet.FlagStatic == "District")
            {
                _selectIDproject = SelecetedItem.Id;
                GoNextNavigate(_selectIDproject);
            }
            else
            {
                LocatorStatic.Data.PageHeader = $"Участки проекта: {SelecetedItem.Name}";
                _selectIDproject = SelecetedItem.Id;
                _selectNameproject = SelecetedItem.Name;
                DataCol.Clear();
                CollectionData.Filter = null;
                CollectionData.Refresh();
                CollectionData = CollectionViewSource.GetDefaultView(DisttrictData());
                GlobalSet.FlagStatic = "District";
                if(CollectionData.Cast<object>().Count() == 0)
                    MessageService.ShowMessageInformation($"Участков по проекту {_selectNameproject} в базе данных нету!");
            }

        }


        public ICommand UpdateCollectionCommand { get; }

        private bool CanUpdateCollectionCommandExecute(object p) => true;

        private void OnUpdateCollectionCommandExcuted(object p)
        {
            if (GlobalSet.FlagStatic == "District")
            {
                DataCol.Clear();
                CollectionData = CollectionViewSource.GetDefaultView(DisttrictData());
            }
            else
            {
                DataCol.Clear();
                CollectionData = CollectionViewSource.GetDefaultView(ProjectData());
            }
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
                    GlobalSet.FlagStatic = "Project";
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
                var dat = db.Districts.Where(r => r.PrgId == _selectIDproject).Include(us => us.IdUserNavigation).ToList();
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

                var dat = db.Projects.Include(us => us.PrgOrganizationNavigation).ToList();
                foreach (var item in dat)
                {
                    _datacol.Add(
                        new ModelData
                    {
                        Id = item.PrgId,
                        Name = item.PrgName,
                        UsName = item.PrgOrganizationNavigation.OrgName,
                        DateAdd = item.PrgDate

                    });
                }               

                return _datacol;

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
        private void GoNextNavigate(int selectedId)
        {
            navigationManager.Navigate("InfoDistrict", selectedId);
        }

        /// <summary>
        /// Функция перехода на страницу назад
        /// </summary>
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
            GlobalSet.FlagStatic = "Project";
            // Обворачиваем ObservableCollection в ICollectionView
            CollectionData = CollectionViewSource.GetDefaultView(ProjectData());
            //Сортируем по возврастанию id
            CollectionData.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            GoDistrictPageCommand = new LamdaCommand(OnGoDistrictPageCommandExcuted, CanGoDistrictPageCommandExecute);
            BackCommand = new LamdaCommand(OnBackCommandExcuted, CanBackCommandExecute);
            UpdateCollectionCommand = new LamdaCommand(OnUpdateCollectionCommandExcuted, CanUpdateCollectionCommandExecute);

        }


    }
}
