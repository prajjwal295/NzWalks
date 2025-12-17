using AutoMapper;
using Azure;
using Employee.Application.Validators;
using Employee.BLL.BOs.Requests;
using Employee.BLL.BOs.Response;
using Employee.BLL.DTOs;
using Employee.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Employee.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper,
            IHttpContextAccessor context,
            ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeBO createEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeBo = _mapper.Map<EmployeeDto>(createEmployeeDto);

                var user = _context.HttpContext?.User;
                employeeBo.CreatedBy = user?.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

                var createdBo = await _employeeService.CreateAsync(employeeBo);

                var responseDto = _mapper.Map<EmployeeResponsebo>(createdBo);
                return Ok(new
                {
                    Message = "Employee Created successfully",
                    Data = responseDto
                });

            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee.");
                return StatusCode(500 , ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employeeBo = await _employeeService.GetByIdAsync(id);

                if (employeeBo == null)
                    return NotFound();

                var responseDto = _mapper.Map<EmployeeResponsebo>(employeeBo);
                return Ok(new
                {
                    Message = "Employee Fetched successfully",
                    Data = responseDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching employee with ID {id}.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employeeBos = await _employeeService.GetAllAsync();

                var responseDtos = _mapper.Map<List<EmployeeResponsebo>>(employeeBos);
                return Ok(new
                {
                    Message = "Employees Fetched successfully",
                    Data = responseDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all employees.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deletedEmployee = await _employeeService.DeleteAsync(id);
                if (deletedEmployee == null)
                    return NotFound();
                var responseDto = _mapper.Map<EmployeeResponsebo>(deletedEmployee);
                return Ok(new
                {
                    Message = "Employee Deleted successfully",
                    Data = responseDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting employee with ID {id}.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateEmployeeBO updateEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var employeeBo = _mapper.Map<EmployeeDto>(updateEmployeeDto);
                var user = _context.HttpContext?.User;
                employeeBo.ModifiedBy = user?.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
                var updatedBo = await _employeeService.UpdateAsync(id, employeeBo);
                if (updatedBo == null)
                    return NotFound();
                var responseDto = _mapper.Map<EmployeeResponsebo>(updatedBo);
                return Ok(new
                {
                    Message = "Employee Updated successfully",
                    Data = responseDto
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating employee.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
