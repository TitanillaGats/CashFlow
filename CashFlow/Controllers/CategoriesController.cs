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
    public class CategoriesController : ApiController
    {
        private CashFlowContext db = new CashFlowContext();

        // GET: api/Categories
        public IList<CategoryDetailsDto> GetCategories()
        {

            List<PurchaseItemDetailsDto> tmp = new List<PurchaseItemDetailsDto>();
            return db.Categories.Select(p => new CategoryDetailsDto
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
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

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryDetailsDto))]
        public IHttpActionResult GetCategory(int id)
        {
            Category categoryTmp = db.Categories.Find(id);
            CategoryDetailsDto category = new CategoryDetailsDto
            {
                ID = categoryTmp.ID,
                Name = categoryTmp.Name,
                Description = categoryTmp.Description,
                PurchaseItems = categoryTmp.PurchaseItems.Select(x => new PurchaseItemDetailsDto
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            };
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, CategoryDetailsDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.ID)
            {
                return BadRequest();
            }

            Category currentCategory = new Category
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                PurchaseItems = category.PurchaseItems.Select(x => new PurchaseItem
                {
                    ID = x.ID,
                    Amount = x.Amount,
                    Date = x.Date,
                    Comment = x.Comment,
                    CategoryID = x.CategoryID,
                    PurchaseLogID = x.PurchaseLogID
                }).ToList()
            };
            db.Entry(currentCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(CategoryDetailsDto))]
        public IHttpActionResult PostCategory(CategoryDetailsDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(new Category
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                PurchaseItems = category.PurchaseItems.Select(x => new PurchaseItem
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

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(CategoryDetailsDto))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            CategoryDetailsDto response = new CategoryDetailsDto
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                PurchaseItems = category.PurchaseItems.Select(x => new PurchaseItemDetailsDto
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

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.ID == id) > 0;
        }
    }
}