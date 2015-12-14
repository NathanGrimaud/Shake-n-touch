using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeNTouch.ViewModel.MVVM
{
    public class RelayCommandWithParam<T> : ICommand
    {
        private readonly Action<T> _actionAExecuter = null;
        public event EventHandler CanExecuteChanged;

        public RelayCommandWithParam(Action<T> action)
        {
            _actionAExecuter = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            T myParam;
            if (parameter is T)
                myParam = (T)parameter;
            else
            {
                try
                {
                    myParam = (T)Convert.ChangeType(parameter, typeof(T));
                }
                catch (InvalidCastException)
                {
                    myParam = default(T);
                }
            }
            _actionAExecuter(myParam);
        }
    }
}
