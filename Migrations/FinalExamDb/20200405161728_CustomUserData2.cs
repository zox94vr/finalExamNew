using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalExamNew.Migrations.FinalExamDb
{
    public partial class CustomUserData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cene",
                columns: table => new
                {
                    CenaId = table.Column<string>(nullable: false),
                    Vrednost = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Valuta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cene", x => x.CenaId);
                });

            migrationBuilder.CreateTable(
                name: "KljucnaRec",
                columns: table => new
                {
                    KljucnaRecId = table.Column<string>(nullable: false),
                    Rec = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KljucnaRec", x => x.KljucnaRecId);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    OglasId = table.Column<string>(nullable: false),
                    Naslov = table.Column<string>(nullable: true),
                    Tekst = table.Column<string>(nullable: true),
                    CenaId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.OglasId);
                    table.ForeignKey(
                        name: "FK_Oglas_Cene_CenaId",
                        column: x => x.CenaId,
                        principalTable: "Cene",
                        principalColumn: "CenaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Oglas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipOglasa",
                columns: table => new
                {
                    TipOglasaId = table.Column<string>(nullable: false),
                    NazivTipaOglasa = table.Column<string>(nullable: true),
                    CenaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipOglasa", x => x.TipOglasaId);
                    table.ForeignKey(
                        name: "FK_TipOglasa_Cene_CenaId",
                        column: x => x.CenaId,
                        principalTable: "Cene",
                        principalColumn: "CenaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KljucneReciOglasa",
                columns: table => new
                {
                    KljucneReciOglasaId = table.Column<string>(nullable: false),
                    KljucnaRecId1 = table.Column<string>(nullable: true),
                    OglasId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KljucneReciOglasa", x => x.KljucneReciOglasaId);
                    table.UniqueConstraint("AK_KljucneReciOglasa_KljucneReciOglasaId_OglasId", x => new { x.KljucneReciOglasaId, x.OglasId });
                    table.ForeignKey(
                        name: "FK_KljucneReciOglasa_KljucnaRec_KljucnaRecId1",
                        column: x => x.KljucnaRecId1,
                        principalTable: "KljucnaRec",
                        principalColumn: "KljucnaRecId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KljucneReciOglasa_Oglas_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglas",
                        principalColumn: "OglasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slika",
                columns: table => new
                {
                    SlikaId = table.Column<string>(nullable: false),
                    AdresaSlike = table.Column<string>(nullable: true),
                    NaslovSlike = table.Column<string>(nullable: true),
                    VremePostavljanjaSlike = table.Column<DateTime>(nullable: false),
                    OglasId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slika", x => new { x.SlikaId, x.OglasId });
                    table.UniqueConstraint("AK_Slika_OglasId_SlikaId", x => new { x.OglasId, x.SlikaId });
                    table.ForeignKey(
                        name: "FK_Slika_Oglas_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglas",
                        principalColumn: "OglasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oglasavanje",
                columns: table => new
                {
                    OglasavanjeId = table.Column<string>(nullable: false),
                    TipOglasaId1 = table.Column<string>(nullable: true),
                    OglasId1 = table.Column<string>(nullable: true),
                    OglasId = table.Column<string>(nullable: false),
                    DatumOd = table.Column<DateTime>(nullable: false),
                    DatumDo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasavanje", x => x.OglasId);
                    table.UniqueConstraint("AK_Oglasavanje_OglasavanjeId_OglasId", x => new { x.OglasavanjeId, x.OglasId });
                    table.ForeignKey(
                        name: "FK_Oglasavanje_Oglas_OglasId1",
                        column: x => x.OglasId1,
                        principalTable: "Oglas",
                        principalColumn: "OglasId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Oglasavanje_TipOglasa_TipOglasaId1",
                        column: x => x.TipOglasaId1,
                        principalTable: "TipOglasa",
                        principalColumn: "TipOglasaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KljucneReciOglasa_KljucnaRecId1",
                table: "KljucneReciOglasa",
                column: "KljucnaRecId1");

            migrationBuilder.CreateIndex(
                name: "IX_KljucneReciOglasa_OglasId",
                table: "KljucneReciOglasa",
                column: "OglasId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_CenaId",
                table: "Oglas",
                column: "CenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_UserId",
                table: "Oglas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglasavanje_OglasId1",
                table: "Oglasavanje",
                column: "OglasId1");

            migrationBuilder.CreateIndex(
                name: "IX_Oglasavanje_TipOglasaId1",
                table: "Oglasavanje",
                column: "TipOglasaId1");

            migrationBuilder.CreateIndex(
                name: "IX_TipOglasa_CenaId",
                table: "TipOglasa",
                column: "CenaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KljucneReciOglasa");

            migrationBuilder.DropTable(
                name: "Oglasavanje");

            migrationBuilder.DropTable(
                name: "Slika");

            migrationBuilder.DropTable(
                name: "KljucnaRec");

            migrationBuilder.DropTable(
                name: "TipOglasa");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Cene");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
