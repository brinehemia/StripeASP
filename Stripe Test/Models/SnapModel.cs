using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Test.Models
{
    public class SnapToken {
        public string token { get; set; }
        public string redirect_url { get; set; }
    }

    public class SnapModel
    {
        public Transaction_Details transaction_details { get; set; }
        public IEnumerable<Item_Details> item_details { get; set; }
        public Customer_Details customer_details { get; set; }
        public Snap_CreditCard credit_card { get; set; }
        public Snap_VA bca_va { get; set; }
        public Snap_VA bni_va { get; set; }
        public Snap_VA permata_va { get; set; }
        public Snap_VA bri_va { get; set; }
        public string user_id { get; set; }
    }

    public class Snap_CreditCard
    {
        public bool secure { get; set; }
        public bool save_card { get; set; }
    }

    public class Snap_VA
    {
        public string va_number { get; set; }
        public string recipient_name { get; set; } // Khusus Permata
        public string sub_company_code { get; set; } // Khusus BCA
        public FreeText_BCA free_text { get; set; } // Khusus BCA
    }

    public class FreeText_BCA
    {
        public IEnumerable<FreeText_BCA_Detail> inquiry { get; set; }
        public IEnumerable<FreeText_BCA_Detail> payment { get; set; }
        
    }

    public class FreeText_BCA_Detail {
        public string id { get; set; }
        public string en { get; set; }
    }

}
