using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VAPV.Models;
using VAPV.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Pøidání služeb do kontejneru
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ExcuseDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("VAPVConnection"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("MonsterVAPVConnection"));
});

builder.Services.AddScoped<ExcuseService>();
builder.Services.AddScoped<CalendarService>();
var app = builder.Build();

// Konfigurace HTTP request pipeline
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Inicializace databáze
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.Run();

//metoda pro okamžité pøidání novì vložených objektù (do excuses.json) do databáze
public static class SeedData {
    public static void Initialize(IServiceProvider serviceProvider) {
        using (var context = serviceProvider.GetRequiredService<ExcuseDbContext>()) {
            var jsonExcuses = JsonConvert.DeserializeObject<List<Excuse>>(
                File.ReadAllText("excuses.json"));

            foreach (var jsonExcuse in jsonExcuses) {
                var existingExcuse = context.Excuses
                    .FirstOrDefault(e => e.Category == jsonExcuse.Category && e.Text == jsonExcuse.Text);

                if (existingExcuse == null) {
                    // Pokud záznam neexistuje, pokusíme se ho pøidat
                    try {
                        context.Excuses.Add(jsonExcuse); //zde EF zaøídí automatické autoinkrementované vygenerování ID a uložení do db pod tímto id
                        context.SaveChanges();
                    }
                    catch (DbUpdateException) {
                        // Pokud nastane chyba pøi pøidávání, nastavíme IDENTITY_INSERT na ON
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Excuses] ON");
                        context.Excuses.Add(jsonExcuse);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Excuses] OFF");
                    }
                }
                else {
                    // Pokud záznam existuje, aktualizujeme ho
                    existingExcuse.Category = jsonExcuse.Category;
                    existingExcuse.Text = jsonExcuse.Text;
                    context.Excuses.Update(existingExcuse);
                    context.SaveChanges();
                }
            }
        }
    }
}



