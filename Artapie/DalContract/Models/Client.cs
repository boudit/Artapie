namespace DalContract.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client : IModel
    {
        public Client()
        {
            this.Seances = new HashSet<Seance>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        
        public virtual ICollection<Seance> Seances { get; set; }
    }
}
