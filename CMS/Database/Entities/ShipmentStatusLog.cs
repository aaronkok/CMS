using CMS.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class ShipmentStatusLog
    {
        public virtual long Id { get; set; }

        public virtual long ShippingOrderId { get; set; }
        [ForeignKey(nameof(ShippingOrderId))]
        public virtual ShippingOrder ShippingOrder { get; set; }

        public virtual ShippingStatus FromStatus { get; set; }
        public virtual ShippingStatus ToStatus { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual string UpdatorId { get; set; }
        [ForeignKey(nameof(UpdatorId))]
        public virtual ApplicationUser Updator { get; set; }
    }
}