using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnacksUdemy.Migrations
{
    public partial class InsertCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(CategoryName,Descripton)" +
              "VALUES('Normal','Hamburger and Salad') ");
            migrationBuilder.Sql("INSERT INTO Categories(CategoryName,Descripton)" +
             "VALUES('Normal','Hamburger and Salad') ");
            migrationBuilder.Sql("INSERT INTO Categories(CategoryName,Descripton)" +
             "VALUES('Natural','Salad') ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
