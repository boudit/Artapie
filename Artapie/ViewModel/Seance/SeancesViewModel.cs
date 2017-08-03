//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FichesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Seance
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class SeancesViewModel : EntitiesViewModel<SeanceViewModel, Seance>
    {
        public SeancesViewModel(ModelContext context)
            :base(context)
        {
        }

        public FicheViewModel FicheViewModel { get; set; }

        public override string ViewTitle
        {
            get
            {
                return "Seances";
            }
        }

        protected override SeanceViewModel CreateViewModel(Seance entity)
        {
            return new SeanceViewModel(entity, this.context);
        }
    }
}
