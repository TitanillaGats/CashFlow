using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CashFlow.Models;

namespace CashFlow.Controllers
{
    public class PurchaseItemsController : ApiController
    {
        private CashFlowContext db = new CashFlowContext();

        // GET: api/PurchaseItems
        public IList<PurchaseItemDetailsDto> GetPurchaseItems()
        {
            return db.PurchaseItems.Select(p => new PurchaseItemDetailsDto
            {
                ID = p.ID,
                Amount = p.Amount,
                Date = p.Date,
                Comment = p.Comment,
                CategoryID = p.CategoryID,
                PurchaseLogID = p.PurchaseLogID
            }).ToList();
        }

        // GET: api/PurchaseItems/5
        [ResponseType(typeof(PurchaseItemDetailsDto))]
        public IHttpActionResult GetPurchaseItem(int id)
        {
            PurchaseItem purchaseItemTmp = db.PurchaseItems.Find(id);
            PurchaseItemDetailsDto purchaseItem = new PurchaseItemDetailsDto
            {
                ID = purchaseItemTmp.ID,
                Amount = purchaseItemTmp.Amount,
                Date = purchaseItemTmp.Date,
                Comment = purchaseItemTmp.Comment,
                CategoryID = purchaseItemTmp.CategoryID,
                PurchaseLogID = purchaseItemTmp.PurchaseLogID
            };
            if (purchaseItem == null)
            {
                return NotFound();
            }

            return Ok(purchaseItem);
        }

        // PUT: api/PurchaseItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseItem(int id, PurchaseItemDetailsDto purchaseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseItem.ID)
            {
                return BadRequest();
            }

            PurchaseItem currentPurchaseItem = new PurchaseItem
            {
                ID = purchaseItem.ID,
                Amount = purchaseItem.Amount,
                Date = purchaseItem.Date,
                Comment = purchaseItem.Comment,
                CategoryID = purchaseItem.CategoryID,
                PurchaseLogID = purchaseItem.PurchaseLogID
            };
            db.Entry(currentPurchaseItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseItems
        [ResponseType(typeof(PurchaseItemDetailsDto))]
        public IHttpActionResult PostPurchaseItem(PurchaseItemDetailsDto purchaseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseItems.Add(new PurchaseItem
            {
                ID = purchaseItem.ID,
                Amount = purchaseItem.Amount,
                Date = purchaseItem.Date,
                Comment = purchaseItem.Comment,
                CategoryID = purchaseItem.CategoryID,
                PurchaseLogID = purchaseItem.PurchaseLogID
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseItem.ID }, purchaseItem);
        }

        // DELETE: api/PurchaseItems/5
        [ResponseType(typeof(PurchaseItemDetailsDto))]
        public IHttpActionResult DeletePurchaseItem(int id)
        {
            PurchaseItem purchaseItem = db.PurchaseItems.Find(id);
            if (purchaseItem == null)
            {
                return NotFound();
            }

            db.PurchaseItems.Remove(purchaseItem);
            db.SaveChanges();

            PurchaseItemDetailsDto response = new PurchaseItemDetailsDto
            {
                ID = purchaseItem.ID,
                Amount = purchaseItem.Amount,
                Date = purchaseItem.Date,
                Comment = purchaseItem.Comment,
                CategoryID = purchaseItem.CategoryID,
                PurchaseLogID = purchaseItem.PurchaseLogID
            };
            return Ok(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseItemExists(int id)
        {
            return db.PurchaseItems.Count(e => e.ID == id) > 0;
        }
    }
}