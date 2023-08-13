using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.AccesoDatos.Migrations
{
    public partial class agregandoTablaRentaS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rentas",
                schema: "SEG",
                columns: table => new
                {
                    RentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRenta = table.Column<string>(type: "varchar(200)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", nullable: true),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.RentaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentas",
                schema: "SEG");
        }
    }
}
