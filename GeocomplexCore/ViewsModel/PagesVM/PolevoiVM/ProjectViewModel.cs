using Egor92.MvvmNavigation;
using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.PagesVM.PolevoiVM
{
    internal class ProjectViewModel : ViewModel

    {
        private NavigationManager navigationManager;

        public ProjectViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }
}
