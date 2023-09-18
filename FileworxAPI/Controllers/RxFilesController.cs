using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileworxObjectClassLibrary;
using Type = FileworxObjectClassLibrary.Type;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileworxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RxFilesController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> ReceiveFile([FromBody] clsFile news)
        {
            try
            {
                clsNews newss = (clsNews) news;
                // Parse the JSON
                var jsonObject = new JObject(); /*JObject.Parse(jsonFileData);*/

                // Check the "class" property
                if (jsonObject["class"] != null)
                {
                    int classValue = jsonObject.Value<int>("class");

                    if (classValue == 2)
                    {
                        //clsNews news = JsonConvert.DeserializeObject<clsNews>(jsonFileData);

                        await news.InsertAsync();
                    }
                    else
                    {
                        //
                    }
                    return Ok("File received and inserted successfully.");
                }
                else 
                {
                    return BadRequest("No class field in the json");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file receiving and insertion
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
