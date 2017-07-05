namespace ViewModel.Item
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class ItemViewModel : BaseEntityViewModel<Item>
    {
        public ItemViewModel(Item entity, ModelContext context)
            : base(entity, context)
        {
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
    }
}
