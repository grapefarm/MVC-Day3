using System.ComponentModel.DataAnnotations;

namespace WebApplication1_search.ViewModels
{
	public class ProductIndexItemViewModel
	{
		public int Id { get; set; }

		[Display(Name = "分類名稱")]
		public string CategoryName { get; set; }

		[Display(Name = "商品名稱")]
		public string ProductName { get; set; }

		[Display(Name = "售價")]
		public decimal UnitPrice { get; set; }
	}
}
