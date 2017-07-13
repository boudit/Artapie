namespace ViewModel.Patient
{
    using System;

    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class PatientViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private string prenom;

        private string nom;

        private bool isSelected;

        public event EventHandler SavedEvent;

        public event EventHandler CloseEvent;

        public event EventHandler IsSelectedChanged;

        public PatientViewModel(Patient entity, ModelContext context)
        {
            this.Entity = entity;
            this.context = context;

            this.SaveCommand = new DelegateCommand(this.Save);
            this.CloseCommand = new DelegateCommand(this.Close);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.SwitchIsSelectedCommand = new DelegateCommand(() => this.IsSelected = !this.IsSelected);

            this.Fiches = new FichesViewModel(context) { PatientViewModel = this };

            this.Refresh();
        }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand RefreshCommand { get; }
        
        public IDelegateCommand CloseCommand { get; }

        public IDelegateCommand SwitchIsSelectedCommand { get; }

        public Patient Entity { get; private set; }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.SetProperty(ref this.prenom, value);
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.SetProperty(ref this.nom, value);
            }
        }

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

        public FichesViewModel Fiches { get; }

        public string DisplayValue
        {
            get
            {
                return $"{this.Prenom} {this.Nom}";
            }
        }
        
        private void Save()
        {
            if (this.Entity == null)
            {
                this.Entity = this.context.Patients.Create();
                this.context.Patients.Add(this.Entity);
            }

            this.Entity.Nom = this.Nom;
            this.Entity.Prenom = this.Prenom;

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
            this.Nom = this.Entity?.Nom;
            this.Prenom = this.Entity?.Prenom;
        }
    }
}
