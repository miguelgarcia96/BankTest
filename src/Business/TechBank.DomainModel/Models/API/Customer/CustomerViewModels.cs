using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel.Models.API.Customer
{
    public class TransferVM
    {
        [Required]
        public string SourceId { get; set; }
        [Required]
        public string DestinationId { get; set; }

        [Required]
        public decimal? Amount { get; set; }
    }

    public class TransferResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
