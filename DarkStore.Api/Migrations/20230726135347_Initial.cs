using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DarkStore.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCardId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Livros" },
                    { 2, "Fantasias" },
                    { 3, "Quadros e Pôsteres" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[,]
                {
                    { 1, "Raquel Silva" },
                    { 2, "Rafael Silva" },
                    { 3, "Samuel Silva" },
                    { 4, "Ana Silva" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Salem é um clássico do gênero de horror e apresenta os elementos característicos das obras de Stephen King, como personagens complexos, tensão psicológica e uma atmosfera sombria. É considerado um dos melhores romances de vampiro já escritos e consolidou o lugar de King como um mestre do terror moderno.", "https://i.imgur.com/marl3ow.jpg", "Salem - Stephen King", 79m, 200 },
                    { 2, 1, "O romance gótico Drácula (1897) do autor britânico Bram Stocker, narra a história do Conde Drácula, vilão morto-vivo da Transilvânia, que se tornou típico representante do mito.", "https://i.imgur.com/OSZUZ0M.jpg", "Dracula - Bram Stoker", 89m, 500 },
                    { 3, 1, "Carmilla é uma novela de ficção gótica do escritor irlandês Joseph Sheridan Le Fanu. É narrada por Laura, uma jovem estiriana que conta os dias passados na companhia da misteriosa Carmilla e os eventos estranhos que ocorreram na região após a sua chegada.", "https://i.imgur.com/3j5XAzq.jpg", "Carmilla: A Vampira de Karnstein - Sheridan Le Fanu", 59m, 100 },
                    { 4, 2, "O vestido longo que vem acompanhado de chapéu de bruxa, peruca e vassoura.", "https://i.imgur.com/NoCSSip.jpg", "Vestido de Bruxa", 59m, 100 },
                    { 5, 2, "Roupa de Múmia com o corpo totalmente enfaixado", "https://i.imgur.com/y6Afq8p.jpg", "Roupa Múmia", 59m, 100 },
                    { 6, 2, "Roupa do temido palhaço Pennywise do filme 'It, a coisa'.", "https://i.imgur.com/y6Afq8p.jpg", "Pennywise", 59m, 100 },
                    { 7, 3, "Arte do Nosferatu, o vampiro está em um cemitério e há morcegos voando no ambiente.", "https://i.imgur.com/L1gbfcI.jpg", "Nosferatu", 119m, 20 },
                    { 8, 3, "Poster do filme clássico do Frankenstein.", "https://i.imgur.com/yHzHNEN.jpg", "Frankenstein", 99m, 25 },
                    { 9, 3, "arte promocional o filme O Vampiro.", "https://i.imgur.com/7Tamzx5.jpg", "O Vampiro", 79m, 34 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
