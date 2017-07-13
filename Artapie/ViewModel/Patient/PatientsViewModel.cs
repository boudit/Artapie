namespace ViewModel.Patient
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class PatientsViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;
        
        private PatientViewModel currentChild;

        public event EventHandler CloseEvent;

        public PatientsViewModel(ModelContext context)
        {
            this.context = context;
            this.Children = new ObservableCollection<PatientViewModel>();

            this.CreateCommand = new DelegateCommand(this.Create);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.CloseCommand = new DelegateCommand(this.Close);
        }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CreateCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public ObservableCollection<PatientViewModel> Children { get; }

        public PatientViewModel CurrentChild
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

        private void Create()
        {
            var viewModel = this.CreateViewModel(null);

            this.Children.Add(viewModel);

            this.CurrentChild = viewModel;
        }

        private void Refresh()
        {
            this.Children.Select(
                c =>
                    {
                        this.RemoveHandlerOnChild(c);
                        return true;
                    });
            this.Children.Clear();

            var entities = this.context.Set<Patient>()
                .ToList();
            entities.ForEach(ent => this.AddViewModel(ent));
        }

        private void Close()
        {
            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private PatientViewModel AddViewModel(Patient entity)
        {
            var viewModel = this.CreateViewModel(entity);

            this.Children.Add(viewModel);
            this.AddHandlerOnChild(viewModel);

            return viewModel;
        }

        private PatientViewModel CreateViewModel(Patient entity)
        {
            return new PatientViewModel(entity, this.context);
        }

        private void AddHandlerOnChild(PatientViewModel child)
        {
            child.IsSelectedChanged += this.Child_OnIsSelectedChanged;
        }

        private void RemoveHandlerOnChild(PatientViewModel child)
        {
            child.IsSelectedChanged -= this.Child_OnIsSelectedChanged;
        }

        private void AddHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent += this.CurrentChild_OnCloseEvent;
            this.currentChild.SavedEvent += this.CurrentChild_OnSavedEvent;
        }

        private void RemoveHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent -= this.CurrentChild_OnCloseEvent;
            this.currentChild.SavedEvent -= this.CurrentChild_OnSavedEvent;
        }
        
        private void CurrentChild_OnCloseEvent(object sender, EventArgs eventArgs)
        {
            this.CurrentChild = null;
        }

        private void CurrentChild_OnSavedEvent(object sender, EventArgs eventArgs)
        {
            this.RefreshCommand.Execute(null);
        }

        private void Child_OnIsSelectedChanged(object sender, EventArgs eventArgs)
        {
            this.CurrentChild = sender as PatientViewModel;
        }
    }
}
