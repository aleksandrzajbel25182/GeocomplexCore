using Egor92.MvvmNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.ViewsModel.AddPagesVM
{
    internal class AddDistrictViewModel
    {
        private NavigationManager navigationManager;

        public AddDistrictViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }
}
