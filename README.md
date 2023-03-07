# DoIt.ExcelWriter.AspNetCore
[![NuGet Badge](https://buildstats.info/nuget/DoIt.ExcelWriter.AspNetCore)](https://www.nuget.org/packages/DoIt.ExcelWriter.AspNetCore/)

Extensions for simplifying usage of the "forward only" Excel writer DoIt.ExcelWriter from ASP.NET Core applications.

# What's this?
If you want to use [DoIt.ExcelWriter](https://github.com/doit-solutions/DoIt.ExcelWriter) to generate Excel files in an ASP.NET Core application, this library contains extension methods for simplifying this.

# Alright, how do I use it?
First of all, you need to add the library to yout ASP.NET Core project

```
dotnet add package DoIt.ExcelWriter.AspNetCore
```

Note that you do not need any package reference to the `DoIt.ExcelWriter` package &ndash; that package is brought in when `DoIt.ExcelWriter.AspNetCore` is references.

Once the package is added to you ASP.NET Core project, a `ExcelContent()` extension method creating an `IActionResult` becomes available for use in your controller actions. This extension will write your generated Excel data to the response body in a memory efficient manner and set the `Content-Type` header to the correct value.

You may also pass a file name which will be sent in the `Content-Disposition` HTTP response header.

```c#
using Microsoft.AspNetCore.Mvc;

record ExcelRow(int Id, string FirstName, string LastName);

public class DefaulController : Controller
{
    [HttpGet]
    public IActionResult GenerateExcel()
    {
        return this.ExcelContent(async writer =>
        {
            await (var sheet = await writer.AddSheetAsync<ExcelRow>("Sheet1", HttpContext.RequestAborted))
            {
                await sheet.WriteAsync(new ExcelRow(1, "Foo", "Bar"), HttpContext.RequestAborted);
            }
        });
    }
}
```

# What else?
That's pretty much it! For more information on how to actually generate Excel files using the `DoIt.ExcelWriter` library, please check out that library's [Github repository](https://github.com/doit-solutions/DoIt.ExcelWriter).