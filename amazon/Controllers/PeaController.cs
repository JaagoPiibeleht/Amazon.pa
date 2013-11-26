using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using amazon.AWSE;
using amazon.Models;
using amazon.Currency;

namespace amazon.Controllers
{
    public class PeaController : Controller
    {
        private const int postlehel = 13;
        static IEnumerable<Item> kogu;
        public ActionResult Index()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Index(string sona)
        {
            if (kogu == null)
            {
                try
                {
                    andmed(sona);
                }
                catch (Exception e)
                {
                    ViewBag.error = e.Message;
                    return View();
                }
            }
            return RedirectToAction("Otsing");
        }
        public ActionResult Otsing(int? id)
        {
            ViewBag.Currency = Enum.GetNames(typeof(Currency.Currency));            
            int lehenumber = id ?? 0;
            
            IEnumerable<Item> products = (from item in kogu
                                          select item).Skip(lehenumber * postlehel).Take(postlehel + 1);
            ViewBag.IsPreviousLinkVisible = lehenumber > 0;
            ViewBag.IsNextLinkVisible = products.Count() > postlehel;
            ViewBag.LeheNumber = lehenumber;

            return View(products.Take(postlehel));
        }
        public double Currency(string from, string to)
        {
            CurrencyConvertorSoapClient client = new CurrencyConvertorSoapClient();
            var rate = client.ConversionRate((amazon.Currency.Currency)Enum.Parse(typeof(amazon.Currency.Currency), from),
                (amazon.Currency.Currency)Enum.Parse(typeof(amazon.Currency.Currency), to));
            return rate;
        }
        public ActionResult BackToMain()
        {
            kogu = null;
            return RedirectToAction("Index");
        }
        static void andmed(string sona)
        {
            productReponse vastus = new productReponse();
            for (int i = 1; i <= 5; i++)
            {
                IEnumerable<Item> itempage = vastus.GetProducts(sona, i);
                if (kogu == null)
                {
                    kogu = itempage;
                }
                else
                {
                    kogu = kogu.Concat(itempage);
                }
            }
        }
    }
}
