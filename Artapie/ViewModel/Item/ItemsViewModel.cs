namespace ViewModel.Item
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class ItemsViewModel : BaseEntitiesViewModel<Item>
    {
        public ItemsViewModel(NavigationViewModel parent, ModelContext context)
            : base(parent, context)
        {
        }
    }
}
