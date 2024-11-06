using System.ComponentModel.DataAnnotations;

namespace BanSach.Model
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

     
    }
}
