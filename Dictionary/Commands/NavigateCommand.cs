using Dictionary.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Commands
{
    public class NavigateCommand:CommandBase
    {
        private readonly Navigation _navigation;
        public NavigateCommand(Navigation navigation)
        {
            _navigation = navigation;
        }
        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new AdminViewModel(new Models.User());
        }
    }
}
