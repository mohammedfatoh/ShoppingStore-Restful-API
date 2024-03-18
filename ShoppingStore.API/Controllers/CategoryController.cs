using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Repository;
using ShoppingStore.Repository.Models;
using ShoppingStore.Repository.Repositories;

namespace ShoppingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork.Categories.GetById(id));
        }

        [HttpGet("GetAll")]
        public  IActionResult GetAll()
        {
            return Ok(_unitOfWork.Categories.GetAll());
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _unitOfWork.Categories.Find(c => c.Name ==name));
        }
    }
}
