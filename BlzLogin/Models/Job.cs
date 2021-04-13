using BlzLogin.Report;
using Microsoft.JSInterop;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlzLogin.Models
{
    public class Job
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Superior Work Order Number is required")]
        [Range(1, 10000000,  ErrorMessage = "Superior Work Order Number must be between 1 and 10000000")]
        public int SuperiorWorkOrderNumber { get; set; }

        [Required]
        [Range(1, 10000000, ErrorMessage = "Customer Order Number must be between 1 and 10000000")]
        public int CustomerOrderNumber { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string Quantity { get; set; } = null;

        public string Size { get; set; } = null;

        [Required(ErrorMessage = "Customer Part Number is required")]
        [Range(1, 10000000, ErrorMessage = "Customer Part Number must be between 1 and 10000000")]
        public int CustomerPartNumber { get; set; }

        [Required]
        public bool ITAR { get; set; }

        [Required]
        public bool HOT { get; set; }

        [Required(ErrorMessage = "Choose a metal type")]
        [DataType(DataType.Text)]
        public string Metal { get; set; }

        [Required(ErrorMessage = "Must specify the minimum thickness")]
        [Range(.00000001, 10000000, ErrorMessage = "Minimum Thickness is required")]
        public double ThicknessMin { get; set; }

        [Required(ErrorMessage = "Must specify the maximum thickness")]
        [Range(.00000001, 10000000, ErrorMessage = "Maximum Thickness is required")]
        public double ThicknessMax { get; set; }

        public string SerialNumbers { get; set; } = null;

        public string OverallRequirements { get; set; } = null;

        [Required(ErrorMessage = "Shipping Carrier is required")]
        [DataType(DataType.Text)]
        public string ShippingCarrier { get; set; }

        [Required(ErrorMessage = "Shipping Speed is required")]
        [DataType(DataType.Text)]
        public string ShippingSpeed { get; set; }

        [Required(ErrorMessage = "Job Reciever is required")]
        [DataType(DataType.Text)]
        public string JobReciever { get; set; }

        public void GeneratePDF(IJSRuntime js, Job job)
        {
            RptJob jobreport = new RptJob();
            js.InvokeAsync<Job>(
                "saveAsFile",
                "OrderList.pdf",
                Convert.ToBase64String(jobreport.Report(job))

            );
        }
    }
}