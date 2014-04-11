using System;
using System.ComponentModel.DataAnnotations;

namespace MagenicManila.Brownbag.Data.Entities
{
    [Serializable]
	public class Person
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Gender { get; set; }
	}
}