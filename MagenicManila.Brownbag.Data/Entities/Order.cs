using System.ComponentModel.DataAnnotations;

namespace MagenicManila.Brownbag.Data.Entities
{
	public class Order
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int PersonId { get; set; }
		[Required]
		public string OrderDetails { get; set; }
		public virtual Person Person { get; set; }
	}
}