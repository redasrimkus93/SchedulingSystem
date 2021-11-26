using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public IActionResult InsertCompany([FromBody] InsertCompanyDto company)
        {
            if (ModelState.IsValid)
            {
                var result = _companyService.InsertCompany(company);

                if (result.IsSuccess)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest("Invalid company");

            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(Guid id)
        {
            var result = _companyService.GetCompanyById(id);

            if (result.CompanyId == Guid.Empty)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
