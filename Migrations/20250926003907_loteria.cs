using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loterria_Bank.Migrations
{
    /// <inheritdoc />
    public partial class loteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loterias",
                columns: table => new
                {
                    LoteriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Activa = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loterias", x => x.LoteriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Jugadas",
                columns: table => new
                {
                    JugadaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroTicket = table.Column<string>(type: "TEXT", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LoteriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrimerLugar = table.Column<int>(type: "INTEGER", nullable: true),
                    SegundoLugar = table.Column<int>(type: "INTEGER", nullable: true),
                    TercerLugar = table.Column<int>(type: "INTEGER", nullable: true),
                    Pick4 = table.Column<string>(type: "TEXT", nullable: true),
                    Pick5 = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadas", x => x.JugadaId);
                    table.ForeignKey(
                        name: "FK_Jugadas_Loterias_LoteriaId",
                        column: x => x.LoteriaId,
                        principalTable: "Loterias",
                        principalColumn: "LoteriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleJugadas",
                columns: table => new
                {
                    DetalleJugadaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    JugadaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleJugadas", x => x.DetalleJugadaId);
                    table.ForeignKey(
                        name: "FK_DetalleJugadas_Jugadas_JugadaId",
                        column: x => x.JugadaId,
                        principalTable: "Jugadas",
                        principalColumn: "JugadaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleJugadas_JugadaId",
                table: "DetalleJugadas",
                column: "JugadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadas_LoteriaId",
                table: "Jugadas",
                column: "LoteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleJugadas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Jugadas");

            migrationBuilder.DropTable(
                name: "Loterias");
        }
    }
}
