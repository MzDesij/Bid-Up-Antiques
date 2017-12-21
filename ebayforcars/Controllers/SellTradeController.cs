using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ebayforcars.Controllers
{
    public class SellTradeController : Controller
    {
        // GET: SellTrade
        public ActionResult Index()
        {
            return View();
        }

        // GET: SellTrade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellTrade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellTrade/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SellTrade/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellTrade/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SellTrade/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellTrade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
