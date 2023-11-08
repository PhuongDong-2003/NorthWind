using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Repository;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
           private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

         [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var accounts = _accountRepository.GetAll();
                return Ok(accounts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("{UserName}")]
        public IActionResult GetAll( string UserName)

        {
            try
            {
                var accounts = _accountRepository.GetByUsername(UserName);
                return Ok(accounts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    

    }
}