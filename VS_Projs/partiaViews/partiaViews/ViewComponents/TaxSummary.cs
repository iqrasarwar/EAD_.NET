using Microsoft.AspNetCore.Mvc;

namespace partiaViews.ViewComponents
{
    public class TaxSummary : ViewComponent
    {

        public IViewComponentResult Invoke(int taxRate, int totalAmount)
        {
            ViewData["1"] = 1;
            ViewData["2"] = "2";
            ViewData["3"] = "3";
            ViewData["TaxRate"] = taxRate;
            ViewData["TotalAmount"] = totalAmount;
            object myName = "iqra";
            return View(myName);
        }
    }
}
