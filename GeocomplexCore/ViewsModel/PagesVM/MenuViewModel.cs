using Egor92.MvvmNavigation;
using GeocomplexCore.Views.Pages;
using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.PagesVM
{
    internal class MenuViewModel : ViewModel
    {
        private NavigationManager navigationManager;

        public MenuViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }
}
