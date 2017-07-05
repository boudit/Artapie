//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SeancesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Seance
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class SeancesViewModel : BaseEntitiesViewModel<Seance>
    {
        public SeancesViewModel(ModelContext context)
            : base(context)
        {
        }
    }
}
