using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Repository.Models;
using ShoppingStore.Repository.Repositories;

namespace ShoppingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       private readonly IBaseRepository<Category> _categoryRepository;

        public CategoryController(IBaseRepository<Category> category)
        {
            _categoryRepository = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryRepository.GetById(id));
        }

        [HttpGet("GetAll")]
        public  IActionResult GetAll()
        {
            return Ok( _categoryRepository.GetAll());
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _categoryRepository.Find(c => c.Name ==name));
        }
    }
}
