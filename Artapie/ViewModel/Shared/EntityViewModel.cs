namespace ViewModel.Shared
{
    using System;

    using Dal;
    
    public abstract class EntityViewModel<T> : NotifyPropertyChangedViewModel, IClosable
        where T : class
    {
        private readonly ModelContext context;
        
        private bool isSelected;

        private bool isEditing;

        public event EventHandler SavedEvent;

        public event EventHandler CloseEvent;

        public event EventHandler IsSelectedChanged;

        public EntityViewModel(T entity, ModelContext context)
        {
            this.Entity = entity;
            this.context = context;

            this.EditCommand = new DelegateCommand(() => this.IsEditing = true);
            this.CancelEditionCommand = new DelegateCommand(() => { this.Refresh(); this.IsEditing = false; });
            this.SaveCommand = new DelegateCommand(this.Save);
            this.CloseCommand = new DelegateCommand(this.Close);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.SwitchIsSelectedCommand = new DelegateCommand(() => this.IsSelected = !this.IsSelected);
        }

        public IDelegateCommand EditCommand { get; }

        public IDelegateCommand CancelEditionCommand { get; }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public IDelegateCommand SwitchIsSelectedCommand { get; }

        public T Entity { get; private set; }
        
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.SetProperty(ref this.isSelected, value);
                this.IsSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsEditing
        {
            get
            {
                return this.isEditing;
            }

            set
            {
                this.SetProperty(ref this.isEditing, value);
            }
        }

        public abstract string DisplayValue { get; }

        protected abstract void SetValuesOnEntity();

        protected abstract void Refresh();

        private void Save()
        {
            if (this.Entity == null)
            {
                this.Entity = this.context.Set<T>().Create();
                this.context.Set<T>().Add(this.Entity);
            }

            this.SetValuesOnEntity();
            
            this.context.SaveChanges();

            this.Refresh();

            this.IsEditing = false;
            this.SavedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Close()
        {
            this.Refresh();

            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
