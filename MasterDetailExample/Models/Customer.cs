using System.ComponentModel.DataAnnotations;

namespace MasterDetailExample.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get;set; }
    }
}