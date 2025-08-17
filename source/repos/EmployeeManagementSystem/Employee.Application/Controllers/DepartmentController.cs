using AutoMapper;
using Employee.Application.Validators;
using Employee.BLL.BOs.Requests;
using Employee.BLL.BOs.Response;
using Employee.BLL.DTOs;
using Employee.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _context;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(IDepartmentService departmentService, IMapper mapper, IHttpContextAccessor context, ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while Fetching Department.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var departmentBo = await _departmentService.GetByIdAsync(id);
            if (departmentBo == null)
            {
                return NotFound();
            }

            var departmentResponseDto = _mapper.Map<DepartmentDto>(departmentBo);
            return Ok(new
            {
                Message = "Department Fetched successfully",
                Data = departmentResponseDto
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while Fetching Department.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] UpsertDepartmentBO createDepartmentRequestDto)
    {
        try
        {
            var departmentBo = _mapper.Map<DepartmentDto>(createDepartmentRequestDto);
            var user = _context.HttpContext.User;
            var userInfo = user.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

            departmentBo.CreatedBy = userInfo;
            departmentBo.ModifiedBy = userInfo;

            departmentBo = await _departmentService.CreateAsync(departmentBo);
            var departmentResponseDto = _mapper.Map<DepartmentResponseBO>(departmentBo);

            return Ok(new
            {
                Message = "Department Created successfully",
                Data = departmentResponseDto
            });
        }
        catch (ArgumentException argEx)
        {
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while Creating Employee.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertDepartmentBO updateDepartmentRequestDto)
    {
        try
        {
            var departmentBo = _mapper.Map<DepartmentDto>(updateDepartmentRequestDto);
            var user = _context.HttpContext.User;
            var userInfo = user.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

            departmentBo.CreatedBy = userInfo;
            departmentBo.ModifiedBy = userInfo;

            var updatedDepartment = await _departmentService.UpdateAsync(id, departmentBo);
            if (updatedDepartment == null)
            {
                return NotFound();
            }

            var departmentResponseDto = _mapper.Map<DepartmentResponseBO>(updatedDepartment);
            return Ok(new
            {
                Message = "Department Updated successfully",
                Data = departmentResponseDto
            });
        }
        catch (ArgumentException argEx)
        {
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while Updating Employee.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        try
        {
            var departmentBo = await _departmentService.DeleteAsync(id);

            if (departmentBo == null)
            {
                return NotFound();
            }

            var departmentResponse = _mapper.Map<DepartmentResponseBO>(departmentBo);

            return Ok(new
            {
                Message = "Department Deleted successfully",
                Data = departmentResponse
            });
        }
        catch (ArgumentException argEx)
        {
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while Deleting Employee.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
