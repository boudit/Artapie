namespace ViewModel.Cotation
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class CotationViewModel : EntityViewModel<Cotation>
    {
        private short cote;

        public CotationViewModel(Cotation entity, ModelContext context)
            : base(entity, context)
        {
            this.Refresh();
        }
        
        public short Cote
        {
            get
            {
                return this.cote;
            }

            set
            {
                this.SetProperty(ref this.cote, value);
            }
        }
        
        public override string DisplayValue
        {
            get
            {
                return this.Cote.ToString();
            }
        }

        protected override void SetValuesOnEntity()
        {
            this.Entity.Cote = this.Cote;
        }

        protected override void Refresh()
        {
            var cotation = this.Entity;

            if (cotation != null)
            {
                this.Cote = cotation.Cote;
            }
        }
    }
}
