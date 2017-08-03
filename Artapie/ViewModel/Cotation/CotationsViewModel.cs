namespace ViewModel.Cotation
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class CotationsViewModel : EntitiesViewModel<CotationViewModel, Cotation>
    {
        public CotationsViewModel(ModelContext context)
            : base(context)
        {
        }

        public FicheViewModel FicheViewModel { get; set; }

        public override string ViewTitle
        {
            get
            {
                return "Cotations";
            }
        }

        protected override CotationViewModel CreateViewModel(Cotation entity)
        {
            return new CotationViewModel(entity, this.context);
        }
    }
}
