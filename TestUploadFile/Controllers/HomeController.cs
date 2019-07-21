using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using GemBox.Document;
using System.Text;
using System.Web.Mvc;

namespace TestUploadFile.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult UploadFile()
        {
            //var path = Server.MapPath("~/Files");
            //var dir = new DirectoryInfo(path);
            //var files = dir.EnumerateFiles().Select(c => c.Name);
            //return View(files);
            return View();
        }

      
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            string path = Server.MapPath("~/App_Data/File");
            string fileName = Path.GetFileName(file.FileName);
            string fullPath = Path.Combine(path, fileName);
            file.SaveAs(fullPath);
            string ext = Path.GetExtension(file.FileName);

            // can read docx & doc & pdf .. etc 
            //  using GemBox.Document; downloded in nuget 
            //https://www.gemboxsoftware.com/document/examples/c-sharp-vb-net-read-word-file/301 


            if (ext.Equals(".pdf"))
            {

                var document = DocumentModel.Load(fullPath);
                string txt = document.Content.ToString();

                string text = "";
                //string text =   System.IO.File.ReadAllText(fullPath);
                for (int i = 0; i < file.ContentLength; i++)
                {
                    //HttpPostedFileBase f = Request.Files[i];
                    using (StreamReader sr = new StreamReader(file.InputStream ))
                    {
                        string fileText = sr.ReadToEnd();

                        text = fileText;
                        //do what you want with the file-text...
                    }
                }
               ViewBag.fileName = txt;
            }
            //var data = new byte[file.ContentLength];
            //file.InputStream.Read(data, 0, file.ContentLength);
            //using (var sw = new FileStream(path, FileMode.Create))
            //{
            //    sw.Write(data, 0, data.Length);  
            //}
            //RedirectToAction("UploadFile");
            return View();
        }
        public ActionResult Index()
        {
           
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}