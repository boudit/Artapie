namespace DalContract.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item : IModel
    {
        public Item()
        {
            this.Cotations = new HashSet<Cotation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        
        public virtual ICollection<Cotation> Cotations { get; set; }
    }
}
