using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationsWithStudentAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "student_address_id_fk",
                schema: "dbo",
                table: "StudentAddresses");

            migrationBuilder.DropIndex(
                name: "IX_StudentAddresses_student_id",
                schema: "dbo",
                table: "StudentAddresses");

            migrationBuilder.DropColumn(
                name: "student_id",
                schema: "dbo",
                table: "StudentAddresses");

            migrationBuilder.AddColumn<int>(
                name: "address_id",
                schema: "dbo",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students",
                column: "address_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "student_address_student_id_fk",
                schema: "dbo",
                table: "students",
                column: "address_id",
                principalSchema: "dbo",
                principalTable: "StudentAddresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "student_address_student_id_fk",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropColumn(
                name: "address_id",
                schema: "dbo",
                table: "students");

            migrationBuilder.AddColumn<int>(
                name: "student_id",
                schema: "dbo",
                table: "StudentAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddresses_student_id",
                schema: "dbo",
                table: "StudentAddresses",
                column: "student_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "student_address_id_fk",
                schema: "dbo",
                table: "StudentAddresses",
                column: "student_id",
                principalSchema: "dbo",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
