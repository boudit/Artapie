namespace ViewModel.Patient
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public sealed class PatientViewModel : EntityViewModel<Patient>
    {
        private string prenom;

        private string nom;
        
        public PatientViewModel(Patient entity, ModelContext context)
            : base(entity, context)
        {
            this.Fiches = new FichesViewModel(context) { PatientViewModel = this };

            this.Refresh();
        }
        
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
        
        public FichesViewModel Fiches { get; }

        public override string DisplayValue
        {
            get
            {
                return $"{this.Prenom} {this.Nom}";
            }
        }

        protected override void SetValuesOnEntity()
        {
            this.Entity.Nom = this.Nom;
            this.Entity.Prenom = this.Prenom;
        }

        protected override void Refresh()
        {
            this.Nom = this.Entity?.Nom;
            this.Prenom = this.Entity?.Prenom;
        }
    }
}
