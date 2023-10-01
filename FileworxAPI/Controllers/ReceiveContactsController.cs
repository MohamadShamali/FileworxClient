using FileworxObjectClassLibrary.Models;
using FileworxObjectClassLibrary.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileworxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveContactsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetReceiveContacts()
        {
            try
            {
                clsContactQuery query = new clsContactQuery();
                query.QDirection = new ContactDirection[] { ContactDirection.Receive, (ContactDirection.Transmit | ContactDirection.Receive) };
                var receiveContacts = await query.RunAsync();

                return Ok(receiveContacts);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file retrieval
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
