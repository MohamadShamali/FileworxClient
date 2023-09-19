using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileworxObjectClassLibrary;
using Type = FileworxObjectClassLibrary.Type;
using Newtonsoft.Json;

namespace FileworxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxPhotoController : ControllerBase
    {

        [HttpGet("{contactId}, {photoId}")]
        public async Task<IActionResult> TransmitFile(Guid contactId, Guid photoId)
        {
            try
            {
                if (contactId != Guid.Empty && photoId != Guid.Empty)
                {
                    clsPhoto photo = new clsPhoto() { Id = photoId };
                    await photo.ReadAsync();

                    clsContact contact = new clsContact() { Id = contactId };
                    await contact.ReadAsync();

                    return Ok(contact.GetTxtFileContent(photo, photo.Id));
                }

                else
                {
                    // Handle the case where the file with the specified ID was not found
                    return NotFound($"File with ID {photoId} not found.");
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
