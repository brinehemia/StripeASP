using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using Stripe_Test.Helpers;
using Stripe_Test.Models;
using Stripe_Test.Services;
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
        private IGetResponseService _getResponse;
        private IPostResponseService _postResponse;

        public HomeController(ILogger<HomeController> logger, IGetResponseService getResponse, IPostResponseService postResponse)
        {
            _getResponse = getResponse;
            _postResponse = postResponse;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SnapTest()
        {
            return View();
        }

        //public void PaymentTest()
        //{
        //    //StripeConfiguration.ApiKey = "sk_test_51JWugLGMu6Ri5kpyJPp0h64O5rl1AnI6shx9psxhVbE0sD1rCCzpRNDfF0KpLYdMlQvtSS09Ea16x0eJ19snItNi00xgGcgtFN";

        //    var options = new PaymentIntentCreateOptions
        //    {
        //        Amount = 5,
        //        Currency = "usd",
        //        PaymentMethodTypes = new List<string>
        //          {
        //            "card",
        //          },
        //        ReceiptEmail = "brinehemia@gmail.com"
        //    };

        //    var service = new PaymentIntentService();
        //    var paymentIntent = service.Create(options);

        //}

        //public IActionResult PaymentWithReturnView(string stripeEmail, string stripeToken)
        //{
        //    var charges = new ChargeService();

        //    var customer = new CustomerService().Create(new CustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        Source = stripeToken
        //    });

        //    var Charge = charges.Create(new ChargeCreateOptions
        //    {
        //        Amount = 500,
        //        Currency = "usd",
        //        Description = "Test Charge",
        //        Customer = customer.Id,
        //        ReceiptEmail = stripeEmail,
        //        Metadata = new Dictionary<string, string>()
        //        {
        //            { "TransaksiId", "TST736476" },
        //            { "Paket", "GreetUp-01" }
        //        }
        //    });

        //    if (Charge.Status == "succeeded") return RedirectToAction("SuccessPayment");
        //    else return RedirectToAction("ErrorPayment");
        //}

        //public void CheckoutTest()
        //{
        //    var options = new SessionCreateOptions
        //    {
        //        LineItems = new List<SessionLineItemOptions>
        //        {
        //          new SessionLineItemOptions
        //          {
        //            Amount = 5,
        //            Currency = "usd"
        //          },
        //        },
        //        PaymentMethodTypes = new List<string>
        //        {
        //          "card",
        //        },
        //        Mode = "payment",
        //        SuccessUrl = "/Home/SuccessPayment",
        //        CancelUrl = "/Home/ErrorPayment",
        //    };
        //    var service = new SessionService();
        //    Session session = service.Create(options);
        //}

        public async Task<IActionResult> TestAjaIni(GetSaveToken test)
        {
            return Ok(test);
        }

        public async Task<IActionResult> GetSnapToken()
        {
            var Generate = new SnapModel()
            {
                transaction_details = new Transaction_Details
                {
                    order_id = "GreetUp_Payment_" + System.Guid.NewGuid().ToString().Replace("-", ""),
                    gross_amount = 5000
                },
                item_details = new List<Item_Details>()
                {
                    new Item_Details() {
                        id = "SubscriptionPlanCode",
                        price = 5000,
                        quantity = 1,
                        name = "Starter Plan",
                        brand = "GreetUp",
                    }
                },
                customer_details = new Customer_Details()
                {
                    first_name = "asdd",
                    last_name = "Nehassssemia",
                    email = "asddd@gmail.com",
                    phone = "+62812 4356 7847",
                    billing_address = new Billing_Address
                    {
                        first_name = "asdd",
                        last_name = "Nehassssemia",
                        email = "asddd@gmail.com",
                        phone = "+62812 4356 7847",
                        address = "Sumbersari",
                        city = "Malang",
                        country_code = "IDN"
                    }
                },
                credit_card = new Snap_CreditCard()
                {
                    secure = true,
                    save_card = true,
                },
                user_id = System.Guid.NewGuid().ToString().Replace("-", ""),
                bca_va = new Snap_VA()
                {
                    va_number = "66666666666", // Set Custom VA BCA. 6-11 Digit
                    free_text = new FreeText_BCA
                    {
                        inquiry = new List<FreeText_BCA_Detail>()
                        {
                            new FreeText_BCA_Detail()
                            {
                                id = "dah makan belum?",
                                en = "Have you eaten already?"
                            }
                        },
                        payment = new List<FreeText_BCA_Detail>()
                        {
                            new FreeText_BCA_Detail()
                            {
                                id = "Payment Greetup",
                                en = "Payment Greetup"
                            }
                        }
                    }
                },
                bni_va = new Snap_VA()
                {
                    va_number = "88888888" // Set Custom VA BNI. 8 atau 12 digit
                },
                permata_va = new Snap_VA()
                {
                    va_number = "1010101010", // Set Custom VA Permata. 10 digit
                    recipient_name = "GreetUp Account Bank Name"
                },
                bri_va = new Snap_VA()
                {
                    va_number = "181818181818181818" // Set Custom VA BRI. 8 atau 18 digit
                },
                
            };

            var obj = JsonConvert.SerializeObject(Generate, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var response = await _postResponse.PostResponseWithAuth("transactions", obj);

            var getSnapToken = JsonConvert.DeserializeObject<SnapToken>(response.Content);

            return Ok(getSnapToken);
        }


        public async Task<IActionResult> PaymentCard(string cardtoken_id)
        {
            try
            {
                var Payment = new PaymentCharge()
                {
                    payment_type = "credit_card",
                    transaction_details = new Transaction_Details
                    {
                        order_id = "GreetUp_Payment_" + "asdddasdddas",
                        gross_amount = 5000
                    },
                    credit_card = new MidtransCreditCard
                    {
                        token_id = cardtoken_id,
                        save_token_id = true
                    },
                    item_details = new List<Item_Details>()
                    {
                        new Item_Details() {
                            id = "SubscriptionPlanCode",
                            price = 5000,
                            quantity = 1,
                            name = "Starter Plan",
                            brand = "GreetUp",
                        }
                    },
                    customer_details = new Customer_Details()
                    {
                        first_name = "Brian",
                        last_name = "Nehemia",
                        email = "briannehemia@gmail.com",
                        phone = "+62812 5214 7847",
                        billing_address = new Billing_Address
                        {
                            first_name = "Brian",
                            last_name = "Nehemia",
                            email = "briannehemia@gmail.com",
                            phone = "+62812 5214 7847",
                            address = "Sumbersari",
                            city = "Malang",
                            country_code = "IDN"
                        }
                    }
                };

                var obj = JsonConvert.SerializeObject(Payment, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                var response = await _postResponse.PostResponseWithAuth("charge", obj);

                return Ok(response);



            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
