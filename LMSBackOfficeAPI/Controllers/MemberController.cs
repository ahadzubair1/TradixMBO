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




namespace LMSBackOfficeAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MemberController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<MemberController> _logger;

		public MemberController(ILogger<MemberController> logger)
		{
			_logger = logger;
		}

		//[HttpGet]
		//public IEnumerable<MemberController> GetMemberCheckOut()
		//{
		//	var rng = new Random();
		//	return Enumerable.Range(1, 5).Select(index => new MemberController
		//	{
		//		Date = DateTime.Now.AddDays(index),
		//		OrderID = rng.Next(-20, 55),
		//		BankName = Summaries[rng.Next(Summaries.Length)]
		//	})
		//	.ToArray();
		//}
	}
}
