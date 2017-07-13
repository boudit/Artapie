namespace ViewModel
{
    using System;
    using System.IO;

    using Dal;

    using ViewModel.Item;
    using ViewModel.Patient;
    using ViewModel.Shared;

    public class MainWindowViewModel : NotifyPropertyChangedViewModel
    {
        private IClosable currentChild;

        private readonly PatientsViewModel patientsViewModel;

        private readonly ItemsViewModel itemsViewModel;
        
        public MainWindowViewModel()
        {
            var context = new ModelContext(Path.GetFullPath("./Database.mdf"));

            this.patientsViewModel = new PatientsViewModel(context);
            this.itemsViewModel = new ItemsViewModel(context);
            
            this.DisplayPatientsCommand = new DelegateCommand(this.DisplayPatients);
            this.DisplayItemsCommand = new DelegateCommand(this.DisplayItems);
        }

        public IDelegateCommand DisplayPatientsCommand { get; private set; }

        public IDelegateCommand DisplayItemsCommand { get; private set; }

        public IClosable CurrentChild
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

        private void DisplayPatients()
        {
            this.patientsViewModel.RefreshCommand.Execute(null);
            this.CurrentChild = this.patientsViewModel;
        }

        private void DisplayItems()
        {
            this.itemsViewModel.RefreshCommand.Execute(null);
            this.CurrentChild = this.itemsViewModel;
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
