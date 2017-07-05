namespace ViewModel.Patient
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class PatientsViewModel : BaseEntitiesViewModel<Patient>
    {
        public PatientsViewModel(ModelContext context)
            : base(context)
        {
        }
    }
}
