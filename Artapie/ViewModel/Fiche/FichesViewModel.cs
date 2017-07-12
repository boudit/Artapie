//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FichesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Fiche
{
    using System;
    using System.Linq.Expressions;

    using Dal;

    using DalContract.Models;

    using ViewModel.Patient;
    using ViewModel.Shared;

    public class FichesViewModel : BaseEntitiesViewModel<Fiche>
    {
        public FichesViewModel(NavigationViewModel parent, ModelContext context)
            : base(parent, context)
        {
        }

        public PatientViewModel PatientViewModel { get; set; }

        protected override Expression<Func<Fiche, bool>> LoadFilter()
        {
            if (this.Parent == null)
            {
                return base.LoadFilter();
            }

            return arg => arg.Patient.Id == this.PatientViewModel.Entity.Id;
        }

        protected override void InitViewModel(BaseEntityViewModel<Fiche> viewModel)
        {
            base.InitViewModel(viewModel);

            ((FicheViewModel)viewModel).PatientViewModel = this.PatientViewModel;
        }
    }
}
