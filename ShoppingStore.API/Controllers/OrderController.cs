using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.API.DTO.CategoryDTo;
using ShoppingStore.Repository.Models;
using ShoppingStore.Repository;
using ShoppingStore.API.DTO.OrderDTos;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null)
            {
                return NotFound("Order is Not Found");
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var orders = _unitOfWork.Orders.GetAll();
            if (orders == null)
            {
                return NotFound("Orders is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var order = await _unitOfWork.Orders.FindAsync(c => c.Name == name);
            if (order == null)
            {
                return NotFound("Order is Not Found");
            }
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll(string name)
        {
            var orders = await _unitOfWork.Orders.FindAllAsync(c => c.Name.Contains(name));
            if (orders == null)
            {
                return NotFound("Order is Not Found");
            }
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);
            try
            {
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.Complete();
                orderDto.Id = order.Id;
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> CancleOrder(int id)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                try
                {
                    _unitOfWork.Orders.Delete(order);
                    await _unitOfWork.Complete();
                    return StatusCode(204, "Order Remove Success");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [Authorize(Roles = "User")]
        [HttpPatch("Update")]
        public async Task<IActionResult> updateOrder(OrderDto orderUpdateDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = await _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return BadRequest("Id Not Valid");
            }
            else
            {
                _mapper.Map(orderUpdateDto, order);
                try
                {
                    _unitOfWork.Orders.Update(order);
                    await _unitOfWork.Complete();
                    return StatusCode(204, orderUpdateDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
    }
}
