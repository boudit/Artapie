namespace ViewModel.Shared
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Dal;

    using DalContract.Models;
    
    public abstract class BaseEntityViewModel<T> : NavigationViewModel where T : class, IModel, new()
    {
        public event EventHandler<EntityState>  SavedEvent;

        protected readonly ModelContext context;
        
        protected BaseEntityViewModel(NavigationViewModel parent, T entity, ModelContext context)
            :base (parent)
        {
            this.context = context;
            this.CurrentEntityEntry = context.Entry(entity);
            
            this.SaveCommand = new DelegateCommand(this.Save);
            this.DisplayMeCommand = new DelegateCommand(this.DisplayMe);

            this.PopulateFromEntity(this.CurrentEntityEntry.Entity);
        }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand DisplayMeCommand { get; }

        public abstract string DisplayValue { get; }

        private DbEntityEntry<T> CurrentEntityEntry { get; }

        public T Entity
        {
            get
            {
                return this.CurrentEntityEntry.Entity;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            base.OnPropertyChanged("DisplayValue");
        }

        protected abstract void InjectIntoEntity(T entity);

        protected abstract void PopulateFromEntity(T entity);

        protected override void Close()
        {
            this.PopulateFromEntity(this.CurrentEntityEntry.Entity);

            base.Close();
        }

        private void Save()
        {
            this.InjectIntoEntity(this.CurrentEntityEntry.Entity);

            var state = this.CurrentEntityEntry.State;

            this.context.SaveChanges();

            this.PopulateFromEntity(this.CurrentEntityEntry.Entity);

            this.SavedEvent?.Invoke(this, state);
        }

        private void DisplayMe()
        {
            this.Parent.CurrentChild = this;
        }
    }
}
