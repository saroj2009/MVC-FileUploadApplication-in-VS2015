using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using FileUpload_MVC.Models;

namespace FileUpload_MVC.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpGet]
        public ActionResult UploadFile()
        {
            List<FileModel> list = new List<FileModel>();
            return View(list);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            List<FileModel> list = new List<FileModel>();
            try
            {
                //if (file.ContentLength > 0)
                //{
                //    string _FileName = Path.GetFileName(file.FileName);
                //    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                //    file.SaveAs(_path);
                //}
                //ViewBag.Message = "File Uploaded Successfully!!";
                //return View();

                if (file != null)
                {
                    string path = Server.MapPath("~/UploadedFiles/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string randomNumber = randomNum();
                   
                    //Append ID to FileName
                    string filename = randomNumber + "_" + System.IO.Path.GetFileName(file.FileName);

                    file.SaveAs(path + filename);

                    //file.SaveAs(path + Path.GetFileName(file.FileName));
                    ViewBag.Message = "File uploaded successfully.";
                   
                }
                
                string partialName = "";
                string _fileName = System.IO.Path.GetFileName(file.FileName);
                if (_fileName != null)
                {
                    string[] arrF = _fileName.Split('.');
                    int arrlength = arrF.Length;
                    partialName = arrF[arrlength-2];
                }
               
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
                List<FileInfo> files = dirInfo.GetFiles("*" + partialName + "*.*").ToList();
              
                foreach (FileInfo item in files)
                {
                    FileModel filedetaisl = new FileModel();
                    filedetaisl.strFileName = item.Name;
                    string[] atrA= item.Name.Split('_');
                    filedetaisl.strFileCreateDate = Convert.ToString(atrA[0]);
                    list.Add(filedetaisl);
                }

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "File upload failed!!";
                return View(list);
            }
           
        }
        [NonAction]
        public string randomNum()
        {
            string strRandomNum = Convert.ToString(DateTime.Now);
            strRandomNum = strRandomNum.Replace(@"/", "");
            strRandomNum = strRandomNum.Replace(@":", "");
            strRandomNum = strRandomNum.Replace(@" ", "");

            return strRandomNum;
        }
    }
}