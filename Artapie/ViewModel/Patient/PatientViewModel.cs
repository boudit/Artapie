namespace ViewModel.Patient
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class PatientViewModel : BaseEntityViewModel<Patient>
    {
        private string prenom;

        private string nom;

        public PatientViewModel(NavigationViewModel parent, Patient entity, ModelContext context)
            : base(parent, entity, context)
        {
            this.Fiches = new FichesViewModel(this, context) { PatientViewModel = this };
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

        protected override void InjectIntoEntity(Patient entity)
        {
            entity.Nom = this.Nom;
            entity.Prenom = this.Prenom;
        }

        protected override void PopulateFromEntity(Patient entity)
        {
            this.Nom = entity.Nom;
            this.Prenom = entity.Prenom;
        }
    }
}
