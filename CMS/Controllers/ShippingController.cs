using CMS.Database;
using CMS.Database.Constants;
using CMS.Database.Entities;
using CMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class ShippingController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("MyShippings");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Management");
            }

            return RedirectToAction("MyShippings");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            return View(new ShippingInfoViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(ShippingInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Succeeded = false });
            }

            string trackingId = Guid.NewGuid().ToString();

            this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().Add(new ShippingOrder
            {
                DestionationAddress = new ShippingDestinationAddress
                {
                    AddressLine1 = model.ShippingAddressLine1,
                    AddressLine2 = model.ShippingAddressLine2,
                    City = model.ShippingCity,
                    Country = model.ShippingCountry,
                    PostalCode = model.ShippingPostalCode,
                    State = model.ShippingState
                },
                BillingAddress = new ShippingBillingAddress
                {
                    AddressLine1 = model.BillingAddressLine1,
                    AddressLine2 = model.BillingAddressLine2,
                    City = model.BillingCity,
                    Country = model.BillingCountry,
                    PostalCode = model.BillingPostalCode,
                    State = model.BillingState
                },
                CreationDate = DateTime.Today,
                Items = model.Items.Select(x => new ShippingItem
                {
                    Category = x.Category,
                    Name = x.Name,
                    EstimateWeightage = x.EstimatedWeightage,
                    Amount = x.Amount
                }).ToList(),
                TrackingId = trackingId,
                ShipmentOwnerId = this.User.Identity.GetUserId(),
                Status = ShippingStatus.OrderReceived,
            });

            await this.HttpContext.GetOwinContext().Get<CMSDbContext>().SaveChangesAsync();

            return Json(new { Succeeded = true, TrackingId = trackingId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Management()
        {
            IList<ShippingOrder> shippingOrders = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().ToListAsync();
            return View(shippingOrders.Count() > 0 ? shippingOrders.OrderByDescending(x => x.CreationDate).Select(x => new ShippingInfoViewModel { Id = x.Id, TrackingId = x.TrackingId, ShippingStatus = x.Status.ToString(), OrderOwner = x.ShipmentOwner.UserName, OrderDateTime = x.CreationDate }).ToList() : null);
        }

        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> MyShippings()
        {
            string userId = this.User.Identity.GetUserId();
            IList<ShippingOrder> shippingOrders = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().Where(x => x.ShipmentOwnerId == userId).ToListAsync();
            return View(shippingOrders.Count() > 0 ? shippingOrders.OrderByDescending(x => x.CreationDate).Select(x => new ShippingInfoViewModel
            {
                Id = x.Id,
                TrackingId = x.TrackingId,
                ShippingStatus = x.Status.ToString(),
                OrderDateTime = x.CreationDate,
                Status = x.Status.ToString()
            }).ToList() : null);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Assignment(string shippingTrackingNo)
        {
            ShippingOrder shippingOrder = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().FirstOrDefaultAsync(x => x.TrackingId == shippingTrackingNo);

            if (shippingOrder == null)
            {
                return new HttpStatusCodeResult(400);
            }

            ShippingAssignment shippingAssignment = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingAssignment>().FirstOrDefaultAsync(x => x.ShippingOrderId == shippingOrder.Id);
            IList<Container> containers = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<Container>().ToListAsync();

            if (shippingAssignment == null)
            {
                return View(new ShippingAssignmentViewModel
                {
                    OrderTrackingId = shippingOrder.TrackingId,
                    ShippingOrderId = shippingOrder.Id,
                    CustomerName = shippingOrder.ShipmentOwner.UserName,
                    Containers = containers,
                    Items = shippingOrder.Items.ToList()
                });
            }

            return View(new ShippingAssignmentViewModel
            {
                OrderTrackingId = shippingAssignment.ShippingOrder.TrackingId,
                ShippingOrderId = shippingAssignment.ShippingOrderId,
                ContainerId = shippingAssignment.ContainerId,
                ActualWeight = shippingAssignment.ShippingOrder.ActualTonnage,
                Id = shippingAssignment.Id,
                CustomerName = shippingAssignment.ShippingOrder.ShipmentOwner.UserName,
                Containers = containers,
                Items = shippingAssignment.ShippingOrder.Items.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Assignment(ShippingAssignmentViewModel model)
        {
            model.Containers = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<Container>().ToListAsync();
            ShippingOrder shippingOrder = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().FirstOrDefaultAsync(x => x.TrackingId == model.OrderTrackingId.ToString());
            model.Items = shippingOrder.Items.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ShippingAssignment shippingAssignment = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingAssignment>().FirstOrDefaultAsync(x => x.ShippingOrderId == model.ShippingOrderId);

                if (shippingAssignment == null)
                {
                    shippingAssignment = new ShippingAssignment();

                    this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingAssignment>().Add(new ShippingAssignment
                    {
                        ContainerId = model.ContainerId,
                        ShippingOrderId = model.ShippingOrderId,
                    });

                    if (shippingOrder != null)
                    {
                        shippingOrder.ActualTonnage = model.ActualWeight;
                    }
                }
                else
                {
                    shippingAssignment.ContainerId = model.ContainerId;
                    shippingAssignment.ShippingOrder.ActualTonnage = model.ActualWeight;
                }

                shippingAssignment.AssignorId = this.User.Identity.GetUserId();
                shippingAssignment.AssignedDateTime = DateTime.Now;

                await this.HttpContext.GetOwinContext().Get<CMSDbContext>().SaveChangesAsync();

                model.Message = "Shipping assignment has been saved.";
            }
            catch (Exception ex)
            {
                model.Message = "Error occured. Failed to save shipping assignment.";
            }
            return View(model);
        }

        public async Task<ActionResult> StatusTracking(string shippingTrackingNo)
        {
            ShippingOrder shippingOrder = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().Where(x => x.TrackingId == shippingTrackingNo).FirstOrDefaultAsync();

            if(shippingOrder == null || (this.User.IsInRole("Customer") && shippingOrder.ShipmentOwnerId != this.User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(404);
            }

            return View(new ShippingStatusViewModel { ShippingOrderId = shippingOrder.Id, ShippingStatus = shippingOrder.Status });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> StatusTracking(ShippingStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ShippingOrder shippingOrder = await this.HttpContext.GetOwinContext().Get<CMSDbContext>().Set<ShippingOrder>().Where(x => x.Id == model.ShippingOrderId).FirstOrDefaultAsync();

            if (shippingOrder == null)
            {
                return new HttpStatusCodeResult(404);
            }

            shippingOrder.Status = model.ShippingStatus;

            await this.HttpContext.GetOwinContext().Get<CMSDbContext>().SaveChangesAsync();

            return View(model);
        }
    }
}