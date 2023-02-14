using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogCategories.Any())
        {
            await context.CatalogCategories.AddRangeAsync(GetPreconfiguredCatalogCategories());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogSubCategories.Any())
        {
            await context.CatalogSubCategories.AddRangeAsync(GetPreconfiguredCatalogSubCategories());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogBrandEntity> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrandEntity>()
        {
            new CatalogBrandEntity() { Title = "Dior" },
            new CatalogBrandEntity() { Title = "Lancome" },
            new CatalogBrandEntity() { Title = "Maybeline" },
            new CatalogBrandEntity() { Title = "NYX" },
            new CatalogBrandEntity() { Title = "Chanel" },
            new CatalogBrandEntity() { Title = "L'Oreal" },
            new CatalogBrandEntity() { Title = "Juvena" }
        };
    }

    private static IEnumerable<CatalogCategoryEntity> GetPreconfiguredCatalogCategories()
    {
        return new List<CatalogCategoryEntity>()
        {
            new CatalogCategoryEntity() { Title = "Perfume" },
            new CatalogCategoryEntity() { Title = "MakeUp" },
            new CatalogCategoryEntity() { Title = "Hair" },
            new CatalogCategoryEntity() { Title = "Face" },
            new CatalogCategoryEntity() { Title = "Body" }
        };
    }

    private static IEnumerable<CatalogSubCategoryEntity> GetPreconfiguredCatalogSubCategories()
    {
        return new List<CatalogSubCategoryEntity>()
        {
            new CatalogSubCategoryEntity() { Title = "Women's perfumery", CatalogCategoryId = 1 },
            new CatalogSubCategoryEntity() { Title = "Men's perfumery", CatalogCategoryId = 1 },
            new CatalogSubCategoryEntity() { Title = "Perfumery unisex", CatalogCategoryId = 1 },
            new CatalogSubCategoryEntity() { Title = "Children's perfumery", CatalogCategoryId = 1 },
            new CatalogSubCategoryEntity() { Title = "Eyes", CatalogCategoryId = 2 },
            new CatalogSubCategoryEntity() { Title = "Brows", CatalogCategoryId = 2 },
            new CatalogSubCategoryEntity() { Title = "Lips", CatalogCategoryId = 2 },
            new CatalogSubCategoryEntity() { Title = "Face", CatalogCategoryId = 2 },
            new CatalogSubCategoryEntity() { Title = "Shampoos", CatalogCategoryId = 3 },
            new CatalogSubCategoryEntity() { Title = "Hair conditioner", CatalogCategoryId = 3 },
            new CatalogSubCategoryEntity() { Title = "Hair Balm", CatalogCategoryId = 3 },
            new CatalogSubCategoryEntity() { Title = "Hair masks", CatalogCategoryId = 3 },
            new CatalogSubCategoryEntity() { Title = "Face cream", CatalogCategoryId = 4 },
            new CatalogSubCategoryEntity() { Title = "Face masks", CatalogCategoryId = 4 },
            new CatalogSubCategoryEntity() { Title = "Serums", CatalogCategoryId = 4 },
            new CatalogSubCategoryEntity() { Title = "Thermal water", CatalogCategoryId = 4 },
            new CatalogSubCategoryEntity() { Title = "Body care", CatalogCategoryId = 5 },
            new CatalogSubCategoryEntity() { Title = "Hand care", CatalogCategoryId = 5 },
            new CatalogSubCategoryEntity() { Title = "Foot care", CatalogCategoryId = 5 },
            new CatalogSubCategoryEntity() { Title = "Massage products", CatalogCategoryId = 5 }
        };
    }

    private static IEnumerable<CatalogItemEntity> GetPreconfiguredItems()
    {
        return new List<CatalogItemEntity>()
        {
            new CatalogItemEntity() { Title = "CHANEL COCO MADEMOISELLE", SubTitle = "perfumed water", PictureFileName = "1.png", Price = 81.95M, AvailableStock = 100, CatalogBrandId = 5, CatalogSubCategoryId = 1, Description = "There are special things for special occasions. And this tart, warming perfume is no exception." },
            new CatalogItemEntity() { Title = "LANCOME LA VIE EST BELLE", SubTitle = "perfumed water", PictureFileName = "2.png", Price = 44.30M, AvailableStock = 100, CatalogBrandId = 2, CatalogSubCategoryId = 1, Description = "It is an exciting, graceful and full of life eau de parfum with magical powers." },
            new CatalogItemEntity() { Title = "DIOR SAUVAGE", SubTitle = "perfumed water", PictureFileName = "3.png", Price = 76.89M, AvailableStock = 100, CatalogBrandId = 1, CatalogSubCategoryId = 2, Description = "Indomitable in character, a glass of eau de toilette was created specifically for rebellious personalities." },
            new CatalogItemEntity() { Title = "NYX Ultimate Shadow Palette", SubTitle = "shadow palette", PictureFileName = "4.png", Price = 12.20M, AvailableStock = 100, CatalogBrandId = 4, CatalogSubCategoryId = 5, Description = "Ultimate Shadow Palette from the world famous American brand Nyx." },
            new CatalogItemEntity() { Title = "CHANEL LES BEIGES EAU DE TEINT", SubTitle = "foundation", PictureFileName = "8.png", Price = 58.90M, AvailableStock = 100, CatalogBrandId = 5, CatalogSubCategoryId = 8, Description = "Chanel Les Beiges Tonal Fluid Tint for the face masterfully masks minor skin imperfections and gives it an inner glow." },
            new CatalogItemEntity() { Title = "Maybelline New York Liquid Lipstick", SubTitle = "liquid lipstick", PictureFileName = "5.png", Price = 7.50M, AvailableStock = 100, CatalogBrandId = 3, CatalogSubCategoryId = 7, Description = "On your lips, lipstick will remain flawless for up to 16 hours, decorating them with a luxurious matte finish and exquisite shade." },
            new CatalogItemEntity() { Title = "Vitamino Color Resveratrol Shampoo", SubTitle = "resveratrol shampoo", PictureFileName = "6.png", Price = 11.15M, AvailableStock = 100, CatalogBrandId = 6, CatalogSubCategoryId = 9, Description = "The masters of the French brand L'Oreal Professionnel have created a special shampoo specifically for colored hair, which will take great care of them." },
            new CatalogItemEntity() { Title = "Dior Prestige La Creme De Nuit Night Cream", SubTitle = "night cream", PictureFileName = "7.png", Price = 70.40M, AvailableStock = 100, CatalogBrandId = 1, CatalogSubCategoryId = 13, Description = "Luxurious facial skin care should be comprehensive and balanced." },
            new CatalogItemEntity() { Title = "Lancome Confort", SubTitle = "hand cream", PictureFileName = "9.png", Price = 6.99M, AvailableStock = 100, CatalogBrandId = 2, CatalogSubCategoryId = 18, Description = "Cream for moisturizing and restoring hand skin with acacia honey extract and rose water." },
            new CatalogItemEntity() { Title = "Performance Vitalizing Massage Oil", SubTitle = "massage oil", PictureFileName = "10.png", Price = 13.00M, AvailableStock = 100, CatalogBrandId = 7, CatalogSubCategoryId = 20, Description = "One of the most effective and instant ways of relaxation is massage." }
        };
    }
}