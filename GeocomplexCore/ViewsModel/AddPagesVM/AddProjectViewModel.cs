using Egor92.MvvmNavigation;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.AddPagesVM
{
    internal class AddProjectViewModel : ViewModel
    {

        #region Parametrs/параметры
        private NavigationManager navigationManager;
      
        /// <summary>
        /// Наименование проект
        /// </summary>
        private string _projectName;
        public string ProjectName { get => _projectName; set => Set(ref _projectName, value); }
        
        /// <summary>
        /// Список организаций 
        /// </summary>
        ObservableCollection<Org> _orgname = new ObservableCollection<Org>();
        public ObservableCollection<Org> OrgName { get => _orgname; set => Set(ref _orgname, value); }

        private Org? _selectedOrg;
        public Org? SelectedOrg
        {
            get { return _selectedOrg; }
            set
            {
                _selectedOrg = value;
                OnPropertyChanged("SelectedOrg");
            }

        }


        #endregion


        #region Command/Команды

        public ICommand AddNewProjectCommand { get; }

        private bool CanAddNewProjectCommandExecute(object p) => true;

        private void OnAddNewProjectCommandExcuted(object p)
        {
            try
            {
                if (!String.IsNullOrEmpty(ProjectName) && !SelectedOrg.IsNull())
                {
                    AddProject();
                    MessageService.ShowMessage("Новый проект занесен в базу");
                }
                else
                {
                    MessageService.ShowMessageValidation();
                }
              
            }
            catch (System.Exception ex )
            {

                MessageService.ShowMessage(ex.Message);
            }
        }

        public async void AddProject()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {

                Project project;

                project = new Project
                {

                    PrgName = ProjectName,
                    PrgDate = DateOnly.FromDateTime(DateTime.Now),
                    PrgOrganization = SelectedOrg.Id
                };
                db.Add(project);
                await db.SaveChangesAsync();
            }
        }


        #endregion

        /// <summary>
        /// Заполнение списка организаций из базы
        /// </summary>
        /// <returns></returns>
        private async void GetObjectsAsync()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {

                var val = await db.Organizations
                       .Where(p => p.OrgName != " Нет в списке. Добавить в примечание")
                       .ToListAsync();
                foreach (var item in val)
                {
                    _orgname.Add(new Org(item.OrgId, item.OrgName));
                }
            }
        }



        public AddProjectViewModel(NavigationManager navigationManager)
        {
            GetObjectsAsync();
            this.navigationManager = navigationManager;
            AddNewProjectCommand = new LamdaCommand(OnAddNewProjectCommandExcuted, CanAddNewProjectCommandExecute);
        }


    }

    class Org
    {
        private int _id;
        private string _name;
        public int Id{ get => _id; set => _id= value; }
        public string Name{ get => _name; set => _name= value; }
        public Org(int id, string name)
        {
            _id = id;
            _name = name;
        }

    }
}
