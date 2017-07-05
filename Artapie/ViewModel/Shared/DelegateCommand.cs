namespace ViewModel.Shared
{
    using System;
    using System.Diagnostics.Contracts;

    internal class DelegateCommand : IDelegateCommand
    {
        #region Fields

        private readonly Func<bool> canExecute;

        private readonly Action execute;

        #endregion

        #region Constructors and Destructors

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            Contract.Requires(execute != null);

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }

    internal class DelegateCommand<T> : IDelegateCommand
    {
        #region Fields

        private readonly Func<T, bool> canExecute;

        private readonly Action<T> execute;

        #endregion

        #region Constructors and Destructors

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            Contract.Requires(execute != null);

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(GetTypedParam(parameter));
        }

        public void Execute(object parameter)
        {
            this.execute(GetTypedParam(parameter));
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private static T GetTypedParam(object parameter)
        {
            if (parameter is T)
            {
                return (T)parameter;
            }

            return default(T);
        }

        #endregion
    }
}