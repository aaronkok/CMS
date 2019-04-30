using CMS.Database.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMS.Database
{
    public class CMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public CMSDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Container> Containers { get; set; }
        public virtual IDbSet<ShipmentStatusLog> ShipmentStatusLogs { get; set; }
        public virtual IDbSet<ShippingAssignment> ShippingAssignments { get; set; }
        public virtual IDbSet<ShippingBillingAddress> ShippingBillingAddresses { get; set; }
        public virtual IDbSet<ShippingDestinationAddress> ShippingDestinationAddresses { get; set; }
        public virtual IDbSet<ShippingOrder> ShippingOrders { get; set; }
        public static CMSDbContext Create()
        {
            return new CMSDbContext();
        }
    }
}