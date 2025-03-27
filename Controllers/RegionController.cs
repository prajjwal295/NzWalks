using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Data;
using NzWalks.API.Model.Domain;
using NzWalks.API.Model.DTO;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NzWalksDbContext _dbContext;
        // constructor DI of the db context
        public RegionController(NzWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var resultDomain = _dbContext.Regions.ToList();

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
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var resultDomain = _dbContext.Regions.Find(id);

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
        public IActionResult Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            var regionDomain = new Region()
            {
                Name = createRegionRequestDto.Name,
                Code = createRegionRequestDto.Code,
                RegionImageUrl = createRegionRequestDto.RegionImageUrl
            };

            // this will add the data into the db;
            _dbContext.Regions.Add(regionDomain);
            _dbContext.SaveChanges();

            var regionResponse = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // createdAtActions gives the status code 200
            // here we can sen the aditional Location in the response header which specifies the exact url for that response
            return CreatedAtAction(nameof(GetRegionById), new { id = regionResponse.Id }, regionResponse);
        }

        [Route("{id:Guid}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModal = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModal == null)
                return NotFound();

            regionDomainModal.Code = updateRegionRequestDto.Code;
            regionDomainModal.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            regionDomainModal.Name = updateRegionRequestDto.Name;

            _dbContext.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModal = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModal == null)
                return NotFound();

            _dbContext.Regions.Remove(regionDomainModal);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
