using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Data;
using NzWalks.API.Model.Domain;
using NzWalks.API.Model.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this._walkRepository = walkRepository;
            this._mapper = mapper;
        }

        //Applying the filtering here based on column name and column value
        // https://localhost:7024/api/Walk?filterOn=name&filterQuery=the&sortBy=name&isAscending=false&pageNumber=1&pageSize=1000
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn , [FromQuery]string ? filterQuery , [FromQuery] string? sortBy , [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 1000)
        {
            List<Walk> walk = await _walkRepository.GetAllAsync(pageNumber, pageSize, isAscending ?? true, sortBy, filterOn, filterQuery);
            return Ok(_mapper.Map<List<WalkDto>>(walk));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetByIdAsync(id);

            if (walk == null)
                return NotFound();

            return Ok(_mapper.Map<WalkDto>(walk));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                // map Dto to domain model
                var walkDomainModal = _mapper.Map<Walk>(addWalkRequestDto);
                var walk = await _walkRepository.CreateAsync(walkDomainModal);

                //Map domain model to dto;
                return Ok(_mapper.Map<WalkDto>(walk));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult?> Delete([FromRoute] Guid id)
        {
            var walk = await _walkRepository.DeleteByIdAsync(id);

            return Ok(_mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult?> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walkDomailModal = _mapper.Map<Walk>(updateWalkRequestDto);
                var walk = await _walkRepository.UpdateByIdAsync(id, walkDomailModal);

                if (walk == null)
                    return NotFound();

                return Ok(_mapper.Map<WalkDto>(walk));

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
