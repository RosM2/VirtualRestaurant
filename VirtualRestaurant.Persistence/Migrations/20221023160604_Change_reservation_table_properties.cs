using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualRestaurant.Persistence.Migrations
{
    public partial class Change_reservation_table_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Reservations");
        }
    }
}
