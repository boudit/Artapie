namespace ViewModel.Item
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Dal;

    using DalContract.Models;

    using ViewModel.Patient;
    using ViewModel.Shared;

    public class ItemsViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private ItemViewModel currentChild;

        public event EventHandler CloseEvent;

        public ItemsViewModel(ModelContext context)
        {
            this.context = context;
            this.Children = new ObservableCollection<ItemViewModel>();

            this.CreateCommand = new DelegateCommand(this.Create);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.CloseCommand = new DelegateCommand(this.Close);
        }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CreateCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public ObservableCollection<ItemViewModel> Children { get; }

        public ItemViewModel CurrentChild
        {
            get
            {
                return this.currentChild;
            }

            set
            {
                this.RemoveHandlerOnCurrentChild();

                this.SetProperty(ref this.currentChild, value);

                this.AddHandlerOnCurrentChild();
            }
        }

        private void Create()
        {
            var viewModel = this.CreateViewModel(null);

            this.Children.Add(viewModel);

            this.CurrentChild = viewModel;
        }

        private void Refresh()
        {
            this.Children.Select(
                c =>
                {
                    this.RemoveHandlerOnChild(c);
                    return true;
                });
            this.Children.Clear();

            var entities = this.context.Set<Item>()
                .ToList();
            entities.ForEach(ent => this.AddViewModel(ent));
        }

        private void Close()
        {
            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private ItemViewModel AddViewModel(Item entity)
        {
            var viewModel = this.CreateViewModel(entity);

            this.Children.Add(viewModel);
            this.AddHandlerOnChild(viewModel);

            return viewModel;
        }

        private ItemViewModel CreateViewModel(Item entity)
        {
            return new ItemViewModel(entity, this.context);
        }

        private void AddHandlerOnChild(ItemViewModel child)
        {
            child.IsSelectedChanged += this.Child_OnIsSelectedChanged;
        }

        private void RemoveHandlerOnChild(ItemViewModel child)
        {
            child.IsSelectedChanged -= this.Child_OnIsSelectedChanged;
        }

        private void AddHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent += this.CurrentChild_OnCloseEvent;
            this.currentChild.SavedEvent += this.CurrentChild_OnSavedEvent;
        }

        private void RemoveHandlerOnCurrentChild()
        {
            if (this.currentChild == null)
            {
                return;
            }

            this.currentChild.CloseEvent -= this.CurrentChild_OnCloseEvent;
            this.currentChild.SavedEvent -= this.CurrentChild_OnSavedEvent;
        }

        private void CurrentChild_OnCloseEvent(object sender, EventArgs eventArgs)
        {
            this.CurrentChild = null;
        }

        private void CurrentChild_OnSavedEvent(object sender, EventArgs eventArgs)
        {
            this.RefreshCommand.Execute(null);
        }

        private void Child_OnIsSelectedChanged(object sender, EventArgs eventArgs)
        {
            this.CurrentChild = sender as ItemViewModel;
        }
    }
}
