using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerProfile.Migrations
{
    public partial class NoCompositPrimairyKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_customerprofile_CustomerProfileID",
                table: "customerprofile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customerprofile",
                table: "customerprofile");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "customerprofile",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "customerprofile",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_customerprofile",
                table: "customerprofile",
                column: "CustomerProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_customerprofile",
                table: "customerprofile");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "customerprofile",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "customerprofile",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_customerprofile_CustomerProfileID",
                table: "customerprofile",
                column: "CustomerProfileID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customerprofile",
                table: "customerprofile",
                columns: new[] { "Name", "PhoneNumber" });
        }
    }
}
