namespace ViewModel.Item
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class ItemViewModel : EntityViewModel<Item>
    {
        private string nom;

        public ItemViewModel(Item entity, ModelContext context)
            : base(entity, context)
        {
            this.Refresh();
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
        
        public override string DisplayValue
        {
            get
            {
                return this.Nom;
            }
        }

        protected override void SetValuesOnEntity()
        {
            this.Entity.Nom = this.Nom;
        }

        protected override void Refresh()
        {
            this.Nom = this.Entity?.Nom;
        }
    }
}
