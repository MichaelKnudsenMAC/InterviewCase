using System.ComponentModel.DataAnnotations;

namespace MachineMonitoring.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

    }
}
