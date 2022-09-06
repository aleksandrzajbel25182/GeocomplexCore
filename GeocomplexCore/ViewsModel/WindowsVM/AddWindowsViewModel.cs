using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using GeocomplexCore.Properties;
using GeocomplexCore.Views.Pages.AddPages;
using GeocomplexCore.ViewsModel.AddPagesVM;
using GeocomplexCore.ViewsModel.Base;
using System.Windows.Controls;

namespace GeocomplexCore.ViewsModel.WindowsVM
{
    internal class AddWindowsViewModel:ViewModel
    {
   
        private ContentControl _currentVM;

        public ContentControl CurrentVM
        {
            get => _currentVM;
            set =>Set(ref _currentVM,value );
        }


        public AddWindowsViewModel()
        {
          
            _currentVM = new ContentControl();

            var navigationManager = new NavigationManager(_currentVM);

            navigationManager.Register<AddProjectView>("Project", () => new AddProjectViewModel(navigationManager));
            navigationManager.Register<AddDistrictView>("District", () => new AddDistrictViewModel(navigationManager));

            switch (GlobalSet.FlagStatic)
            {
                case "Project":
                    navigationManager.Navigate("Project");
                    break;
                case "District":
                    navigationManager.Navigate("District");
                    break;
                default:
                    break;
            }
            
        }
       

    }
}
