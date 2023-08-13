using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.AccesoDatos.Migrations
{
    public partial class primerMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AUDI");

            migrationBuilder.EnsureSchema(
                name: "MENU");

            migrationBuilder.EnsureSchema(
                name: "SEG");

            migrationBuilder.CreateTable(
                name: "LogError",
                schema: "AUDI",
                columns: table => new
                {
                    LogErrorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "varchar(200)", nullable: false),
                    FechaProceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgramaProcedimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionErrorBD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variables = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogError", x => x.LogErrorID);
                });

            migrationBuilder.CreateTable(
                name: "LogTransaccion",
                schema: "AUDI",
                columns: table => new
                {
                    LogTransaccionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaProceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variables = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTransaccion", x => x.LogTransaccionID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "MENU",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controlador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreVista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    EstaAnulado = table.Column<bool>(type: "bit", nullable: false),
                    IconoCss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posicion = table.Column<int>(type: "int", nullable: false),
                    PadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentaId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesMenu",
                schema: "MENU",
                columns: table => new
                {
                    RolesMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstaAnulado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesMenu", x => x.RolesMenuId);
                    table.ForeignKey(
                        name: "FK_RolesMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "MENU",
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesMenu_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesNotificaciones",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesNotificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesNotificaciones_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosLogin",
                schema: "SEG",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UsuariosLogin_Usuarios_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosNotificaciones",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosNotificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosNotificaciones_Usuarios_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                schema: "SEG",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosToken",
                schema: "SEG",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    UsuarioLogId = table.Column<int>(type: "int", nullable: false),
                    EsBorrado = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsuariosToken_Usuarios_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "SEG",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolesMenu_MenuId",
                schema: "MENU",
                table: "RolesMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesMenu_RoleId",
                schema: "MENU",
                table: "RolesMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesNotificaciones_RoleId",
                schema: "SEG",
                table: "RolesNotificaciones",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "SEG",
                table: "Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "SEG",
                table: "Usuarios",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosLogin_UserId",
                schema: "SEG",
                table: "UsuariosLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosNotificaciones_UserId",
                schema: "SEG",
                table: "UsuariosNotificaciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RoleId",
                schema: "SEG",
                table: "UsuariosRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogError",
                schema: "AUDI");

            migrationBuilder.DropTable(
                name: "LogTransaccion",
                schema: "AUDI");

            migrationBuilder.DropTable(
                name: "RolesMenu",
                schema: "MENU");

            migrationBuilder.DropTable(
                name: "RolesNotificaciones",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsuariosLogin",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsuariosNotificaciones",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsuariosRoles",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsuariosToken",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "MENU");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "SEG");
        }
    }
}
