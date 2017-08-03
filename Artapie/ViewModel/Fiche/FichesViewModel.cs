//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FichesViewModel.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace ViewModel.Fiche
{
    using System.Linq;

    using Dal;

    using DalContract.Models;

    using ViewModel.Patient;
    using ViewModel.Seance;
    using ViewModel.Shared;

    public class FichesViewModel : EntitiesViewModel<FicheViewModel, Fiche>
    {
        public FichesViewModel(ModelContext context)
            :base(context)
        {
        }
        
        public PatientViewModel PatientViewModel { get; set; }

        public SeanceViewModel SeanceViewModel { get; set; }

        public override string ViewTitle
        {
            get
            {
                return "Fiches";
            }
        }
        
        protected override IQueryable<Fiche> ApplyFilterOnRefresh(IQueryable<Fiche> query)
        {
            query = base.ApplyFilterOnRefresh(query);

            if (this.PatientViewModel != null)
            {
                query = query.Where(fiche => fiche.Patient.Id == this.PatientViewModel.Entity.Id);
            }

            if (this.SeanceViewModel != null)
            {
                query = query.Where(fiche => fiche.Seance.Id == this.SeanceViewModel.Entity.Id);
            }

            return query;
        }
        
        protected override FicheViewModel CreateViewModel(Fiche entity)
        {
            var result = new FicheViewModel(entity, this.context);

            result.PatientViewModel = this.PatientViewModel;
            result.SeanceViewModel = this.SeanceViewModel;

            return result;
        }
    }
}
