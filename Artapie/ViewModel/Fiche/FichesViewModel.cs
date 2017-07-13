//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FichesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Fiche
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Dal;

    using DalContract.Models;

    using ViewModel.Patient;
    using ViewModel.Seance;
    using ViewModel.Shared;

    public class FichesViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private FicheViewModel currentChild;

        public event EventHandler CloseEvent;

        public FichesViewModel(ModelContext context)
        {
            this.context = context;
            this.Children = new ObservableCollection<FicheViewModel>();

            this.CreateCommand = new DelegateCommand(this.Create);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.CloseCommand = new DelegateCommand(this.Close);
        }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CreateCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public ObservableCollection<FicheViewModel> Children { get; }

        public FicheViewModel CurrentChild
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

        public PatientViewModel PatientViewModel { get; set; }

        public SeanceViewModel SeanceViewModel { get; set; }

        private void Create()
        {
            var viewModel = this.CreateViewModel(null);

            this.Children.Add(viewModel);

            this.CurrentChild = viewModel;
        }

        private void Refresh()
        {
            this.Children.Clear();

            var query = this.context.Fiches.AsQueryable();

            if (this.PatientViewModel != null)
            {
                query = query.Where(fiche => fiche.Patient.Id == this.PatientViewModel.Entity.Id);
            }

            if (this.SeanceViewModel != null)
            {
                query = query.Where(fiche => fiche.Seance.Id == this.SeanceViewModel.Entity.Id);
            }

            var entities = query.ToList();

            entities.ForEach(ent => this.AddViewModel(ent));
        }

        private void Close()
        {
            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private FicheViewModel AddViewModel(Fiche entity)
        {
            var viewModel = this.CreateViewModel(entity);

            this.Children.Add(viewModel);

            return viewModel;
        }

        private FicheViewModel CreateViewModel(Fiche entity)
        {
            var result = new FicheViewModel(entity, this.context);

            result.PatientViewModel = this.PatientViewModel;

            return result;
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
    }
}
