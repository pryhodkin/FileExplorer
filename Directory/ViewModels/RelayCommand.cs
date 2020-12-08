using System;
using System.Windows.Input;

namespace FileExplorer
{
    /// <summary>
    /// A basic command that runs an Action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private members

        /// <summary>
        /// The action to run.
        /// </summary>
        private Action execute;

        #endregion

        #region Public events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RelayCommand(Action action)
        {
            execute = action;
        }

        #endregion

        #region Command methods

        /// <summary>
        /// A relay command can always execute.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the commands Action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            execute();
        }

        #endregion
    }
}
