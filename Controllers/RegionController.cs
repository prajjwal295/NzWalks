using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.CustomActionFilter;
using NzWalks.API.Data;
using NzWalks.API.Model.Domain;
using NzWalks.API.Model.DTO;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository , IMapper mapper)
        {
            this._mapper = mapper;
            this._regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //var resultDomain = _dbContext.Regions.ToList();
            var resultDomain = await _regionRepository.GetAllAsync();
            //var resultDto = new List<RegionDto>();


            // older approach
            //foreach(var result in resultDomain)
            //{
            //    resultDto.Add(new RegionDto()
            //    {
            //        Id = result.Id,
            //        Name = result.Name,
            //        Code = result.Code,
            //        RegionImageUrl = result.RegionImageUrl
            //    });
            //}

            // Auto Mapper Approach
            var resultDto = _mapper.Map<List<RegionDto>>(resultDomain);
            return Ok(resultDto);
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var resultDomain = await _regionRepository.GetByIdAsync(id);

            if (resultDomain == null)
                return NotFound();

            //var resultDto = new RegionDto()
            //{
            //    Id = resultDomain.Id,
            //    Name = resultDomain.Name,
            //    Code = resultDomain.Code,
            //    RegionImageUrl = resultDomain.RegionImageUrl
            //};

            var resultDto = _mapper.Map<RegionDto>(resultDomain);
            return Ok(resultDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            //if(ModelState.IsValid)
            //{
                //var regionDomain = new Region()
                //{
                //    Name = createRegionRequestDto.Name,
                //    Code = createRegionRequestDto.Code,
                //    RegionImageUrl = createRegionRequestDto.RegionImageUrl
                //};

                var regionDomain = _mapper.Map<Region>(createRegionRequestDto);

                var region = await _regionRepository.CreateAsync(regionDomain);

                //var regionResponse = new RegionDto()
                //{
                //    Id = region.Id,
                //    Name = region.Name,
                //    Code = region.Code,
                //    RegionImageUrl = region.RegionImageUrl
                //};

                var regionResponse = _mapper.Map<RegionDto>(region);
                // createdAtActions gives the status code 200
                // here we can sen the aditional Location in the response header which specifies the exact url for that response
                return CreatedAtAction(nameof(GetRegionById), new { id = regionResponse.Id }, regionResponse);
            //}
            //else
            //{
            //    // it returns of error type 400
            //    return BadRequest(ModelState);
            //}

        }

        [Route("{id:Guid}")]
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //if(ModelState.IsValid)
            //{
            //var regionDomain = new Region()
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            var regionDomain = _mapper.Map<Region>(updateRegionRequestDto);

            var regionDomainModal = await _regionRepository.UpdateAsync(id , regionDomain);

            if (regionDomainModal == null)
                return NotFound();

            //var regionResponse = new RegionDto()
            //{
            //    Id = regionDomainModal.Id,
            //    Code = regionDomainModal.Code,
            //    Name = regionDomainModal.Name,
            //    RegionImageUrl = regionDomainModal.RegionImageUrl
            //};

            var regionResponse = _mapper.Map<RegionDto>(regionDomainModal);

            return Ok(regionResponse);

            //}
            //else
            //{
            //    return BadRequest();
            //}
        }

        [Route("{id:Guid}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _regionRepository.DeleteAsync(id);

            if (response == null)
                return NotFound();

            return Ok();
        }
    }
}
