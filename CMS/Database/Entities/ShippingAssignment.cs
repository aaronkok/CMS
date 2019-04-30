using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class ShippingAssignment
    {
        public virtual long Id { get; set; }

        public virtual long ShippingOrderId { get; set; }

        [ForeignKey(nameof(ShippingOrderId))]
        public virtual ShippingOrder ShippingOrder { get; set; }


        public virtual long ContainerId { get; set; }
        [ForeignKey(nameof(ContainerId))]
        public virtual Container Container { get; set; }

        public virtual string AssignorId {get;set;}
        [ForeignKey(nameof(AssignorId))]
        public virtual ApplicationUser Assignor { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime AssignedDateTime { get; set; }
    }
}