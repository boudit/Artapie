namespace DalContract.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Patient : IModel
    {
        public Patient()
        {
            this.Fiches = new HashSet<Fiche>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Prenom { get; set; }

        public string Nom { get; set; }
        
        public virtual ICollection<Fiche> Fiches { get; set; }
    }
}
