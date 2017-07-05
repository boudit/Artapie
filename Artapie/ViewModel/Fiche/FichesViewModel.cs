//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FichesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Fiche
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class FichesViewModel : BaseEntitiesViewModel<Fiche>
    {
        public FichesViewModel(ModelContext context)
            : base(context)
        {
        }

        public Patient Parent { get; set; }

        protected override Expression<Func<Fiche, bool>> LoadFilter()
        {
            if (this.Parent == null)
            {
                return base.LoadFilter();
            }

            return arg => arg.Patient.Id == this.Parent.Id;
        }
    }
}
