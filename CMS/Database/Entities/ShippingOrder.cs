using CMS.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class ShippingOrder
    {
        public virtual long Id { get; set; }
        public virtual string TrackingId { get; set; }
        public virtual decimal ActualTonnage { get; set; }
        public virtual ShippingStatus Status { get; set; }
        public virtual DateTime CreationDate { get; set; }

        public virtual long BillingAddressId { get; set; }
        [ForeignKey(nameof(BillingAddressId))]
        public virtual ShippingBillingAddress BillingAddress { get; set; }

        public virtual long DestinationAddressId { get; set; }
        [ForeignKey(nameof(DestinationAddressId))]
        public virtual ShippingDestinationAddress DestionationAddress { get; set; }

        public virtual string ShipmentOwnerId { get; set; }

        [ForeignKey(nameof(ShipmentOwnerId))]
        public virtual ApplicationUser ShipmentOwner { get; set; }


        public virtual ICollection<ShippingItem> Items { get; set; }
    }
}