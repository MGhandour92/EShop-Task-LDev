using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronics_Shop.Migrations
{
    public partial class intitDBWSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverToAddress = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DisountToApply = table.Column<byte>(type: "tinyint", nullable: false),
                    CategoryId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AfterDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_OrderHeader_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)1, "TVs" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)2, "Laptops" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)3, "Sound Systems" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "DisountToApply", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, (short)1, "Samsung TV 32 inch", (byte)10, "Samsung 32", 2500m },
                    { 2, (short)1, "LG TV 32 Inch Satellite", (byte)20, "LG 32", 3000m },
                    { 3, (short)1, "Toshiba 49 inch oled", (byte)0, "Toshiba 49", 5000m },
                    { 4, (short)1, "Toshiba 64 inch QLED", (byte)15, "Toshiba 64", 9000m },
                    { 5, (short)1, "LG 50 inch QLED", (byte)30, "LG 50", 10000m },
                    { 6, (short)1, "Samsung 65 QLED 4K", (byte)25, "Samsung 65", 15000m },
                    { 7, (short)2, "HP Probook 15 inch i7 G3", (byte)10, "HP Probook", 11000m },
                    { 8, (short)2, "Dell Latitude 15 inch i5", (byte)0, "Dell Latitude", 9500m },
                    { 9, (short)2, "Lenovo Thinkpad 15 inch i5", (byte)15, "Lenovo Thinkpad", 9000m },
                    { 10, (short)2, "Dell Vostro 15 inch FHD i7", (byte)0, "Dell Vostro", 10500m },
                    { 11, (short)3, "YHT-4950U 4K Ultra HD 5.1-Channel Home Theater System with Bluetooth", (byte)5, "Yamaha", 10800m },
                    { 12, (short)3, "Sony Complete 8 Speaker System- SSCS3 (2), SSCS5, SSCS8, SACS9, SSCSE", (byte)15, "Sony", 25000m },
                    { 13, (short)3, "JBL Professional EON208P Portable All-in-One 2-way PA System with 8-Channel Mixer and Bluetooth", (byte)0, "JBL", 16000m },
                    { 14, (short)3, "Logitech Z906 5.1 Surround Sound Speaker System - THX, Dolby Digital and DTS Digital Certified - Black", (byte)5, "Logitech", 8000m },
                    { 15, (short)3, "Klipsch Black Reference Theater Pack 5.1 Surround Sound System", (byte)10, "Klipsch", 6000m },
                    { 16, (short)3, "Earthquake Sound DJ-Array Gen2 4x4 Line Array Loudspeaker System, Set of 2", (byte)0, "Earthquake", 8750m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderHeaderId",
                table: "OrderLine",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ProductId",
                table: "OrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
