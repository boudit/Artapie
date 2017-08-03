namespace ViewModel.Seance
{
    using System;

    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class SeanceViewModel : EntityViewModel<Seance>
    {
        private DateTime? date;
        
        public SeanceViewModel(Seance entity, ModelContext context)
            : base(entity, context)
        {
            this.Fiches = new FichesViewModel(context) { SeanceViewModel = this };
        }
        
        public DateTime? Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.SetProperty(ref this.date, value);
            }
        }
        
        public FichesViewModel Fiches { get; }

        public override string DisplayValue
        {
            get
            {
                return $"{this.date.Value.ToLongDateString()}";
            }
        }

        protected override void SetValuesOnEntity()
        {
            this.Entity.Date = this.Date.Value;
        }

        protected override void Refresh()
        {
            this.Date = this.Entity?.Date;
        }
    }
}
