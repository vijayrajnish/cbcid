using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBCID_APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class ADD_FORMAT1_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FMT1_DET_CRIME_FEMALE_CHILDREN",
                columns: table => new
                {
                    DCFCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FRMDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TODATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SECTORID = table.Column<int>(type: "int", nullable: false),
                    DISTRICTID = table.Column<int>(type: "int", nullable: false),
                    CBCID_NO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    APRADHNO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ACT = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    THANAID = table.Column<int>(type: "int", nullable: false),
                    BNAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    COURTID = table.Column<int>(type: "int", nullable: false),
                    DAKILDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARROPVICHARANTHITHI = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PSHAKSHI_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RSHAKSHI_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ADATHAN_STATUS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NEXT_HEARING_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ABHIYUKT = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    YEAR = table.Column<int>(type: "int", nullable: false),
                    MONTH = table.Column<int>(type: "int", nullable: false),
                    REMARK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CREATEDBY = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CREATEDON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEDBY = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UPDATEDON = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMT1_DET_CRIME_FEMALE_CHILDREN", x => x.DCFCID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FMT1_DET_CRIME_FEMALE_CHILDREN");
        }
    }
}
