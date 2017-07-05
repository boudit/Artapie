namespace ViewModel.Patient
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class PatientViewModel : BaseEntityViewModel<Patient>
    {
        public PatientViewModel(Patient entity, ModelContext context)
            : base(entity, context)
        {
            this.Fiches = new FichesViewModel(context) { Parent = entity };
        }

        public string Prenom
        {
            get
            {
                return this.CurrentEntityEntry.Entity.Prenom;
            }

            set
            {
                if (this.CurrentEntityEntry.Entity.Prenom == value)
                {
                    return;
                }

                this.CurrentEntityEntry.Entity.Prenom = value;
                this.OnPropertyChanged();
            }
        }

        public string Nom
        {
            get
            {
                return this.CurrentEntityEntry.Entity.Nom;
            }

            set
            {
                if (this.CurrentEntityEntry.Entity.Nom == value)
                {
                    return;
                }

                this.CurrentEntityEntry.Entity.Nom = value;
                this.OnPropertyChanged();
            }
        }

        public FichesViewModel Fiches { get; }
    }
}
