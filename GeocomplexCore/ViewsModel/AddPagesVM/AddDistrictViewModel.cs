using Egor92.MvvmNavigation;
using GeocomplexCore.BD;
using GeocomplexCore.BD.Context;
using GeocomplexCore.Infrastructure.Commands;
using GeocomplexCore.Properties;
using GeocomplexCore.Service;
using GeocomplexCore.Views.Windows;
using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeocomplexCore.ViewsModel.AddPagesVM
{
    internal class AddDistrictViewModel : ViewModel
    {
        private NavigationManager navigationManager;

        private string _namedis;

        public string NameDist
        {
            get => _namedis;
            set => Set(ref _namedis, value);
        }



        public ICommand AddDiscrictCommand { get; }

        private bool CanAddDiscrictCommandExecute(object p) => true;

        private void OnAddDiscrictCommandExcuted(object p)
        {
            try
            {
                if (!String.IsNullOrEmpty(NameDist))
                {
                    AddDiscrict();
                    MessageService.ShowMessage("Новый участок занесен в базу");

                }
                else
                {
                    MessageService.ShowMessageValidation();
                }
                
            }
            catch (System.Exception ex)
            {

                MessageService.ShowMessage(ex.Message);
            }
        }


        public async void AddDiscrict()
        {
            using (GeocomplexContext db = new GeocomplexContext())
            {

                District district;

                district = new District
                {

                    NameDistrict = NameDist,
                    DateAddDistrict = DateOnly.FromDateTime(DateTime.Now),
                    IdUser = Convert.ToInt32(GlobalSet.staticUserID),
                    PrgId = GlobalSet.IdProjectStatic
                };
                db.Add(district);
                await db.SaveChangesAsync();
            }
        }

        public AddDistrictViewModel(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
            AddDiscrictCommand = new LamdaCommand(OnAddDiscrictCommandExcuted, CanAddDiscrictCommandExecute);
        }

    }
}

