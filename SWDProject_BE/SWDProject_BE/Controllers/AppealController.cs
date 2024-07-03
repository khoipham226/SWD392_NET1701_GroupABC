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
        [Route("GetAllByUserId/{UserId}")]
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

        [HttpPost]
        [Route("AcceptAppeal/{AppealId}")]
        public async Task<IActionResult> AcceptAppeal(int AppealId)
        {
            try
            {
                var result = await _appealService.AcceptAppeal(AppealId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("not found Appeal!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
