namespace ViewModel.Fiche
{
    using System;

    using Dal;

    using DalContract.Models;

    using ViewModel.Cotation;
    using ViewModel.Patient;
    using ViewModel.Seance;
    using ViewModel.Shared;

    public class FicheViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private bool isSelected;

        public event EventHandler SavedEvent;

        public event EventHandler CloseEvent;

        public event EventHandler IsSelectedChanged;

        public FicheViewModel(Fiche entity, ModelContext context)
        {
            this.Entity = entity;
            this.context = context;

            this.SaveCommand = new DelegateCommand(this.Save);
            this.CloseCommand = new DelegateCommand(this.Close);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.SwitchIsSelectedCommand = new DelegateCommand(() => this.IsSelected = !this.IsSelected);

            // this.CotationsViewModel = new CotationsViewModel(context) { FicheViewModel = this };

            this.Refresh();
        }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public IDelegateCommand SwitchIsSelectedCommand { get; }

        public Fiche Entity { get; private set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.SetProperty(ref this.isSelected, value);
                this.IsSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public CotationsViewModel CotationsViewModel { get; }

        public PatientViewModel PatientViewModel { get; set; }

        public SeanceViewModel SeanceViewModel { get; set; }

        private void Save()
        {
            if (this.Entity == null)
            {
                this.Entity = this.context.Fiches.Create();
                this.context.Fiches.Add(this.Entity);
            }

            this.Entity.Patient = this.PatientViewModel.Entity;
            this.Entity.Seance = this.SeanceViewModel.Entity;

            this.context.SaveChanges();

            this.Refresh();

            this.SavedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Close()
        {
            this.Refresh();

            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Refresh()
        {
            this.PatientViewModel = new PatientViewModel(this.Entity?.Patient, this.context);
            this.SeanceViewModel = new SeanceViewModel(this.Entity?.Seance, this.context);
        }
    }
}
