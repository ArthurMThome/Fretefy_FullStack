using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Fretefy.Test.Infra.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Regiao",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Nome = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Regiao", x => x.Id);
            });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true),
                    RegiaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidade_Regiao_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regiao",
                        principalColumn: "Id"
                    );
                });

            migrationBuilder.InsertData(
                table: "Regiao",
                columns: new[] { "Id", "Nome" },
                values: new object[] { new Guid("afc12ff5-eacd-420a-835a-69e9b3b78456"), "Sul" }
            );

            migrationBuilder.InsertData(
                table: "Cidade",
                columns: new[] { "Id", "Nome", "UF", "RegiaoId" },
                values: new object[] { new Guid("c215fc2e-bcda-4404-8efb-4b23c4433c5b"), "Curitiba", "PR", Guid.Parse("afc12ff5-eacd-420a-835a-69e9b3b78456") }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
