using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWDProject_BE.Controllers
{
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("GetAllReport")]
        public async Task<IActionResult> GetAllReport()
        {
            try
            {
                var report = await _reportService.GetAll();
                return Ok(report);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

}
