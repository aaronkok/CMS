using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class ShippingItem
    {
        public virtual long Id { get; set; }
        public virtual string Category { get; set; }
        public virtual string Name { get; set; }
        public virtual int Amount { get; set; }
        public virtual decimal EstimateWeightage { get; set; }

        public virtual long ShippingOrderId { get; set; }
        [ForeignKey(nameof(ShippingOrderId))]
        public virtual ShippingOrder ShippingOrder { get; set; }
    }
}