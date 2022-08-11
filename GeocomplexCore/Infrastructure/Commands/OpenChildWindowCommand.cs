using GeocomplexCore.ViewsModel.WindowsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeocomplexCore.Infrastructure.Commands
{
     class OpenChildWindowCommand : CommandOpen
    {
        public OpenChildWindowCommand(MainWindowViewModel mainWindowVeiwModel) :base(mainWindowVeiwModel)
        {
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            var displayRootRegistry = (Application.Current as App).displayRootRegistry;

            var otherWindowViewModel = new AddWindowsViewModel();
            displayRootRegistry.ShowPresentation(otherWindowViewModel);
        }
    }
}
