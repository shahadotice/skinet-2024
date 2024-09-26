using System.Reflection;
using System.Text.Json;
using Core.Entity;


namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
      

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            // var productsData = await File
            //     .ReadAllTextAsync(path + @"/Data/SeedData/products.json");
                   var productsData = await File
                .ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }

        
    }
}
