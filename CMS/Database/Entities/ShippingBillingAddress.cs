using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class ShippingBillingAddress
    {
        public virtual long Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
    }
}