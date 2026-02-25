using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1_search.Models;
using WebApplication1_search.Models.EfModels;
using WebApplication1_search.ViewModels;

namespace WebApplication1_search.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ISpanDemoContext _context;

		public ProductsController(ISpanDemoContext context)
		{
			_context = context;
		}

		//從URL Query String接收搜尋條件 ?productName=xxx&priceStart=100&priceEnd=500
		public IActionResult Index(ProductCriteria criteria)
		{
			//取得紀錄
			IQueryable<Product> query = _context.Products
				.AsNoTracking()  //不追蹤變更，提升效能
				.Include(x => x.Category) //inner join，一次撈出產品與分類資料
				.OrderBy(x => x.Category.DisplayOrder) //先依照分類排序
				.ThenBy(x => x.UnitPrice); // 再依照售價排序

			if (criteria.PriceStart.HasValue)
			{
				query = query.Where(x => x.UnitPrice >= criteria.PriceStart.Value);
			}	
			if (criteria.PriceEnd.HasValue)
			{
				query = query.Where(x => x.UnitPrice <= criteria.PriceEnd.Value);
			}
			if (string.IsNullOrEmpty(criteria.ProductName) == false)
			{
				query = query.Where(x => x.ProductName.Contains(criteria.ProductName));
			}

			List<ProductIndexItemViewModel> data = query
				.Select(x => new ProductIndexItemViewModel
				{
					Id = x.Id,
					CategoryName = x.Category.CategoryName,
					ProductName = x.ProductName,
					UnitPrice = x.UnitPrice
				})
				.ToList();  //LINQ的延遲執行在此時才會真正執行SQL查詢

			var vm = new ProductIndexViewModel
			{
				Data = data,
				Criteria = criteria
			};

			return View(vm);
		}
	}
}
