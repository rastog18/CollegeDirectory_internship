using CRUDmvc.DataAccessLayer;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDmvc.Controllers
{
    //[ApiController]
    //[Route("api/[controller]/")]
    //[Route("api/[controller]/[action]")]
    public class CrudOperationController : Controller
    {
        private readonly ICrudOperationDL? _crudOperationDL;
        private readonly ILogger<CrudOperationController> _logger;
        // GET: /<controller>/

        public CrudOperationController(ICrudOperationDL crudOperationDL, ILogger<CrudOperationController> logger)
        {
            _crudOperationDL = crudOperationDL;
            _logger = logger;
        }
        //[HttpPost]
        //[Route("Insert")]
        ////IActionResult makes sure that the reutrn is in the form of OK, BAD RESULT
        //Here InsertRecord is an API which uses an instance of the FIELD class InsertRecordRequest
        [HttpGet]
        public async Task<IActionResult> InsertRecord()
        {
            return View();
        }
        //[Authorize]
        [HttpPost]
        [Route("/en/CrudOperation/InsertRecord")]
        public async Task<IActionResult> InsertRecord([FromBody]InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                response = await _crudOperationDL.InsertRecord(request);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }


        //TODO: What do we call these tags: Attributes allow us to add declerative information, which can be retrieved at run time, in C# there were things like{Obselete("Give some information", true)]
        //Another example would be serelizable, which serilizes  i.e converts into objects which are transferrabe (json, xml)
        //TODO: default http
        //TODO: Views
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserIndex()
        {
            ReadRecordResponse response2 = new ReadRecordResponse();
            try
            {
                response2 = await _crudOperationDL.ReadRecord();
                response2.IsSuccess = true;
                response2.Message = "Data Read!";

            }
            catch (Exception e)
            {
                response2.IsSuccess = false;
                response2.Message = e.Message;
            }
            //return (Json(response2));
            return View("UserIndex", response2); // Pass the model to the view
        }

        public async Task<IActionResult> ReadRecordView()
        {
            return View("ReadRecord");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/en/CrudOperation/ReadRecord")]
        public async Task<IActionResult> ReadRecord()
        {
            ReadRecordResponse response2 = new ReadRecordResponse();
            try
            {
                response2 = await _crudOperationDL.ReadRecord();
                response2.IsSuccess = true;
                response2.Message = "Data Read!";

            }
            catch (Exception e)
            {
                response2.IsSuccess = false;
                response2.Message = e.Message;
            }
            return (Json(response2));
            //return View(response2); // Pass the model to the view
        }
        [HttpGet]
        public async Task<IActionResult> ReadWithJson()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReadWithJson(ReadRecordJsonRequest request)
        {
            Dictionary<string, string> searchPair =
             new Dictionary<string, string>();
            if (request.firstName != "-")
            {
                searchPair.Add("firstName", request.firstName);
            }
            ReadJsonResponse response = new ReadJsonResponse();
            response.data = new List<InsertRecordRequest>();
            try
            {
                response = await _crudOperationDL.ReadWithJson(searchPair, request);
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }

        [HttpGet]
        public async Task <IActionResult> UpdateRecord()
        {
            return View();
        }

        [Authorize (Roles = "User")]
        [Route("/en/CrudOperation/UpdateGPA")]
        public async Task<IActionResult> UpdateGPA([FromBody] UpdateGPARequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            if (request == null)
            {
                response.IsSuccess = false;
                response.Message = "Request is null.";
                return View(response);
            }
            try
            {
                if (request.firstName == null || request.GPA == null)
                {
                    throw new InvalidOperationException("One or more required properties are null.");
                }
                response = await _crudOperationDL.UpdateGPA(request);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }

        [Route("/en/CrudOperation/UpdateRecord")]
        public async Task<IActionResult> UpdateRecord([FromBody]UpdateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            if (request == null)
            {
                response.IsSuccess = false;
                response.Message = "Request is null.";
                return View(response);
            }
            try
            {
                if (request.firstName == null || request.lastName == null || request.age == null || request.major == null)
                {
                    throw new InvalidOperationException("One or more required properties are null.");
                }
                response = await _crudOperationDL.UpdateRecord(request);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }
        //[HttpDelete]
        //[Route("Delete")]
        //[Authorize]
        [HttpPost]
        [Route("/en/CrudOperation/DeleteRecord")]
        public async Task<IActionResult> DeleteRecord([FromBody]DeleteRecordRequest request)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();
            try
            {
                response = await _crudOperationDL.DeleteRecord(request);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRecord()
        {
            return View();
        }

        [HttpGet]
        [Authorize (Roles = "student")]
        public async Task<IActionResult> StudentIndex()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> ViewRecord()
        {
            _logger.LogInformation("Endpoint hit");
            return View("ViewRecord");
        }

        [Authorize(Roles = "student")]
        public async Task<IActionResult> ViewRecord([FromForm] MarksRequest request)
        {
            _logger.LogInformation("POST ViewRecord endpoint hit with Id: {Id}", request.Id);
            ViewMarksResponse response = new ViewMarksResponse();
            try
            {
                response = await _crudOperationDL.ViewMarks(request);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return View(response);
        }
    }
}

