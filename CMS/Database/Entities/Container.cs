using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Database.Entities
{
    public class Container
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal MaxTonnage { get; set; }
        public virtual string CurrentLocation { get; set; }
    }
}