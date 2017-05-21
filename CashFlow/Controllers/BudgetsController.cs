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
    public class BudgetsController : ApiController
    {
        private CashFlowContext db = new CashFlowContext();

        // GET: api/Budgets
        public IList<BudgetDetailsDto> GetBudgets()
        {
            return db.Budgets.Select(p => new BudgetDetailsDto
            {
                ID = p.ID,
                Balance = p.Balance,
                PurchaseLogID = p.PurchaseLogID
            }).ToList();
        }

        // GET: api/Budgets/5
        [ResponseType(typeof(BudgetDetailsDto))]
        public IHttpActionResult GetBudget(int id)
        {
            Budget budgetTmp = db.Budgets.Find(id);
            BudgetDetailsDto budget = new BudgetDetailsDto
            {
                ID = budgetTmp.ID,
                Balance = budgetTmp.Balance,
                PurchaseLogID = budgetTmp.PurchaseLogID
            };
            if (budget == null)
            {
                return NotFound();
            }

            return Ok(budget);
        }

        // PUT: api/Budgets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBudget(int id, [FromBody]BudgetDetailsDto budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!BudgetExists(id))
            {
                return BadRequest();
            }

            Budget currentBudget = db.Budgets.Find(id);
            if (id != currentBudget.ID)
            {
                return BadRequest();
            }

            db.Entry(currentBudget).State = EntityState.Modified;
            currentBudget = new Budget
            {
                ID = budget.ID,
                Balance = budget.Balance,
                PurchaseLogID = budget.PurchaseLogID
            };
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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

        // POST: api/Budgets
        [ResponseType(typeof(BudgetDetailsDto))]
        public IHttpActionResult PostBudget(BudgetDetailsDto budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Budgets.Add(new Budget
            {
                ID = budget.ID,
                Balance = budget.Balance,
                PurchaseLogID = budget.PurchaseLogID
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = budget.ID }, budget);
        }

        // DELETE: api/Budgets/5
        [ResponseType(typeof(BudgetDetailsDto))]
        public IHttpActionResult DeleteBudget(int id)
        {
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }

            db.Budgets.Remove(budget);
            db.SaveChanges();

            BudgetDetailsDto response = new BudgetDetailsDto
            {
                ID = budget.ID,
                Balance = budget.Balance,
                PurchaseLogID = budget.PurchaseLogID
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

        private bool BudgetExists(int id)
        {
            return db.Budgets.Count(e => e.ID == id) > 0;
        }
    }
}