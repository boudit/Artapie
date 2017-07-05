namespace ViewModel.Shared
{
    using System;
    using System.Collections.ObjectModel;

    public class NavigationViewModel : NotifyPropertyChangedViewModel
    {
        private NavigationViewModel currentChild;

        private event EventHandler CloseEvent;

        public NavigationViewModel()
        {
            this.Children = new ObservableCollection<NavigationViewModel>();

            this.CloseCommand = new DelegateCommand(this.Close);
        }

        public NavigationViewModel CurrentChild
        {
            get
            {
                return this.currentChild;
            }

            set
            {
                this.RemoveHandlerOnCurrentChild();

                this.SetProperty(ref this.currentChild, value);

                this.AddHandlerOnCurrentChild();
            }
        }

        public ObservableCollection<NavigationViewModel> Children { get; }

        public IDelegateCommand CloseCommand { get; }
        
        private void Close()
        {
            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void AddHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent += this.CurrentChild_OnCloseEvent;
        }

        protected virtual void RemoveHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent -= this.CurrentChild_OnCloseEvent;
        }

        private void CurrentChild_OnCloseEvent(object sender, EventArgs eventArgs)
        {
            this.CurrentChild = null;
        }
    }
}
