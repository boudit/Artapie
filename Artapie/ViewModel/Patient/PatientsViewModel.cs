namespace ViewModel.Patient
{
    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class PatientsViewModel : EntitiesViewModel<PatientViewModel, Patient>
    {
        public PatientsViewModel(ModelContext context)
            : base(context)
        {
        }

        public override string ViewTitle
        {
            get
            {
                return "Patients";
            }
        }

        protected override PatientViewModel CreateViewModel(Patient entity)
        {
            return new PatientViewModel(entity, this.context);
        }
    }
}
