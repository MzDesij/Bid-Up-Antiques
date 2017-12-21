using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ebayforcars.Models;
using Microsoft.AspNet.Identity;

namespace ebayforcars.Controllers
{
    [Authorize]
    public class BidsController : Controller
    {
        private EbayforCarsEntities db = new EbayforCarsEntities();

        // GET: Bids
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.Product).Include(b => b.User);
            return View(bids.ToList());
        }

        // GET: Bids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }





        // GET: Bids/Create
        public ActionResult Create()
        {
            ViewBag.product_ID = new SelectList(db.Products, "Product_ID", "Auction1");
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "LastName");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_ID,Amount")] Bid bid)
        {

            if (ModelState.IsValid)
            {
                Product prod = db.Products.Find(bid.product_ID);
                var userid = User.Identity.GetUserId();
                var user = db.Users.Find(userid);
                bid.User_ID = userid;
                bid.Time = DateTime.Now;
                db.Bids.Add(bid);
                db.SaveChanges();
                EmailUtility.sendMail(user.EmailAddress, "This is to confirm your bid on " + prod.productName + " for the amount of " + bid.Amount);
                ViewBag.confirmMsg = "A confirmation email for your bid has been sent to you.";

                return View("EmailConfirmation");




                return RedirectToAction("Index");
            }

            ViewBag.product_ID = new SelectList(db.Products, "product_ID", "Auction1", bid.product_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "LastName", bid.User_ID);
            return View(bid);
        }

        // GET: Bids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_ID = new SelectList(db.Products, "product_ID", "Auction1", bid.product_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "LastName", bid.User_ID);
            return View(bid);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy([Bind(Include = "product_ID")] Product product)
        {
            //Get the hidden productId to customise the email to the user. 

            //For bids - get the productId and the Bid amount for the email.
            Product prod = db.Products.Find(product.Product_ID);
            var userid = User.Identity.GetUserId();
            var user = db.Users.Find(userid);


            EmailUtility.sendMail(user.EmailAddress, "This is to confirm your purchase of "+prod.productName+" for the amount of "+prod.price);
            ViewBag.confirmMsg = "A confirmation email for your purchase has been sent to you.";

            return View("EmailConfirmation");
        }


        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_ID,User_ID,Bid_ID,Amount,Time,Bid1")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_ID = new SelectList(db.Products, "product_ID", "Auction1", bid.product_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "LastName", bid.User_ID);
            return View(bid);
        }

        // GET: Bids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bid bid = db.Bids.Find(id);
            db.Bids.Remove(bid);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
