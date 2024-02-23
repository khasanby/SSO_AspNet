using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notes.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteText = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OwnerId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "NoteText", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("3358d2bd-9f30-4442-aa12-f673d23f2d8c"), "Another note by David", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { new Guid("90e92bd6-f222-43ca-a650-fd10a0717f24"), "A note by David", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { new Guid("db314a0e-3205-4c97-910b-01a99037f477"), "Another note by Emma", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { new Guid("e1c4b0b2-302d-4286-b11b-695f6b68215e"), "A note by Emma", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
