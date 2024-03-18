using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Repository.Models;
using ShoppingStore.Repository.Repositories;

namespace ShoppingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productsRepository;
        
        public ProductController(IBaseRepository<Product> productRepository)
        {
            _productsRepository = productRepository;
        }

        [HttpGet]
        public  async Task<IActionResult> GetById(int id)
        {
            return  Ok(await _productsRepository.GetById(id));
        }

    }
}
