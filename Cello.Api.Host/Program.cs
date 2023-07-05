using static Cello.Api.Services.Extensions.FamilyTreeServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFamilyTreeService();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            //policy.AllowCredentials();
        });
});

//builder.WebHost.UseUrls($"http://*:3000");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

try
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
             System.IO.Path.Combine(builder.Environment.ContentRootPath, "static")),
        RequestPath = "/static"
    });
}
catch
{

}

app.UseRouting();

app.UseCors("default");

app.UseEndpoints(endpoints =>
{

    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("<html><head></head><body><script>location = '/static/index.html' + location.search;</script></body></html>");
    });

    endpoints.MapControllers();

});

app.Run();