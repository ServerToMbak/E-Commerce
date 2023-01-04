using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public  BuggyController(StoreContext context)
        {
            _context = context;

        } 


       [HttpGet("notfound")]
       public ActionResult GetNotFoundRequest()
       {
            var thing = _context.Products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
       }
       [HttpGet("servererror")]
       public ActionResult GetServerError()
       {
         var thing = _context.Products.Find(42);

         var thingToReturn = thing.ToString();

            return Ok(new ApiResponse(500));
       }
       [HttpGet("badRequest")]
       public ActionResult GetBadRequest()
       {
            return BadRequest(new ApiResponse(400));
       }
       [HttpGet("badRequest/{id}")]
       public ActionResult GetNotFoundRequest(int id)
       {
            return Ok();
       }
       [HttpGet("testauth")]
       [Authorize]
       public ActionResult<string> GetSecretText()
       {
            return "Secret Stuff";
       }
    }
}