using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileworxObjectClassLibrary; 
using Type = FileworxObjectClassLibrary.Type;
using Newtonsoft.Json;

namespace FileworxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxFilesController : ControllerBase
    {

        [HttpGet("{fileId}")]
        public IActionResult TransmitFile(Guid fileId)
        {
            try
            {
                // Use _fileworxLibrary to read the file with the specific ID
                // Replace 'ReadAsync' with the appropriate method from your library
                clsFile file = new clsFile() { Id= fileId};
                file.Read();
                if (!String.IsNullOrEmpty(file.Body))
                {
                    if(file.Class == Type.News)
                    {
                        clsNews news = new clsNews() { Id = fileId };
                        news.Read();

                        // Serialize the clsFile object to JSON
                        string json = JsonConvert.SerializeObject(news);

                        // Return the clsFile object as JSON in the response
                        return Ok(json);
                    }

                    else
                    {
                        clsPhoto photo = new clsPhoto() { Id = fileId };
                        photo.Read();

                        // Serialize the clsFile object to JSON
                        string json = JsonConvert.SerializeObject(photo);

                        // Return the clsFile object as JSON in the response
                        return Ok(photo);
                    }

                }
                else
                {
                    // Handle the case where the file with the specified ID was not found
                    return NotFound($"File with ID {fileId} not found.");
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
