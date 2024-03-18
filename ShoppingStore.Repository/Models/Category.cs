using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Repository.Models
{
    public class Category
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Miniumum 3 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
