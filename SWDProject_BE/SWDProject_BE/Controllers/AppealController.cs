using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWDProject_BE.Controllers
{
    [Route("api/Appeal/")]
    [ApiController]
    public class AppealController : ControllerBase
    {
        private readonly IAppealService _appealService;

        public AppealController(IAppealService appealService)
        {
            _appealService = appealService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _appealService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAll/{UserId}")]
        public async Task<IActionResult> GetAllByUserId(int UserId)
        {
            try
            {
                var result = await _appealService.GetAllByUserId(UserId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("not found User!");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
