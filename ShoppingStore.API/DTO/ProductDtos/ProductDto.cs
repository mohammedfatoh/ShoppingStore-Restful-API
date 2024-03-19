using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.API.DTO.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Required")]
        [StringLength(100, ErrorMessage = "Miniumum 3 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description of product Required")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "image of product Required")]
        public byte[]? Image { get; set; }


        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "70000", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
