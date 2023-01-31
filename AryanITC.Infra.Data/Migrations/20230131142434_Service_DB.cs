using Microsoft.EntityFrameworkCore.Migrations;

namespace AryanITC.Infra.Data.Migrations
{
    public partial class Service_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AboutUsImage",
                table: "AboutUs",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ServiceLink = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ServiceImage = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    FontAwesome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontAwesomeColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "AboutUsImage",
                table: "AboutUs",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600,
                oldNullable: true);
        }
    }
}
