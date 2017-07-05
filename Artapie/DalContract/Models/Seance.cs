namespace DalContract.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Seance : IModel
    {
        public Seance()
        {
            this.Fiches = new HashSet<Fiche>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public System.DateTime Date { get; set; }
        
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<Fiche> Fiches { get; set; }
    }
}
