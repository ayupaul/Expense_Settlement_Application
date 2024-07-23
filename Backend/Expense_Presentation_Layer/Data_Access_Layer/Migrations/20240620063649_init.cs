using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Amount", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, 1000, "ayush@gmail.com", "Ayush123@", "Ayush" },
                    { 2, 1000, "abhishek@gmail.com", "Abhishek@123", "Abhishek" },
                    { 3, 1000, "ankit@gmail.com", "Ankit123@", "Ankit" },
                    { 4, 1000, "anamika@gmail.com", "Anamika123@", "Anamika" },
                    { 5, 1000, "admin@gmail.com", "Admin123@", "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
