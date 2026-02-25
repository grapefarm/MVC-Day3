using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadFile02ApiController : ControllerBase
	{

		private readonly IWebHostEnvironment _env;
		private readonly string _uploads;

		public UploadFile02ApiController(IWebHostEnvironment env)
		{
			_env = env;
			//找到本專案的根目錄，計算出要存放的路徑
			_uploads = Path.Combine(_env.WebRootPath, "uploads");
		}
	}
}
