using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileworxObjectClassLibrary;
using Type = FileworxObjectClassLibrary.Type;
using Newtonsoft.Json;

namespace FileworxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxNewsController : ControllerBase
    {

        [HttpGet("{contactId}, {newsId}")]
        public async Task<IActionResult> TransmitFile(Guid contactId, Guid newsId)
        {
            try
            {
                if(contactId != Guid.Empty && newsId != Guid.Empty) 
                {
                    clsNews news = new clsNews() { Id = newsId };
                    await news.ReadAsync();

                    clsContact contact = new clsContact() { Id = contactId };
                    await contact.ReadAsync();

                    return Ok(contact.GetTxtFileContent(news));
                }

                else
                {
                    // Handle the case where the file with the specified ID was not found
                    return NotFound($"File with ID {newsId} not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file retrieval
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
