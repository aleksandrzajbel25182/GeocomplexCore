using Egor92.MvvmNavigation;
using GeocomplexCore.BD.Context;
using GeocomplexCore.ViewsModel.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.AddPagesVM
{
    internal class AddProjectViewModel : ViewModel
    {

        #region Parametrs/параметры
        private NavigationManager navigationManager;
      
        /// <summary>
        /// Наименование проект
        /// </summary>
        private int _projectName;
        public int ProjectName{ get => _projectName; set => Set(ref _projectName, value); }
        
        /// <summary>
        /// Список организаций 
        /// </summary>
        ObservableCollection<string> _orgname = new ObservableCollection<string>();
        public ObservableCollection<string> OrgName { get => _orgname; set => Set(ref _orgname, value); }


        #endregion

        /// <summary>
        /// Заполнение списка организаций из базы
        /// </summary>
        /// <returns></returns>
        public async Task GetObjectsAsync()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {
                await db.Organizations.ForEachAsync(p => 
                {

                    _orgname.Add(p.OrgName);
                });
            }
        }

        public AddProjectViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }   
}
