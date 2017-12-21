using ebayforcars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ebayforcars.Controllers
{
    public class DynamicImagesController : Controller
    {
        private EbayforCarsEntities db = new EbayforCarsEntities();
        // GET: DynamicImages
        public ActionResult Index(int id)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            //Response.ContentType = image.ContentType;
           return new FileContentResult(image.Content, image.ContentType);
           
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