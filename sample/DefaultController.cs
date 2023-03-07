using Microsoft.AspNetCore.Mvc;

namespace DoIt.ExcelWriter.AspNetCore.Sample;

record ExcelRow(int Id, string FirstName, string LastName);

[Route("/")]
public class DefaultController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return this.ExcelContent(async writer =>
        {
            await using (var sheet = await writer.AddSheetAsync<ExcelRow>("Sheet1", HttpContext.RequestAborted))
            {
                await sheet.WriteAsync(new ExcelRow(1, "David", "Nordvall"));
                await sheet.WriteAsync(new ExcelRow(2, "Navid", "Dordvall"));
            }
        }, "Sample file.xlsx");
    }
}
