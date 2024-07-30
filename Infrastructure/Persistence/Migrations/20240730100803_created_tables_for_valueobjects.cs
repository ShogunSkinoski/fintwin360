using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class created_tables_for_valueobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Latitude",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Longitude",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "EducationLevel_EducationCode",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "EducationLevel_EducationLevelName",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "EmploymentStatus_EmploymentIndustry",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "EmploymentStatus_Status",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "FamilySize_Size",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "ResidentialInformation_Status",
                table: "MemberProfiles");

            migrationBuilder.DropColumn(
                name: "ResidentialInformation_Type",
                table: "MemberProfiles");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    MemberProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Street = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<float>(type: "float", nullable: true),
                    Longitude = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.MemberProfileId);
                    table.ForeignKey(
                        name: "FK_Addresses_MemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "MemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    MemberProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EducationLevelName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EducationCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.MemberProfileId);
                    table.ForeignKey(
                        name: "FK_EducationLevels_MemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "MemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmploymentStatuses",
                columns: table => new
                {
                    MemberProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmploymentIndustry = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentStatuses", x => x.MemberProfileId);
                    table.ForeignKey(
                        name: "FK_EmploymentStatuses_MemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "MemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FamilySizes",
                columns: table => new
                {
                    MemberProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilySizes", x => x.MemberProfileId);
                    table.ForeignKey(
                        name: "FK_FamilySizes_MemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "MemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResidentialInformation",
                columns: table => new
                {
                    MemberProfileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialInformation", x => x.MemberProfileId);
                    table.ForeignKey(
                        name: "FK_ResidentialInformation_MemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "MemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropTable(
                name: "EmploymentStatuses");

            migrationBuilder.DropTable(
                name: "FamilySizes");

            migrationBuilder.DropTable(
                name: "ResidentialInformation");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "MemberProfiles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "MemberProfiles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "Address_Latitude",
                table: "MemberProfiles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Address_Longitude",
                table: "MemberProfiles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "MemberProfiles",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EducationLevel_EducationCode",
                table: "MemberProfiles",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EducationLevel_EducationLevelName",
                table: "MemberProfiles",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentStatus_EmploymentIndustry",
                table: "MemberProfiles",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentStatus_Status",
                table: "MemberProfiles",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FamilySize_Size",
                table: "MemberProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResidentialInformation_Status",
                table: "MemberProfiles",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ResidentialInformation_Type",
                table: "MemberProfiles",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
