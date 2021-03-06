﻿namespace ViewModel.Shared
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Dal;

    using DalContract.Models;

    public class BaseEntitiesViewModel<T> : NavigationViewModel where T : class, IModel, new()
    {
        protected readonly ModelContext context;

        protected BaseEntitiesViewModel(NavigationViewModel parent, ModelContext context)
            :base (parent)
        {
            this.context = context;
            
            this.CreateCommand = new DelegateCommand(this.Create);
            this.LoadCommand = new DelegateCommand(this.Load);
        }
        
        public IDelegateCommand LoadCommand { get; }

        public IDelegateCommand CreateCommand { get; }

        protected override void AddHandlerOnCurrentChild()
        {
            base.AddHandlerOnCurrentChild();

            var currentChild = this.CurrentChild as BaseEntityViewModel<T>;
            if (currentChild == null)
            {
                return;
            }

            currentChild.SavedEvent += this.CurrentChild_OnSavedEvent;
        }

        protected override void RemoveHandlerOnCurrentChild()
        {
            base.AddHandlerOnCurrentChild();

            var currentChild = this.CurrentChild as BaseEntityViewModel<T>;
            if (currentChild == null)
            {
                return;
            }

            currentChild.SavedEvent -= this.CurrentChild_OnSavedEvent;
        }

        protected virtual Expression<Func<T, bool>> LoadFilter()
        {
            return arg => true;
        }

        protected virtual void InitViewModel(BaseEntityViewModel<T> viewModel)
        {
            
        }

        private BaseEntityViewModel<T> CreateViewModel(T entity)
        {
            var result = ViewModelFactory.Create(this, entity, this.context);

            this.InitViewModel(result);

            return result;
        }

        private void Create()
        {
            var entity = this.context.Set<T>().Create();
            this.context.Set<T>().Add(entity);

            var viewModel = this.CreateViewModel(entity);

            this.Children.Add(viewModel);

            this.CurrentChild = viewModel;
        }

        private void Load()
        {
            this.Children.Clear();

            var entities = this.context.Set<T>()
                .Where(this.LoadFilter())
                .ToList();
            entities.ForEach(ent => this.AddViewModel(ent));
        }

        private BaseEntityViewModel<T> AddViewModel(T entity)
        {
            var viewModel = this.CreateViewModel(entity);

            this.AddChild(viewModel);

            return viewModel;
        }

        private void AddChild(NavigationViewModel viewModel)
        {
            this.Children.Add(viewModel);
        }

        private void RemoveChild(NavigationViewModel viewModel)
        {
            if (this.CurrentChild == viewModel)
            {
                this.CurrentChild = null;
            }

            this.Children.Remove(viewModel);
        }

        private void CurrentChild_OnSavedEvent(object sender, EntityState eventArgs)
        {
            //if (eventArgs == EntityState.Added)
            //{
            //    this.AddChild(sender as BaseEntityViewModel<T>);
            //}
            //else if (eventArgs == EntityState.Deleted)
            //{
            //    this.RemoveChild(sender as BaseEntityViewModel<T>);
            //}

            this.LoadCommand.Execute(null);
        }
    }
}
