using Caramel.Pattern.Services.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Caramel.Pattern.Services.Domain.Entities
{
    public class Pet : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Castrated { get; set; }
        public bool Vaccinated { get; set; }
        public int Age { get; set; }
        public PetStatus Status { get; set; }
    }
}
