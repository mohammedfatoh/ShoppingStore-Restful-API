using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.API.DTO.OrderDTos
{
    public class OrderUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string NameonCard { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string? StatusOfOrder { get; set; }

        public string? Quantities { get; set; }
    }
}
