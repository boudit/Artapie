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
        
        protected BaseEntityViewModel(T entity, ModelContext context)
        {
            this.context = context;
            this.CurrentEntityEntry = context.Entry(entity);
            
            this.SaveCommand = new DelegateCommand(this.Save);
        }
        
        public IDelegateCommand SaveCommand { get; }
        
        protected DbEntityEntry<T> CurrentEntityEntry { get; }
        
        private void Save()
        {
            var state = this.CurrentEntityEntry.State;

            this.context.SaveChanges();
            
            this.SavedEvent?.Invoke(this, state);
        }
    }
}
