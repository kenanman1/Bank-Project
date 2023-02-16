using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankProject.Migrations
{
    /// <inheritdoc />
    public partial class changeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amountHistoryVIPs_VIPs_VIP_UserId",
                table: "amountHistoryVIPs");

            migrationBuilder.DropForeignKey(
                name: "FK_VIPAccounts_VIPs_VIP_UserId",
                table: "VIPAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VIPs",
                table: "VIPs");

            migrationBuilder.RenameTable(
                name: "VIPs",
                newName: "UsersVIP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersVIP",
                table: "UsersVIP",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_amountHistoryVIPs_UsersVIP_VIP_UserId",
                table: "amountHistoryVIPs",
                column: "VIP_UserId",
                principalTable: "UsersVIP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VIPAccounts_UsersVIP_VIP_UserId",
                table: "VIPAccounts",
                column: "VIP_UserId",
                principalTable: "UsersVIP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amountHistoryVIPs_UsersVIP_VIP_UserId",
                table: "amountHistoryVIPs");

            migrationBuilder.DropForeignKey(
                name: "FK_VIPAccounts_UsersVIP_VIP_UserId",
                table: "VIPAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersVIP",
                table: "UsersVIP");

            migrationBuilder.RenameTable(
                name: "UsersVIP",
                newName: "VIPs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VIPs",
                table: "VIPs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_amountHistoryVIPs_VIPs_VIP_UserId",
                table: "amountHistoryVIPs",
                column: "VIP_UserId",
                principalTable: "VIPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VIPAccounts_VIPs_VIP_UserId",
                table: "VIPAccounts",
                column: "VIP_UserId",
                principalTable: "VIPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
