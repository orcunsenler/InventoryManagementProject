using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel;





namespace InventoryManagementProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public bool Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
