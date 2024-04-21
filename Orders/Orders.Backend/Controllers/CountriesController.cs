using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        [HttpGet]
        public async override Task<ActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async override Task<ActionResult> GetAsync(int id)
        {
            var response = await _countriesUnitOfWork.GetAsync(id);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }
    }

}
