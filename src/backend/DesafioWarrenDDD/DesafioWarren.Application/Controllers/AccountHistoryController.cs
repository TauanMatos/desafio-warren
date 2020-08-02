using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
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
    public class AccountHistoryController : ControllerBase
    {
        private readonly IAccountMovementService _accountMovementService;
        public AccountHistoryController(IAccountMovementService accountMovementService)
        {
            this._accountMovementService = accountMovementService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IList<AccountMovementResponseDto>> GetAccountHistory(int id)
        {
            try
            {
                var accountMovements = this._accountMovementService.GetAccountMovement(id);
                return Ok(accountMovements);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
