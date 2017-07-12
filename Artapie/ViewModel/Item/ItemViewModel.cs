namespace ViewModel.Item
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class ItemViewModel : BaseEntityViewModel<Item>
    {
        private string nom;

        public ItemViewModel(NavigationViewModel parent, Item entity, ModelContext context)
            : base(parent, entity, context)
        {
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

        protected override void InjectIntoEntity(Item entity)
        {
            entity.Nom = this.Nom;
        }

        protected override void PopulateFromEntity(Item entity)
        {
            this.Nom = entity.Nom;
        }
    }
}
