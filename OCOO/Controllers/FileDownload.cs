using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCOO.Models;
using OCOO.Models.FirmMasters;
using System.Data;

namespace OCOO.Controllers
{
    public class FileDownloadController : Controller
    {

        public async Task<IActionResult> Index(string filename)
        {
            try
            {
                string FileFullPath = HttpContext.Session.GetString("FilePath").ToString();
                var path = FileFullPath + filename;
                MemoryStream memory = new MemoryStream();
                FileInfo file = new FileInfo(path);
                if (file.Exists.Equals(true))
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, "image/png", filename);
                }
                path = "wwwroot/assets/images/NoRecord.png";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "image/png", "NoRecordFound.png");
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
