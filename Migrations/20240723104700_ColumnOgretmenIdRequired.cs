﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class ColumnOgretmenIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar");

            migrationBuilder.RenameColumn(
                name: "DaslamaTarihi",
                table: "Ogretmenler",
                newName: "BaslamaTarihi");

            migrationBuilder.AlterColumn<int>(
                name: "OgretmenId",
                table: "Kurslar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar",
                column: "OgretmenId",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar");

            migrationBuilder.RenameColumn(
                name: "BaslamaTarihi",
                table: "Ogretmenler",
                newName: "DaslamaTarihi");

            migrationBuilder.AlterColumn<int>(
                name: "OgretmenId",
                table: "Kurslar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar",
                column: "OgretmenId",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenId");
        }
    }
}
