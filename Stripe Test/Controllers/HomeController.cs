using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using Stripe_Test.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void PaymentTest()
        {
            //StripeConfiguration.ApiKey = "sk_test_51JWugLGMu6Ri5kpyJPp0h64O5rl1AnI6shx9psxhVbE0sD1rCCzpRNDfF0KpLYdMlQvtSS09Ea16x0eJ19snItNi00xgGcgtFN";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 5,
                Currency = "usd",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
                ReceiptEmail = "brinehemia@gmail.com"
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

        }

        public IActionResult PaymentWithReturnView(string stripeEmail, string stripeToken)
        {
            var charges = new ChargeService();

            var customer = new CustomerService().Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var Charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Currency = "usd",
                Description = "Test Charge",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    { "TransaksiId", "TST736476" },
                    { "Paket", "GreetUp-01" }
                }
            });

            if (Charge.Status == "succeeded") return RedirectToAction("SuccessPayment");
            else return RedirectToAction("ErrorPayment");
        }

        public void CheckoutTest()
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Amount = 5,
                    Currency = "usd"
                  },
                },
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Mode = "payment",
                SuccessUrl = "/Home/SuccessPayment",
                CancelUrl = "/Home/ErrorPayment",
            };
            var service = new SessionService();
            Session session = service.Create(options);

        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult ErrorPayment()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
