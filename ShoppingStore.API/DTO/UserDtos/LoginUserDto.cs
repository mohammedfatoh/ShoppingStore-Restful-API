using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.API.DTO.UserDtos
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
