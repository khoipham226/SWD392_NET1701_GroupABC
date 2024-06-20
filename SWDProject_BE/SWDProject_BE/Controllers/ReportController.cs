﻿using BusinessLayer.Services;
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

        [HttpGet]
        [Route("GetAllValidReport")]
        public async Task<IActionResult> GetAllValidReport()
        {
            try
            {
                var report = await _reportService.GetAllValidReport();
                return Ok(report);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllReportByUserId/{UserId}")]
        public async Task<IActionResult> GetAllReportByUserId(int UserId)
        {
            try
            {
                var report = await _reportService.GetReportByUserId(UserId);
                if(report != null)
                {
                    return Ok(report);
                }
                else
                {
                    return NotFound("Not found UserId!");
                }
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("GetAllReportByPostrId/{PostId}")]
        public async Task<IActionResult> GetAllReportByPostrId(int PostId)
        {
            try
            {
                var report = await _reportService.GetReportByPostId(PostId);
                if (report != null)
                {
                    return Ok(report);
                }
                else
                {
                    return NotFound("Not found PostId!");
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

}
