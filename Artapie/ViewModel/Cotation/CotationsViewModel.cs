using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Cotation
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class CotationsViewModel : BaseEntitiesViewModel<Cotation>
    {
        public CotationsViewModel(NavigationViewModel parent, ModelContext context)
            : base(parent, context)
        {
        }

        public FicheViewModel FicheViewModel { get; set; }

    }
}
