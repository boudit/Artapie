namespace ViewModel.Seance
{
    using System;

    using Dal;

    using DalContract.Models;

    using ViewModel.Fiche;
    using ViewModel.Shared;

    public class SeanceViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private DateTime? date;
        
        public event EventHandler SavedEvent;

        public event EventHandler CloseEvent;

        public SeanceViewModel(Seance entity, ModelContext context)
        {
            this.Entity = entity;
            this.context = context;

            this.SaveCommand = new DelegateCommand(this.Save);
            this.CloseCommand = new DelegateCommand(this.Close);
            this.RefreshCommand = new DelegateCommand(this.Refresh);

            this.Fiches = new FichesViewModel(context) { SeanceViewModel = this };

            this.Refresh();
        }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand RefreshCommand { get; }
        
        public IDelegateCommand CloseCommand { get; }

        public Seance Entity { get; private set; }

        public DateTime? Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.SetProperty(ref this.date, value);
            }
        }
        
        public FichesViewModel Fiches { get; }

        public string DisplayValue
        {
            get
            {
                return $"{this.date.Value.ToLongDateString()}";
            }
        }
        
        private void Save()
        {
            if (this.Entity == null)
            {
                this.Entity = this.context.Seances.Create();
                this.context.Seances.Add(this.Entity);
            }

            this.Entity.Date = this.Date.Value;

            this.context.SaveChanges();

            this.Refresh();

            this.SavedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Close()
        {
            this.Refresh();

            this.CloseEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Refresh()
        {
            this.Date = this.Entity?.Date;
        }
    }
}
