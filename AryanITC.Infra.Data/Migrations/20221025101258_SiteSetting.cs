using Microsoft.EntityFrameworkCore.Migrations;

namespace AryanITC.Infra.Data.Migrations
{
    public partial class SiteSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanServiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanPortfolioTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanPortfolioGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanHostPriceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanHostPriceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanDomainTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanDomainDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanCloudHostTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AryanCloudHostDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SitePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteLogo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");
        }
    }
}
