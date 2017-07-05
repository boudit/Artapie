namespace ViewModel
{
    using System.IO;

    using Dal;

    using ViewModel.Item;
    using ViewModel.Patient;
    using ViewModel.Shared;

    public class MainWindowViewModel : NavigationViewModel
    {
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

        private void DisplayPatients()
        {
            this.patientsViewModel.LoadCommand.Execute(null);
            this.CurrentChild = this.patientsViewModel;
        }

        private void DisplayItems()
        {
            this.itemsViewModel.LoadCommand.Execute(null);
            this.CurrentChild = this.itemsViewModel;
        }
    }
}
