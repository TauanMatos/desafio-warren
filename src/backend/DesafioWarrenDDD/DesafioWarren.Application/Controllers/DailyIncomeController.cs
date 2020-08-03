using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class DailyIncomeController : ControllerBase
    {
        private readonly IDailyIncomeService _dailyIncomeService;

        public DailyIncomeController(IDailyIncomeService dailyIncomeService)
        {
            this._dailyIncomeService = dailyIncomeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetDailyIncome(int clientId)
        {
            try
            {
                var result = this._dailyIncomeService.GetDailyIncome(clientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
