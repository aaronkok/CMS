using CMS.Database.Constants;
using CMS.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class ShippingInfoViewModel
    {
        public long Id { get; set; }
        public string TrackingId { get; set; }
        public string OrderOwner { get; set; }
        public decimal ActualTonnage { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string ShippingAddressLine1 { get; set; }
        [Required]
        [Display(Name = "Address Line 2")]
        public string ShippingAddressLine2 { get; set; }
        [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }
        [Required]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        public string ShippingStatus { get; set; }
        [Required]
        [Display(Name = "Postcode")]
        public string ShippingPostalCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string BillingAddressLine1 { get; set; }
        [Required]
        [Display(Name = "Address Line 2")]
        public string BillingAddressLine2 { get; set; }
        [Required]
        [Display(Name = "City")]
        public string BillingCity { get; set; }
        [Required]
        [Display(Name = "State")]
        public string BillingState { get; set; }
        [Required]
        [Display(Name = "Postcode")]
        public string BillingPostalCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Status { get; set; }
        public IList<ShippingItemViewModel> Items { get; set; }
    }

    public class ShippingItemViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal EstimatedWeightage { get; set; }
    }

    public class ShippingAssignmentViewModel
    {
        public long Id { get; set; }
        public string OrderTrackingId { get; set; }
        public long ShippingOrderId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public decimal ActualWeight { get; set; }
        [Required]
        public long ContainerId { get; set; }
        public string Message { get; set; }
        public IList<Container> Containers { get; set; }
        public IList<ShippingItem> Items { get; set; }
    }

    public class ShippingStatusViewModel
    {
        public long ShippingOrderId { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
    }
}