namespace ViewModel.Item
{
    using Dal;

    using DalContract.Models;
    
    using ViewModel.Shared;

    public class ItemsViewModel : EntitiesViewModel<ItemViewModel, Item>
    {
        public ItemsViewModel(ModelContext context)
            : base(context)
        {
        }

        public override string ViewTitle
        {
            get
            {
                return "Items";
            }
        }

        protected override ItemViewModel CreateViewModel(Item entity)
        {
            return new ItemViewModel(entity, this.context);
        }
    }
}
