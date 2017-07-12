namespace ViewModel.Fiche
{
    using System.Linq;

    using Dal;

    using DalContract.Models;

    using ViewModel.Cotation;
    using ViewModel.Patient;
    using ViewModel.Shared;

    public class FicheViewModel : BaseEntityViewModel<Fiche>
    {
        public FicheViewModel(NavigationViewModel parent, Fiche entity, ModelContext context)
            : base(parent, entity, context)
        {
            this.CotationsViewModel = new CotationsViewModel(this, context) { FicheViewModel = this };
        }
        
        public CotationsViewModel CotationsViewModel { get; }

        public BaseEntityViewModel<Patient> PatientViewModel { get; set; }

        public BaseEntityViewModel<Seance> SeanceViewModel { get; set; }

        public override string DisplayValue
        {
            get
            {
                return $"{this.PatientViewModel.DisplayValue} : {this.SeanceViewModel.DisplayValue}";
            }
        }

        protected override void InjectIntoEntity(Fiche entity)
        {
            // entity.Cotations = this.CotationsViewModel.Children.Select(c => c.Entity);

            entity.Patient = this.PatientViewModel.Entity;
            entity.Seance = this.SeanceViewModel.Entity;
        }

        protected override void PopulateFromEntity(Fiche entity)
        {
            this.PatientViewModel = ViewModelFactory.Create(this, entity.Patient, this.context);
            this.SeanceViewModel = ViewModelFactory.Create(this, entity.Seance, this.context);
        }
    }
}
