namespace ViewModel.Shared
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Dal;
    
    public abstract class EntitiesViewModel<T, U> : NotifyPropertyChangedViewModel, IClosable
        where T : EntityViewModel<U> where U : class
    {
        public event EventHandler CloseEvent;

        public event EventHandler SelectionChangedEvent;

        protected readonly ModelContext context;

        private T currentChild;

        private bool canCreate;

        public EntitiesViewModel(ModelContext context)
        {
            this.context = context;
            this.Children = new ObservableCollection<T>();
            this.canCreate = true;
            this.SelectionMode = EnumSelectionMode.NoSelection;

            this.CreateCommand = new DelegateCommand(this.Create, this.CanCreate);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.CloseCommand = new DelegateCommand(this.Close);
        }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CreateCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public ObservableCollection<T> Children { get; }

        public T CurrentChild
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

        public bool IsCreationEnabled
        {
            get
            {
                return this.canCreate;
            }

            set
            {
                this.SetProperty(ref this.canCreate, value);
            }
        }

        public EnumSelectionMode SelectionMode { get; set; }

        public abstract string ViewTitle { get; }
        
        protected abstract T CreateViewModel(U entity);

        protected virtual IQueryable<U> ApplyFilterOnRefresh(IQueryable<U> query)
        {
            return query;
        }
        
        private bool CanCreate()
        {
            return this.canCreate;
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

            var query = this.context.Set<U>().AsQueryable();

            this.ApplyFilterOnRefresh(query);

            var entities = query.ToList();

            entities.ForEach(ent => this.AddViewModel(ent));
        }

        private void Close()
        {
            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private T AddViewModel(U entity)
        {
            var viewModel = this.CreateViewModel(entity);

            this.Children.Add(viewModel);
            this.AddHandlerOnChild(viewModel);

            return viewModel;
        }

        private void AddHandlerOnChild(T child)
        {
            child.IsSelectedChanged += this.Child_OnIsSelectedChanged;
        }

        private void RemoveHandlerOnChild(T child)
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
            var typedSender = (T)sender;

            if (this.SelectionMode == EnumSelectionMode.NoSelection)
            {
                this.ManageNoSelectionMode(typedSender);
            }
            else if (this.SelectionMode == EnumSelectionMode.Single)
            {
                this.ManageSingleSelectionMode(typedSender);
            }

            this.SelectionChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void ManageSingleSelectionMode(T sender)
        {
            if (!sender.IsSelected)
            {
                return;
            }

            var selectedChildren = this.Children.Where(c => c.IsSelected).ToList();
            foreach (var selectedChild in selectedChildren)
            {
                if (!ReferenceEquals(selectedChild, sender))
                {
                    selectedChild.IsSelected = false;
                }
            }
        }

        private void ManageNoSelectionMode(T sender)
        {
            if (sender.IsSelected)
            {
                if (this.CurrentChild != null && this.CurrentChild.IsSelected)
                {
                    this.CurrentChild.IsSelected = false;
                }

                this.CurrentChild = sender;
            }
            else if (ReferenceEquals(sender, this.CurrentChild))
            {
                this.CurrentChild = null;
            }
        }
    }
}
