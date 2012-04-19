using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace ICOMI_SPClient.Utilities
{
    public class GenericCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> executeMethod;
        private readonly Func<T, bool> canExecuteMethod;        

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public GenericCommand(Action<T> executeMethod)
        {
            this.executeMethod = executeMethod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public GenericCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
        }


        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return canExecuteMethod == null ? true : canExecuteMethod(parameter);
        }

      
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(T parameter)
        {
           
            if (executeMethod != null)
            {
                Cursor keepIt = Mouse.OverrideCursor;

                try
                {
                    Application.Current.MainWindow.Cursor = Cursors.Wait;

                    executeMethod(parameter);
                }
                finally
                {
                    Application.Current.MainWindow.Cursor = keepIt;
                }
            }
        }

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        bool ICommand.CanExecute(object parameter)
        {            
            return CanExecute((T)parameter);
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        void ICommand.Execute(object parameter)
        {            
            Execute((T)parameter);
        }
        #endregion
    }
}
