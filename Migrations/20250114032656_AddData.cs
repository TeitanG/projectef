using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projectef.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "peso" },
                values: new object[,]
                {
                    { new Guid("0e1c33da-c46f-4fa0-8c2f-18a2ad76e20f"), "Descripcion pendiente", "Actividades Pendientes", 20 },
                    { new Guid("b2c36661-97d0-4fa7-8982-ee49db042e93"), "Descripcion pendiente", "Actividades Personales", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("0e1c33da-c46f-4fa0-8c2f-18a2ad76e20f"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("b2c36661-97d0-4fa7-8982-ee49db042e93"));
        }
    }
}
