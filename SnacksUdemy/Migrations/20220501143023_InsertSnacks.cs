using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnacksUdemy.Migrations
{
    public partial class InsertSnacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO " +
                    "Snacks(CategoryId,ShortDescripton,DetailedDescripton,Stock,ImageThumbnailUrl,ImageUrl,IsFavoriteSnack," +
                    "Name,Price)" +
                    " VALUES (1,'Bread,Hamburger,Eggs','Delicious hamburger bun with fried egg'," +
                    "1, 'cheesesalad1.jpg', 'cheesesalad1.jpg', 0, 'Cheese Salad', 10.50)");
            migrationBuilder.Sql("INSERT INTO " +
                   "Snacks(CategoryId,ShortDescripton,DetailedDescripton,Stock,ImageThumbnailUrl,ImageUrl,IsFavoriteSnack," +
                   "Name,Price)" +
                   " VALUES (2,'Bread,Hamburger,Eggs,Cheese','Delicious hamburger bun with fried egg; ham and cheese'," +
                   "1, 'cheesesalad1.jpg', 'cheesesalad1.jpg', 0, 'Cheese Salad', 15.50)");
                    migrationBuilder.Sql("INSERT INTO " +
                 "Snacks(CategoryId,ShortDescripton,DetailedDescripton,Stock,ImageThumbnailUrl,ImageUrl,IsFavoriteSnack," +
                  "Name,Price)" +
                   " VALUES (3,'Bread,Hamburger,Eggs,Cheese,Plaha Potato','Delicious hamburger bun with fried egg; ham and cheese and with potato sticks'," +
                    "1, 'cheesesalad1.jpg', 'cheesesalad1.jpg', 0, 'Cheese Salad', 20.50)");
            migrationBuilder.Sql("INSERT INTO " +
                     "Snacks(CategoryId,ShortDescripton,DetailedDescripton,Stock,ImageThumbnailUrl,ImageUrl,IsFavoriteSnack," +
                      "Name,Price)" +
                       " VALUES (1,'Bread,Hamburger,Eggs,Cheese,Plaha Potato','Delicious hamburger bun with fried egg; ham and cheese and with potato sticks'," +
                        "1, 'cheesesalad1.jpg', 'cheesesalad1.jpg', 1, 'Cheese Salad', 25.50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Snacks");
        }
    }
}
