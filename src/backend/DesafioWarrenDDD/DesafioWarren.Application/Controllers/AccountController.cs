using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWarren.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize("Bearer")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AccountResponseDto> Deposit([FromBody] AccountRequestDto accountRequest)
        {
            try
            {
                var response = this._accountService.Deposit(accountRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AccountResponseDto> Withdraw([FromBody] AccountRequestDto accountRequest)
        {
            try
            {
                var response = this._accountService.Withdraw(accountRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AccountResponseDto> Payment([FromBody] AccountRequestDto accountRequest)
        {
            try
            {
                var response = this._accountService.Payment(accountRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
