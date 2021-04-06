using BlzLogin.Report;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlzLogin.Data
{
    public class Order : PageModel
    {
        public int OrderNum { get; set; } = 0;
        public string ClientName { get; set; } = "";
        public int Total { get; set; } = 0;
        public void GeneratePDF(IJSRuntime js)
        {
            List<Order> oOrders = new List<Order>();
            for (int i = 1; i <= 9; i++)
            {
                oOrders.Add(new Order()
                {
                    OrderNum = i,
                    ClientName = "Client " + i,
                    Total = 1000 * i
                });
            }
            RptOrder oRptOrder = new RptOrder();
            js.InvokeAsync<Order>(
                "saveAsFile",
                "OrderList.pdf",
                Convert.ToBase64String(oRptOrder.Report(oOrders))

            );
        }
    }

}
