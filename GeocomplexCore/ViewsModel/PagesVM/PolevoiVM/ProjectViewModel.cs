using Egor92.MvvmNavigation;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Model;
using GeocomplexCore.ViewsModel.Base;
using GeocomplexCore.ViewsModel.WindowsVM;
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

        /// <summary>
        /// Загрузка данных из базы в колекцию по модели ProjectModel
        /// </summary>
        private ObservableCollection<ProjectModel> _projectdata = new ObservableCollection<ProjectModel>();
        public ObservableCollection<ProjectModel> ProjectData
        {

            get
            {
                using (GeocomplexContext db = new GeocomplexContext())
                {
                    _projectdata = db.Projects.Join(db.Organizations,
                                     p => p.PrgOrganization,
                                     o => o.OrgId,
                                     (p, o) => new ProjectModel // результат
                                     {
                                         PrgId = p.PrgId,
                                         PrgName = p.PrgName,
                                         PrgOrganization = o.OrgName,
                                         PrgDate = p.PrgDate

                                     }).ToObservableCollection();

                    return _projectdata;

                }
            }
            set
            {
                _projectdata = value;
                OnPropertyChanged("ProjectData");
            }
        }

        /// <summary>
        /// Выделенный объект
        /// </summary>
        private ProjectModel? _selectedProject;
        public ProjectModel? SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }

        }


        /// <summary>
        /// Поле для ввода текста фильтрации 
        /// </summary>
        private string? _textToFilter;
        public  string? TextToFilter { get => _textToFilter;
            set 
            {
                _textToFilter = value;
                OnPropertyChanged("TextToFilter");
                // Проводим фильтрацию
                ProjectCollection.Filter = FilterByName;
            }
        }

        /// <summary>
        /// Выводим в Datagrid для просмотра и фильтрации 
        /// </summary>
        private ICollectionView? _projectCollection;
        public ICollectionView? ProjectCollection { get => _projectCollection; set => Set(ref _projectCollection, value); }
        #endregion
        MainWindowViewModel mainWindowViewModel;


        public ProjectViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;

            // Обворачиваем ObservableCollection в ICollectionView
            ProjectCollection = CollectionViewSource.GetDefaultView(ProjectData);

           
            GoDistrictPageCommand = new LamdaCommand(OnGoDistrictPageCommandExcuted, CanGoDistrictPageCommandExecute);


        }





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
        /// Команда открытия нового окна для добавления проекта 
        /// </summary>
        public ICommand GoDistrictPageCommand { get; }

        private bool CanGoDistrictPageCommandExecute(object p) => true;

        private void OnGoDistrictPageCommandExcuted(object p)
        {            

        }




        #endregion


       



        /// <summary>
        /// Фильтрация 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterByName(object obj)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var objProject = obj as ProjectModel;
                return objProject != null && objProject.PrgName.Contains(TextToFilter);
            }
            return true;

        }

        /// <summary>
        /// Функция для перехода на другую страницу
        /// </summary>
        private void GoNext()
        {
            //navigationManager.Navigate("UserControl2", SelectedProduct.PrgId);
        }

    }
}
