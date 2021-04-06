using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        //       [Required]
        //       [Range(1, 100000)]
        public int SuperiorWorkOrderNumber { get; set; }

        //      [Required]
        //      [Range(1, 100000)]
        public int CustomerOrderNumber { get; set; }

        public string Metal { get; set; }

        //      [Required]
        //      [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
