using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.API.DTO.CategoryDTo;
using ShoppingStore.API.DTO.ProductDtos;
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
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            if (category == null)
            {
                return NotFound("Category is Not Found");
            }

            return Ok(_mapper.Map<CategoryDTo>(category));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll();
            if (categories == null)
            {
                return NotFound("Categories is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<CategoryDTo>>(categories));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _unitOfWork.Categories.FindAsync(c => c.Name == name);
            if (category == null)
            {
                return NotFound("Category is Not Found");
            }
            return Ok(_mapper.Map<CategoryDTo>(category));
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll(string name)
        {
           var categories = await _unitOfWork.Categories.FindAllAsync(c => c.Name.Contains(name));
            if (categories == null)
            {
                return NotFound("Category is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<CategoryDTo>>(categories));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Addcategory(CategoryDTo categoryDTo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existcategory = await _unitOfWork.Categories.FindAsync(c => c.Name == categoryDTo.Name);
            if (existcategory != null)
                return BadRequest("Category Already With The Same Name exists");

            Category category = _mapper.Map<Category>(categoryDTo);
            try
            {
                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.Complete();
                categoryDTo.ID = category.Id;
                return Ok(categoryDTo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> deletecategory(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            if (category == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                try
                {
                    _unitOfWork.Categories.Delete(category);
                    await _unitOfWork.Complete();
                    return StatusCode(204, "Category Remove Success");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update")]
        public async Task<IActionResult> updatecategory(CategoryUpdateDto categoryUpdateDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _unitOfWork.Categories.GetById(id);

            var existcategory = await _unitOfWork.Categories.FindAsync(c => c.Name == categoryUpdateDto.Name);
            if (existcategory != null && existcategory.Id != category.Id)
                return BadRequest("Category Already With The Same Name exists");

            if (category == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                _mapper.Map(categoryUpdateDto, category);
                try
                {
                    _unitOfWork.Categories.Update(category);
                    await _unitOfWork.Complete();
                    return StatusCode(204, categoryUpdateDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
    }
}
