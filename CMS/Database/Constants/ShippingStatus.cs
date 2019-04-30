using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Database.Constants
{
    public enum ShippingStatus
    {
        Cancelled = 0,
        OrderReceived = 1,
        OTWToSource = 2,
        ArriveSource = 3,
        OTWSourceHarbour = 4,
        ArriveSourceHarbour = 5,
        OTWDestinationHarbour = 6,
        ArriveDestinationHarbour = 7,
        OTWDestination = 8,
        ArriveDestionation = 9,
        Completed = 10
    }
}