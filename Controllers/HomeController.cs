using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using TestAPI.Models;
using static TestAPI.Models.BLPage;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        [Route ("GetFile")]
        [HttpGet]
        public IActionResult CSV_FileFromDatatable()
        {
            try
            {
                string strk = BLPage.GetAndCreateCSV();
                return Ok("CSV data Created successfully.");
            }
            catch (Exception Ex)
            {
                return BadRequest("Invalid form data.");
            }
         
        }

        [Route("Submit")]
        [HttpPost]
        public  IActionResult CSV_FileToDatatable([FromForm] FormDataModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strk = BLPage.insertCSVtoDatatable(model);
                   
                   

                }
                return Ok("Form data submitted successfully.");
            }
            catch (Exception Ex)
            {
                return BadRequest("Invalid form data.");
            }
        }
    }
}
