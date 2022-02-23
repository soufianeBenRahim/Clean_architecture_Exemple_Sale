﻿namespace Clean_Architecture_Soufiane.Infrastructure.Persistence
{
    using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
    using Clean_Architecture_Soufiane.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Clean_Architecture_Soufiane.Domain.AggregatesModel.Catalog;
    using System.Globalization;
    using System.IO.Compression;

    using System.Text.RegularExpressions;
    using Clean_Architecture_Soufiane.Domain.AggregatesModel.Identity;

    public class ApplicationDbContextSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed> logger)
        {

            using (context)
                {
                    if (!context.SaleStatuss.Any())
                    {
                        context.SaleStatuss.AddRange( GetPredefinedSaleStatus());
                    }

                    await context.SaveChangesAsync();


                if (!context.CatalogBrands.Any())
                {
                    await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

                    await context.SaveChangesAsync();
                }

                if (!context.CatalogTypes.Any())
                {
                    await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

                    await context.SaveChangesAsync();
                }

                if (!context.CatalogItems.Any())
                {
                    await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

                    await context.SaveChangesAsync();

                   // GetCatalogItemPictures(Environment.CurrentDirectory,Path.Combine( Environment.CurrentDirectory,"Images"));
                }
                if (!context.Users.Any())
                {
                    await context.Users.AddAsync(new ApplicationUser() { ID = Guid.NewGuid(), Password = "1234", UserName = "soufiane" });
                    await context.SaveChangesAsync();
                }
            }

        }

        private IEnumerable<SaleStatus> GetPredefinedSaleStatus()
        {
            return new List<SaleStatus>()
            {
                SaleStatus.AwaitingValidation,
                SaleStatus.Paid,
                SaleStatus.Cancelled
            };
        }


        private IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Brand = "Azure"},
                new CatalogBrand() { Brand = ".NET" },
                new CatalogBrand() { Brand = "Visual Studio" },
                new CatalogBrand() { Brand = "SQL Server" },
                new CatalogBrand() { Brand = "Other" }
            };
        }

      

      

        private IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Type = "Mug"},
                new CatalogType() { Type = "T-Shirt" },
                new CatalogType() { Type = "Sheet" },
                new CatalogType() { Type = "USB Memory Stick" }
            };
        }

    

   

       


        public static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem {Id=1,ShortCut="001", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Bot Black Hoodie", Name = ".NET Bot Black Hoodie", Price = 19.5M, PictureFileName = "1.png",Bar_Code="1111" },
                new CatalogItem {Id=2,ShortCut="003", CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Black & White Mug", Name = ".NET Black & White Mug", Price= 8.50M, PictureFileName = "2.png" ,Bar_Code="2222" },
                new CatalogItem {Id=3,ShortCut="003", CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Prism White T-Shirt", Name = "Prism White T-Shirt", Price = 12, PictureFileName = "3.png",Bar_Code="3333"  },
                new CatalogItem {Id=4, CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Foundation T-shirt", Name = ".NET Foundation T-shirt", Price = 12, PictureFileName = "4.png" ,Bar_Code="4444" },
                new CatalogItem {Id=5, CatalogTypeId = 3, CatalogBrandId = 5, AvailableStock = 100, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M, PictureFileName = "5.png",Bar_Code="5555"  },
                new CatalogItem {Id=6, CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Blue Hoodie", Name = ".NET Blue Hoodie", Price = 12, PictureFileName = "6.png",Bar_Code="6666"  },
                new CatalogItem {Id=7, CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Roslyn Red T-Shirt", Name = "Roslyn Red T-Shirt", Price = 12, PictureFileName = "7.png",Bar_Code="7777"  },
                new CatalogItem {Id=8, CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Kudu Purple Hoodie", Name = "Kudu Purple Hoodie", Price = 8.5M, PictureFileName = "8.png",Bar_Code="8888"  },
                new CatalogItem {Id=9, CatalogTypeId = 1, CatalogBrandId = 5, AvailableStock = 100, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug", Price = 12, PictureFileName = "9.png" ,Bar_Code="9999" },
                new CatalogItem {Id=10, CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Foundation Sheet", Name = ".NET Foundation Sheet", Price = 12, PictureFileName = "10.png",Bar_Code="1011"  },
                new CatalogItem {Id=11, CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 100, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet", Price = 8.5M, PictureFileName = "11.png" ,Bar_Code="1000" },
                new CatalogItem {Id=12, CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Prism White TShirt", Name = "Prism White TShirt", Price = 12, PictureFileName = "12.png",Bar_Code="1000"  },
            };
        }

      
        private void GetCatalogItemPictures(string contentRootPath, string picturePath)
        {
            if (picturePath != null)
            {
                DirectoryInfo directory = new DirectoryInfo(picturePath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                string zipFileCatalogItemPictures = Path.Combine(contentRootPath, "Setup", "CatalogItems.zip");
                ZipFile.ExtractToDirectory(zipFileCatalogItemPictures, picturePath);
            }
        }

    }
}
