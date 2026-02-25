using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
	public class UploadFile01Controller : Controller
	{
		private readonly IWebHostEnvironment _env;
		private readonly string _uploads;

		public UploadFile01Controller(IWebHostEnvironment env)
		{
			_env = env;
			//找到本專案的根目錄，計算出要存放的路徑
			_uploads = Path.Combine(_env.WebRootPath, "uploads");
		}

		public IActionResult Create(IFormFile f)
		{
			
			var allowExts = new string[] { ".jpg", ".jpeg", ".png" };
			//判斷沒有上傳f
			if (f==null || f.Length == 0)
			{
				ModelState.AddModelError("", "必須上傳檔案");
			}
			else
			{
				//判斷副檔名，檔名及大小，......是否符合要求
				string ext= Path.GetExtension(f.FileName); //".jpg"
				if (allowExts.Contains(ext) == false)
				{
					ModelState.AddModelError("", "副檔名不符合規定");
				}
				else
				{
					//為了避免檔名重複，必須取一個唯一的檔名，但副檔名相同
					//ToString()會生成一個有"-"的字串
					//ToString("N")會生成一個沒有"-"的字串，較短，較不易超出windows的檔名長度限制
					string newFileName = Guid.NewGuid().ToString("N") + ext;

					//存檔
					
					var filePath = Path.Combine(_uploads, newFileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						f.CopyTo(stream);
					}

					//todo新增紀錄
				}
			}

				return View();
		}

		private string UploadProductImage(IFormFile f) {
			var allowExts = new string[] { ".jpg", ".jpeg", ".png" };
			
			//判斷沒有上傳f
			if (f == null || f.Length == 0)
			{
				throw new Exception("必須上傳檔案");
			}

			//判斷副檔名，檔名及大小，......是否符合要求
			string ext = Path.GetExtension(f.FileName); //".jpg"
			if (allowExts.Contains(ext) == false)
			{
				throw new Exception("副檔名不符合規定");
			}

			//為了避免檔名重複，必須取一個唯一的檔名，但副檔名相同
			string newFileName = Guid.NewGuid().ToString("N") + ext;

			//存檔
			var filePath = Path.Combine(_uploads, newFileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				f.CopyTo(stream);
			}
		
			return newFileName;
		}
	}
}
