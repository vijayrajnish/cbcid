using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBCID_APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class addgavahatabletodatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YEAR",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATEDON",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UPDATEDBY",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TODATE",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SECTORID",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RSHAKSHI_NAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "PSHAKSHI_NAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "MONTH",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FRMDATE",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATEDON",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CREATEDBY",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "CBCID_NO",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "BNAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "APRADHNO",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ADATHAN_STATUS",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ACT",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "ABHIYUKT",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<int>(
                name: "FMTID",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddGavaha",
                columns: table => new
                {
                    GavahaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DCFCID = table.Column<int>(type: "int", nullable: false),
                    GavahaName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GavaType = table.Column<int>(type: "int", nullable: true),
                    AttendenceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entrydate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdby = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    createdate = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    updatedby = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    updatedon = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddGavaha", x => x.GavahaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddGavaha");

            migrationBuilder.DropColumn(
                name: "FMTID",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN");

            migrationBuilder.AlterColumn<int>(
                name: "YEAR",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATEDON",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UPDATEDBY",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TODATE",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SECTORID",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RSHAKSHI_NAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PSHAKSHI_NAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MONTH",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FRMDATE",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATEDON",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CREATEDBY",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CBCID_NO",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BNAME",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "APRADHNO",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ADATHAN_STATUS",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ACT",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ABHIYUKT",
                table: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
