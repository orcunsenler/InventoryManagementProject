using System.ComponentModel.DataAnnotations;

namespace InventoryManagementProject.Models
{
	public class Admin
	{
		[Key]
		public int AdminId { get; set; }
		[StringLength(20)]
		public string UserName { get; set; }
		[StringLength(20)]
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
