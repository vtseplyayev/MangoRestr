using Microsoft.EntityFrameworkCore.Migrations;

namespace Mango.Services.ProductAPI.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageSrc", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Samosa Description", "https://dotnetviktor.blob.core.windows.net/mango/samosa.jpg", "Samosa", 15.0 },
                    { 2, "Appetizer", "Paneer Tikka Description", "https://dotnetviktor.blob.core.windows.net/mango/paneer-tikka.webp", "Paneer Tikka", 13.99 },
                    { 3, "Dessert", "Sweet Pie Description", "https://dotnetviktor.blob.core.windows.net/mango/Sweet-Pie.jpeg", "Sweet Pie", 10.99 },
                    { 4, "Entree", "Pav Bhaji Description", "https://dotnetviktor.blob.core.windows.net/mango/pav-bhaji.jpeg", "Pav Bhaji", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
