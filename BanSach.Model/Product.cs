using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Model
{
    public class Product
    {
        // các thuộc tính
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        // giá khi mua từ 50 cuốn
        [Required]
        [Range(1, 100000000)]
        public double Price50 { get; set; }

        // giá khi mua từ 100 cuốn
        [Required]
        [Range(1, 100000000)]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        //khoá ngoại CategoryId
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
     
        [ForeignKey("CategoryId")]

        public Category Category { get; set; }


        //khoá ngoại CoverTypeId
        [Required]
        public int CoverTypeId { get; set; }
     
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType coverType { get; set; }
    }
}
