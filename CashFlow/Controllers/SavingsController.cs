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
    public class SavingsController : ApiController
    {
        private CashFlowContext db = new CashFlowContext();

        // GET: api/Savings
        public IList<SavingDetailsDto> GetSavings()
        {
            return db.Savings.Select(p => new SavingDetailsDto
            {
                ID = p.ID,
                Balance = p.Balance,
                GoalBalance = p.GoalBalance,
                Deadline = p.Deadline,
                Description = p.Description,
                PurchaseLogID = p.PurchaseLogID
            }).ToList();
        }

        // GET: api/Savings/5
        [ResponseType(typeof(SavingDetailsDto))]
        public IHttpActionResult GetSaving(int id)
        {
            Saving savingTmp = db.Savings.Find(id);
            SavingDetailsDto saving = new SavingDetailsDto
            {
                ID = savingTmp.ID,
                Balance = savingTmp.Balance,
                GoalBalance = savingTmp.GoalBalance,
                Deadline = savingTmp.Deadline,
                Description = savingTmp.Description,
                PurchaseLogID = savingTmp.PurchaseLogID
            };
            if (saving == null)
            {
                return NotFound();
            }

            return Ok(saving);
        }

        // PUT: api/Savings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSaving(int id, SavingDetailsDto saving)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saving.ID)
            {
                return BadRequest();
            }
            
            Saving currentSaving = new Saving
            {
                ID = saving.ID,
                Balance = saving.Balance,
                GoalBalance = saving.GoalBalance,
                Deadline = saving.Deadline,
                Description = saving.Description,
                PurchaseLogID = saving.PurchaseLogID
            };
            db.Entry(currentSaving).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavingExists(id))
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

        // POST: api/Savings
        [ResponseType(typeof(SavingDetailsDto))]
        public IHttpActionResult PostSaving(SavingDetailsDto saving)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Savings.Add(new Saving
            {
                ID = saving.ID,
                Balance = saving.Balance,
                GoalBalance = saving.GoalBalance,
                Deadline = saving.Deadline,
                Description = saving.Description,
                PurchaseLogID = saving.PurchaseLogID
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = saving.ID }, saving);
        }

        // DELETE: api/Savings/5
        [ResponseType(typeof(SavingDetailsDto))]
        public IHttpActionResult DeleteSaving(int id)
        {
            Saving saving = db.Savings.Find(id);
            if (saving == null)
            {
                return NotFound();
            }

            db.Savings.Remove(saving);
            db.SaveChanges();
            SavingDetailsDto response = new SavingDetailsDto
            {
                ID = saving.ID,
                Balance = saving.Balance,
                GoalBalance = saving.GoalBalance,
                Deadline = saving.Deadline,
                Description = saving.Description,
                PurchaseLogID = saving.PurchaseLogID
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

        private bool SavingExists(int id)
        {
            return db.Savings.Count(e => e.ID == id) > 0;
        }
    }
}