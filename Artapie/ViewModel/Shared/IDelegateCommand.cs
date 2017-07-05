namespace ViewModel.Shared
{
    using System.Windows.Input;

    public interface IDelegateCommand : ICommand
    {
        #region Public Methods and Operators

        void RaiseCanExecuteChanged();

        #endregion
    }
}