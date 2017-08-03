namespace ViewModel.Fiche
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Cotation;
    using ViewModel.Patient;
    using ViewModel.Seance;
    using ViewModel.Shared;

    public class FicheViewModel :  EntityViewModel<Fiche>
    {
        public FicheViewModel(Fiche entity, ModelContext context)
            : base(entity, context)
        {
            this.CotationsViewModel = new CotationsViewModel(context) { FicheViewModel = this };
            this.SeancesViewModel = new SeancesViewModel(context) { FicheViewModel = this, IsCreationEnabled = false, SelectionMode = EnumSelectionMode.Single };
        }
        
        public CotationsViewModel CotationsViewModel { get; }

        public PatientViewModel PatientViewModel { get; set; }

        public SeanceViewModel SeanceViewModel { get; set; }

        public SeancesViewModel SeancesViewModel { get; }

        public override string DisplayValue
        {
            get
            {
                return "Fiche";
            }
        }

        protected override void SetValuesOnEntity()
        {
            this.Entity.Patient = this.PatientViewModel.Entity;
            this.Entity.Seance = this.SeanceViewModel.Entity;
        }

        protected override void Refresh()
        {
            this.PatientViewModel.RefreshCommand.Execute(null);
            this.SeanceViewModel.RefreshCommand.Execute(null);
        }
    }
}
