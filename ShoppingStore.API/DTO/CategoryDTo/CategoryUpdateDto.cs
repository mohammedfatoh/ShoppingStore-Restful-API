using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.API.DTO.CategoryDTo
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Miniumum 3 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
