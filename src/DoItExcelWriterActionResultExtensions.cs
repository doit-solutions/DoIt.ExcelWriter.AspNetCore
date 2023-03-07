using System.Web;
using DoIt.ExcelWriter;

namespace Microsoft.AspNetCore.Mvc;

class ExcelContentActionResult : IActionResult
{
    private const string XlsxMimeType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";

    private readonly Func<IExcelWriter, Task> _writeAction;
    private readonly string? _fileName;

    public ExcelContentActionResult(Func<IExcelWriter, Task> writerAction, string? fileName)
    {
        _writeAction = writerAction;
        _fileName = fileName;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.Headers.ContentType = XlsxMimeType;
        if (!string.IsNullOrWhiteSpace(_fileName))
        {
            context.HttpContext.Response.Headers.ContentDisposition = $"attachment; filename=\"{HttpUtility.UrlEncode(_fileName)}\"";
        }

        await using (var writer = ExcelWriterFactory.Create(context.HttpContext.Response.BodyWriter.AsStream()))
        {
            await _writeAction.Invoke(writer);
        }
    }
}

public static class DoItExcelWriterActionResultExtensions
{
    public static IActionResult ExcelContent(this Controller _, Func<IExcelWriter, Task> writerAction, string? fileName = null)
    {
        return new ExcelContentActionResult(writerAction, fileName);
    }
}
