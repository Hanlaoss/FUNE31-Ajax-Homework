using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class ApiController : Controller
	{
		private readonly MyDBContext _dbContext;

        public ApiController(MyDBContext dbContext)
        {
			_dbContext = dbContext;
		}

        public IActionResult Index()
		{
			System.Threading.Thread.Sleep(5000);
			//return View();
			//return Content("Helle Content");
			//return Content("<h2>Helle Content</h2>","text/html");
			return Content("<h2>Helle Content</h2>","text/html",System.Text.Encoding.UTF8);
		}

		
		public IActionResult Cities()
		{
			var cities = _dbContext.Addresses.Select(a=>a.City).Distinct();
			return Json(cities);
		}


		public IActionResult Districts(string city)
		{
			var districts = _dbContext.Addresses.Where(a=>a.City == city).Select(a => a.SiteId).Distinct();
			return Json(districts);

		}

		public IActionResult Roads(string district)
		{
			var roads = _dbContext.Addresses.Where(a =>a.SiteId == district).Select(a => a.Road).Distinct();
			return Json(roads);
		}

		public IActionResult CheckAccount(string name)
		{
			return Json(_dbContext.Members.Any(m => m.Name == name));
		}

		public IActionResult Avatar(int id = 1)
		{
			Member? member = _dbContext.Members.Find(id);
			if (member != null)
			{
				byte[] img = member.FileData;
				return File(img, "image/png");
			}
			return NotFound();
		}

		public IActionResult First()
		{
			
			return View("First");
		}

		public IActionResult Register(string name , int age = 23)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "Guest";
			}
				return Content($"Hello {name}, You are already {age} years old.");
		}
	}
}
