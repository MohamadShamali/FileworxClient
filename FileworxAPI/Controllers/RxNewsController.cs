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
    public class RxNewsController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> ReceiveNews([FromBody] string txtFileContant)
        {
            try
            {
                string[] content = txtFileContant.Split(new string[] { EditBeforRun.Separator }, StringSplitOptions.None);

                // News
                if ((content[0] == $"{(int)Type.News}") && (content.Count() >= 6))
                {
                    string format = "M/d/yyyy h:mm:ss tt";
                    clsNews news = new clsNews()
                    {
                        Id = Guid.NewGuid(),
                        Description = content[1],
                        CreationDate = DateTime.ParseExact(content[2], format, System.Globalization.CultureInfo.InvariantCulture),
                        CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"),
                        CreatorName = "admin",
                        Name = content[3],
                        Body = content[4],
                        Category = content[5]
                    };

                    await news.InsertAsync();

                    return Ok("Success");
                }
                else
                {
                    return BadRequest("Not the specified format");
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

