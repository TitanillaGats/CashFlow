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
    public class PurchaseLogsController : ApiController
    {
        private CashFlowContext db = new CashFlowContext();

        // GET: api/PurchaseLogs
        public IList<PurchaseLogDetailsDto> GetPurchaseLogs()
        {
            return db.PurchaseLogs.Select(p => new PurchaseLogDetailsDto
            {
                ID = p.ID,
                Name = p.Name,
                Budgets = p.Budgets.Select(x => new BudgetDetailsDto
                {
                    ID = x.ID,
                    Balance= x.Balance,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                Savings = p.Savings.Select(x => new SavingDetailsDto
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    GoalBalance = x.GoalBalance,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                PurchaseItems = p.PurchaseItems.Select(x => new PurchaseItemDetailsDto
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            }).ToList();
        }

        // GET: api/PurchaseLogs/5
        [ResponseType(typeof(PurchaseLogDetailsDto))]
        public IHttpActionResult GetPurchaseLog(int id)
        {
            PurchaseLog purchaseLogTmp = db.PurchaseLogs.Find(id);
            PurchaseLogDetailsDto purchaseLog = new PurchaseLogDetailsDto
            {
                ID = purchaseLogTmp.ID,
                Name = purchaseLogTmp.Name,
                Budgets = purchaseLogTmp.Budgets.Select(x => new BudgetDetailsDto
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                Savings = purchaseLogTmp.Savings.Select(x => new SavingDetailsDto
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    GoalBalance = x.GoalBalance,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                PurchaseItems = purchaseLogTmp.PurchaseItems.Select(x => new PurchaseItemDetailsDto
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            };
            if (purchaseLog == null)
            {
                return NotFound();
            }

            return Ok(purchaseLog);
        }

        // PUT: api/PurchaseLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseLog(int id, PurchaseLogDetailsDto purchaseLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseLog.ID)
            {
                return BadRequest();
            }

            PurchaseLog currentPurchaseLog = new PurchaseLog
            {
                ID = purchaseLog.ID,
                Name = purchaseLog.Name,
                Budgets = purchaseLog.Budgets.Select(x => new Budget
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                Savings = purchaseLog.Savings.Select(x => new Saving
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    GoalBalance = x.GoalBalance,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                PurchaseItems = purchaseLog.PurchaseItems.Select(x => new PurchaseItem
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            };
            db.Entry(purchaseLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseLogExists(id))
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

        // POST: api/PurchaseLogs
        [ResponseType(typeof(PurchaseLogDetailsDto))]
        public IHttpActionResult PostPurchaseLog(PurchaseLogDetailsDto purchaseLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseLogs.Add(new PurchaseLog
            {
                ID = purchaseLog.ID,
                Name = purchaseLog.Name,
                Budgets = purchaseLog.Budgets.Select(x => new Budget
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                Savings = purchaseLog.Savings.Select(x => new Saving
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    GoalBalance = x.GoalBalance,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                PurchaseItems = purchaseLog.PurchaseItems.Select(x => new PurchaseItem
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseLog.ID }, purchaseLog);
        }

        // DELETE: api/PurchaseLogs/5
        [ResponseType(typeof(PurchaseLogDetailsDto))]
        public IHttpActionResult DeletePurchaseLog(int id)
        {
            PurchaseLog purchaseLog = db.PurchaseLogs.Find(id);
            if (purchaseLog == null)
            {
                return NotFound();
            }

            db.PurchaseLogs.Remove(purchaseLog);
            db.SaveChanges();

            PurchaseLogDetailsDto response = new PurchaseLogDetailsDto
            {
                ID = purchaseLog.ID,
                Name = purchaseLog.Name,
                Budgets = purchaseLog.Budgets.Select(x => new BudgetDetailsDto
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                Savings = purchaseLog.Savings.Select(x => new SavingDetailsDto
                {
                    ID = x.ID,
                    Balance = x.Balance,
                    GoalBalance = x.GoalBalance,
                    Deadline = x.Deadline,
                    Description = x.Description,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList(),
                PurchaseItems = purchaseLog.PurchaseItems.Select(x => new PurchaseItemDetailsDto
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
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

        private bool PurchaseLogExists(int id)
        {
            return db.PurchaseLogs.Count(e => e.ID == id) > 0;
        }
    }
}