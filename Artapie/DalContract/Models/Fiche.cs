namespace DalContract.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Fiche : IModel
    {
        public Fiche()
        {
            this.Cotations = new HashSet<Cotation>();
        }

        [Key]
        public int Id { get; set; }
        
        public virtual ICollection<Cotation> Cotations { get; set; }

        [Required]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required]
        public int SeanceId { get; set; }
        public virtual Seance Seance { get; set; }
    }
}
