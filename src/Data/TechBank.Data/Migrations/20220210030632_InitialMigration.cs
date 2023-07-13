using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBank.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "contact");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                schema: "contact",
                columns: table => new
                {
                    contact_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    locked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locked_by = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    middle_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    first_last_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    second_last_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.contact_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityPublicKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    Locked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockedBy = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "contact",
                        principalTable: "contact",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "customer",
                schema: "contact",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    locked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locked_by = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_customer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bank_account",
                schema: "account",
                columns: table => new
                {
                    bank_account_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    locked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locked_by = table.Column<int>(type: "int", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankAccountTypeId = table.Column<int>(type: "int", nullable: false),
                    AppCustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_account", x => x.bank_account_id);
                    table.ForeignKey(
                        name: "FK_bank_account_customer_AppCustomerId",
                        column: x => x.AppCustomerId,
                        principalSchema: "contact",
                        principalTable: "customer",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "account_transaction",
                schema: "account",
                columns: table => new
                {
                    account_transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    locked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    locked_by = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    AccountTransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    BankAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_transaction", x => x.account_transaction_id);
                    table.ForeignKey(
                        name: "FK_account_transaction_bank_account_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "account",
                        principalTable: "bank_account",
                        principalColumn: "bank_account_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_transaction_BankAccountId",
                schema: "account",
                table: "account_transaction",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_ContactId",
                table: "AspNetUsers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_bank_account_AppCustomerId",
                schema: "account",
                table: "bank_account",
                column: "AppCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_UserId",
                schema: "contact",
                table: "customer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_transaction",
                schema: "account");

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
                name: "bank_account",
                schema: "account");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "contact");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "contact",
                schema: "contact");
        }
    }
}
