using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using LMSBackOfficeAPI;
using LMSBackOfficeDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;



namespace LMSBackOfficeAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BonusTypesController : ControllerBase
    {
        public Result response;

        private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<BonusTypesController> _logger;

		public BonusTypesController(ILogger<BonusTypesController> logger)
		{
			          
            _logger = logger;
		}



     



        //[HttpGet]
        //[Authorize(Roles = "Admin, Staff")]
        //[Route("GetBonusTypeByID/{bonusTypeID:guid}")]
        //[ActionName("GetBonusTypeByID")]
        /*public IActionResult GetBonusTypeByID(Guid bonusTypeID)
        {
            try
            {
                   if (bonusTypeID != Guid.Empty)
                    {
                        var customerDetail = LMSBackOfficeDAL.GetBonusTypeByID0(bonusTypeID, out var errMsg);

                        response = FormatResponse(errMsg, HttpMethod.Get, bonusTypeDetails);
                    }
                    else
                    {
                        response = FormatResponse("Customer ID is required", HttpMethod.Get, null);
                    }
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid or unable to process your request", null);
                response = ConstructReturnResponse(104, "Invalid or unable to process your request");
            }

            return Ok(response);
        }*/


        public static Result ConstructReturnResponse(int statusCode, string message, object responseData = null)
        {
            return new Result
            {
                StatusCode = statusCode,
                Message = message,
                Data = responseData
            };
        }



        //public static Result FormatResponse(string errMsg, HttpMethod httpMethod, object responseData = null, string successMsg = "")
        //{
        //    var result = new Result();
        //    if (!string.IsNullOrEmpty(errMsg))
        //    {
        //        if (errMsg == SessionErrorMsg)
        //        {
        //            result = ConstructReturnResponse(103, errMsg); // means session expired, must login again
        //        }
        //        else
        //        {
        //            result = ConstructReturnResponse(104, errMsg);
        //        }
        //    }
        //    else
        //    {
        //        if (responseData != null)
        //        {
        //            switch (httpMethod)
        //            {
        //                case HttpMethod.Put:
        //                    result = ConstructReturnResponse(100, successMsg == "" ? "Updated Successfully" : successMsg, responseData);
        //                    break;

        //                case HttpMethod.Get:
        //                case HttpMethod.Delete:
        //                    result = ConstructReturnResponse(100, successMsg == "" ? "Action-Succeeded" : successMsg, responseData);
        //                    break;

        //                case HttpMethod.Post:
        //                    result = ConstructReturnResponse(101, successMsg == "" ? "Created Successfully" : successMsg, responseData); // here the status code must be 201 as per guidelines
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            result = ConstructReturnResponse(105, "No results found", null);
        //        }
        //    }

        //    return result;
        //}
        //[HttpGet("GetBonusTypesRequests")]
        public static DataTable GetBonusTypesRequests()
        {
            DataTable dtBonusTypes = new DataTable();
            try
            {
                dtBonusTypes = Setup_DataAccess.GetAllBonusTypes();
                if (dtBonusTypes != null && dtBonusTypes.Rows.Count > 0)
                {
                    return dtBonusTypes;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An Error Occurred During BONUS-TYPES Fetch Operation : " + ex.Message);
            }
            finally
            {

            }

            return dtBonusTypes;
        }

    }


   

    public class Result
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

}
