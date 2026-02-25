namespace WebApplication3.Models
{
	public class Info
	{
		public string Name { get; set; }
		public string Email { get; set; }

		//field吃不到model binding的效果
		public string Address;
	}
}
