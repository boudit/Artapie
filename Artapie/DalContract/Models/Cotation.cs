namespace DalContract.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cotation : IModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public short Cote { get; set; }

        [Required]
        public int ItemId { get; set; }
        public virtual Fiche Fiche { get; set; }

        [Required]
        public int FicheId { get; set; }
        public virtual Item Item { get; set; }
    }
}
