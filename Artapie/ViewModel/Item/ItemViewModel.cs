namespace ViewModel.Item
{
    using System;

    using Dal;

    using DalContract.Models;

    using ViewModel.Shared;

    public class ItemViewModel : NotifyPropertyChangedViewModel, IClosable
    {
        private readonly ModelContext context;

        private string nom;

        private bool isSelected;

        public event EventHandler SavedEvent;

        public event EventHandler CloseEvent;

        public event EventHandler IsSelectedChanged;

        public ItemViewModel(Item entity, ModelContext context)
        {
            this.Entity = entity;
            this.context = context;

            this.SaveCommand = new DelegateCommand(this.Save);
            this.CloseCommand = new DelegateCommand(this.Close);
            this.RefreshCommand = new DelegateCommand(this.Refresh);
            this.SwitchIsSelectedCommand = new DelegateCommand(() => this.IsSelected = !this.IsSelected);

            this.Refresh();
        }

        public IDelegateCommand SaveCommand { get; }

        public IDelegateCommand RefreshCommand { get; }

        public IDelegateCommand CloseCommand { get; }

        public IDelegateCommand SwitchIsSelectedCommand { get; }

        public Item Entity { get; private set; }
        
        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.SetProperty(ref this.nom, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.SetProperty(ref this.isSelected, value);
                this.IsSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string DisplayValue
        {
            get
            {
                return $"{this.Nom}";
            }
        }

        private void Save()
        {
            if (this.Entity == null)
            {
                this.Entity = this.context.Items.Create();
                this.context.Items.Add(this.Entity);
            }

            this.Entity.Nom = this.Nom;

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
            this.Nom = this.Entity?.Nom;
        }
    }
}
