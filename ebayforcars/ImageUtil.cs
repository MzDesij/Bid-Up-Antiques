using ebayforcars.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ebayforcars
{
    public class ImageUtil
    {
        public static Image Uploadimages(EbayforCarsEntities DB, HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }
            if(!file.ContentType.StartsWith("image/"))
            {
                return null;
            }
            Image Pics = new Image();
            Pics.ContentType = file.ContentType;
            int extpos = file.FileName.LastIndexOf('.');
            Pics.Extension = file.FileName.Substring(extpos);
            Pics.Content = new byte[file.ContentLength];
            file.InputStream.Read(Pics.Content, 0, file.ContentLength);
            var result = DB.Images.Add(Pics);
            DB.SaveChanges();
            return result;


            
        }

    }
}