using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodInspectionTracker.MVC.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Town = table.Column<string>(type: "TEXT", nullable: false),
                    RiskRating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PremisesId = table.Column<int>(type: "INTEGER", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    Outcome = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InspectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUps_Inspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "Inspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Premises",
                columns: new[] { "Id", "Address", "Name", "RiskRating", "Town" },
                values: new object[,]
                {
                    { 1, "12 High St", "The Golden Fork", 0, "Dorchester" },
                    { 2, "34 Market Sq", "Burger Palace", 1, "Dorchester" },
                    { 3, "5 Harbour Rd", "Sea Breeze Fish & Chips", 2, "Dorchester" },
                    { 4, "78 Church Lane", "Mama's Italian Kitchen", 0, "Dorchester" },
                    { 5, "2 Station Rd", "Sunrise Cafe", 1, "Weymouth" },
                    { 6, "19 Pier Approach", "The Rusty Anchor", 2, "Weymouth" },
                    { 7, "45 Park Ave", "Green Garden Deli", 0, "Weymouth" },
                    { 8, "7 Broadway", "Hot Wok Express", 1, "Weymouth" },
                    { 9, "1 Kings Rd", "The Crown Pub", 1, "Bridport" },
                    { 10, "22 West St", "Bridport Bakery", 0, "Bridport" },
                    { 11, "88 South St", "Spice Route", 2, "Bridport" },
                    { 12, "3 Cliff Road", "Coastline Catering", 1, "Bridport" }
                });

            migrationBuilder.InsertData(
                table: "Inspections",
                columns: new[] { "Id", "InspectionDate", "Notes", "Outcome", "PremisesId", "Score" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Excellent hygiene standards.", 0, 1, 92 },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Inadequate cold storage temperatures.", 1, 2, 55 },
                    { 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cross-contamination risks identified.", 1, 3, 48 },
                    { 4, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Good overall, minor paperwork issues.", 0, 4, 88 },
                    { 5, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Satisfactory.", 0, 5, 76 },
                    { 6, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rodent evidence found in storeroom.", 1, 6, 41 },
                    { 7, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Outstanding. No issues.", 0, 7, 95 },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pass with recommendations.", 0, 8, 63 },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Handwashing facilities non-compliant.", 1, 9, 52 },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Good standards maintained.", 0, 10, 84 },
                    { 11, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Critical violations — pest control.", 1, 11, 38 },
                    { 12, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Adequate, staff training recommended.", 0, 12, 71 },
                    { 13, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Follow-up: still excellent.", 0, 1, 90 },
                    { 14, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Cold storage now fixed.", 0, 2, 68 },
                    { 15, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Still non-compliant on storage.", 1, 3, 59 },
                    { 16, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Paperwork now in order.", 0, 4, 91 },
                    { 17, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Consistent performance.", 0, 5, 80 },
                    { 18, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Pest issue resolved.", 0, 6, 66 },
                    { 19, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Exemplary.", 0, 7, 97 },
                    { 20, new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Improvements noted.", 0, 8, 74 },
                    { 21, new DateTime(2026, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Handwashing facilities upgraded.", 0, 9, 70 },
                    { 22, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Consistent high standard.", 0, 10, 88 },
                    { 23, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Partial improvement, still failing.", 1, 11, 45 },
                    { 24, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Good progress.", 0, 12, 83 },
                    { 25, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "All cleared.", 0, 6, 78 }
                });

            migrationBuilder.InsertData(
                table: "FollowUps",
                columns: new[] { "Id", "ClosedDate", "DueDate", "InspectionId", "Notes", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Cold storage repaired and verified.", 1 },
                    { 2, new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Separation protocols implemented.", 1 },
                    { 3, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, "Pest control contractor report submitted.", 1 },
                    { 4, null, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Handwashing facilities replacement pending.", 0 },
                    { 5, null, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 11, "Pest control contract not yet signed.", 0 },
                    { 6, null, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 15, "Storage unit replacement ordered.", 0 },
                    { 7, null, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), 23, "Deep clean and pest exclusion required.", 0 },
                    { 8, null, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 23, "Staff food hygiene training scheduled.", 0 },
                    { 9, null, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 15, "Re-inspection scheduled for next month.", 0 },
                    { 10, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Interim fix verified by inspector.", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_InspectionId",
                table: "FollowUps",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_PremisesId",
                table: "Inspections",
                column: "PremisesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "Premises");
        }
    }
}
