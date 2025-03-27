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
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            this._regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //var resultDomain = _dbContext.Regions.ToList();
            var resultDomain = await _regionRepository.GetAllAsync();
            var resultDto = new List<RegionDto>();

            foreach(var result in resultDomain)
            {
                resultDto.Add(new RegionDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Code = result.Code,
                    RegionImageUrl = result.RegionImageUrl
                });
            }
            return Ok(resultDto);
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var resultDomain = await _regionRepository.GetByIdAsync(id);

            if (resultDomain == null)
                return NotFound();

            var resultDto = new RegionDto()
            {
                Id = resultDomain.Id,
                Name = resultDomain.Name,
                Code = resultDomain.Code,
                RegionImageUrl = resultDomain.RegionImageUrl
            };

            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            var regionDomain = new Region()
            {
                Name = createRegionRequestDto.Name,
                Code = createRegionRequestDto.Code,
                RegionImageUrl = createRegionRequestDto.RegionImageUrl
            };

            var region =  await _regionRepository.CreateAsync(regionDomain);

            var regionResponse = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            // createdAtActions gives the status code 200
            // here we can sen the aditional Location in the response header which specifies the exact url for that response
            return CreatedAtAction(nameof(GetRegionById), new { id = regionResponse.Id }, regionResponse);
        }

        [Route("{id:Guid}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = new Region()
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            var regionDomainModal = await _regionRepository.UpdateAsync(id , regionDomain);

            if (regionDomainModal == null)
                return NotFound();

            var regionResponse = new RegionDto()
            {
                Id = regionDomainModal.Id,
                Code = regionDomainModal.Code,
                Name = regionDomainModal.Name,
                RegionImageUrl = regionDomainModal.RegionImageUrl
            };

            return Ok(regionResponse);
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
