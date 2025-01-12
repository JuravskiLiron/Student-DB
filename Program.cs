﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRUD_Students.Data;

using Microsoft.Extensions.FileProviders;
namespace CRUD_Students
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<CRUD_StudentsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CRUD_StudentsContext") ?? throw new InvalidOperationException("Connection string 'CRUD_StudentsContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
                RequestPath = "/uploads"
            });

            

            app.UseAuthorization();

            app.MapRazorPages();
            
            app.MapGet("/", () => Results.Redirect("/User/Index"));
            
            app.Run();
        }
    }
}
