using DoIt.ExcelWriter.AspNetCore.Sample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
// app.UseRouting();
// app.UseEndpoints(routes =>
// {
//     routes.MapFallbackToController(nameof(DefaultController.Get), nameof(DefaultController).Replace("Controller", string.Empty));
// });
app.UseHttpsRedirection();
app.UseHsts();
app.MapControllers();
app.Run();
