using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Test.Models
{
    public class PaymentCharge
    {
        public string payment_type { get; set; }
        public Transaction_Details transaction_details { get; set; }
        public MidtransCreditCard credit_card { get; set; }
        public BankTransfer bank_transfer { get; set; }
        public IEnumerable<Item_Details> item_details { get; set; }
        public Customer_Details customer_details { get; set; }
    }

    public class Transaction_Details
    {
        public string order_id { get; set; }
        public int gross_amount { get; set; }
    }

    public class Item_Details
    {
        public string id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string merchant_name { get; set; }
    }

    public class Customer_Details
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Billing_Address billing_address { get; set; }
    }

    public class Billing_Address
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string MyProperty { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }

    // Payment Method Class

    public class MidtransCreditCard
    {
        public string token_id { get; set; }
        public bool authentication { get; set; }
        public bool save_token_id { get; set; }
    }

    public class BankTransfer {
        public string bank { get; set; }
        public string va_number { get; set; }

    }

    public class GetSaveToken
    {
        public string saved_token_id { get; set; }
        public string saved_token_id_expired_at { get; set; }
    }

}
