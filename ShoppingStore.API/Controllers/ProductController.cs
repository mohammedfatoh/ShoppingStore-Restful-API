using AutoMapper;
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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if(product == null)
            {
                return NotFound("Product is Not Found");
            }
                  
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            if (products == null)
            {
                return NotFound("Products is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _unitOfWork.Products.FindAsync(c => c.Name == name);
            if(product == null)
            {
                return NotFound("Product is Not Found");
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll(string name)
        {
            var products = await _unitOfWork.Products.FindAllAsync(c => c.Name.Contains(name));
            if (products == null)
            {
                return NotFound("Category is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Addproduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existproduct = await _unitOfWork.Products.FindAsync(c => c.Name == productDto.Name);
            if (existproduct != null)
                return BadRequest("Product Already With The Same Name exists");

            var product = _mapper.Map<Product>(productDto);
            try
            {
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.Complete();
                productDto.Id = product.Id;
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> deleteproduct(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                try
                {
                    _unitOfWork.Products.Delete(product);
                    await _unitOfWork.Complete();
                    return StatusCode(204, "Product Remove Success");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> updateproduct(ProductUpdateDto productUpdateDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _unitOfWork.Products.GetById(id);

            var existproduct = await _unitOfWork.Products.FindAsync(c => c.Name == productUpdateDto.Name);
            if (existproduct != null && existproduct.Id != product.Id)
                return BadRequest("Product Already With The Same Name exists");

            if (product == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                 product = _mapper.Map(productUpdateDto,product);
                try
                {
                    _unitOfWork.Products.Update(product);
                    await _unitOfWork.Complete();
                    return StatusCode(204, productUpdateDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

    }
}
