using EmotionPlatzi.Models;
using EmotionPlatzi.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFolderPath;
        EmotionHelper emoHelper;
        String key;
        EmotionPlatziContext db = new EmotionPlatziContext();
        public EmoUploaderController()
        {
           
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }
        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            if (file?.ContentLength > 0) {

                var pictureName = Guid.NewGuid().ToString();
                pictureName+=Path.GetExtension(file.FileName);

                var route = Server.MapPath(serverFolderPath);
                route = route + "\\" + pictureName;

                file.SaveAs(route);
             var emoPicture=  await emoHelper.DetectAndExtracfacesAsync(file.InputStream);

                emoPicture.Name = file.FileName;
                emoPicture.Path = $"{serverFolderPath}/{pictureName}";
                db.Emopictures.Add(emoPicture);
              await  db.SaveChangesAsync();

                return RedirectToAction("Details","Emopictures",new { Id=emoPicture.Id});
            }
            return View();
        }
    }
}