using DarkStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DarkStore.Api.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User>  Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(data: new Category
        {
            Id = 1,
            Name = "Livros"
        });
        modelBuilder.Entity<Category>().HasData(data: new Category
        {
            Id = 2,
            Name = "Fantasias"
        });
        modelBuilder.Entity<Category>().HasData(data: new Category
        {
            Id = 3,
            Name = "Quadros e Pôsteres"
        });

        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 1,
            Name = "Salem - Stephen King", 
            Description = "Salem é um clássico do gênero de horror e apresenta os elementos característicos das obras de Stephen King, como personagens complexos, tensão psicológica e uma atmosfera sombria. É considerado um dos melhores romances de vampiro já escritos e consolidou o lugar de King como um mestre do terror moderno.",
            ImageUrl = "https://i.imgur.com/marl3ow.jpg",
            Price = 79,
            Quantity = 200,
            CategoryId = 1
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 2,
            Name = "Dracula - Bram Stoker", 
            Description = "O romance gótico Drácula (1897) do autor britânico Bram Stocker, narra a história do Conde Drácula, vilão morto-vivo da Transilvânia, que se tornou típico representante do mito.",
            ImageUrl = "https://i.imgur.com/OSZUZ0M.jpg",
            Price = 89,
            Quantity = 500,
            CategoryId = 1
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 3,
            Name = "Carmilla: A Vampira de Karnstein - Sheridan Le Fanu", 
            Description = "Carmilla é uma novela de ficção gótica do escritor irlandês Joseph Sheridan Le Fanu. É narrada por Laura, uma jovem estiriana que conta os dias passados na companhia da misteriosa Carmilla e os eventos estranhos que ocorreram na região após a sua chegada.",
            ImageUrl = "https://i.imgur.com/3j5XAzq.jpg",
            Price = 59,
            Quantity = 100,
            CategoryId = 1
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 4,
            Name = "Vestido de Bruxa", 
            Description = "O vestido longo que vem acompanhado de chapéu de bruxa, peruca e vassoura.",
            ImageUrl = "https://i.imgur.com/NoCSSip.jpg",
            Price = 59,
            Quantity = 100,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 5,
            Name = "Roupa Múmia", 
            Description = "Roupa de Múmia com o corpo totalmente enfaixado",
            ImageUrl = "https://i.imgur.com/y6Afq8p.jpg",
            Price = 59,
            Quantity = 100,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 6,
            Name = "Pennywise", 
            Description = "Roupa do temido palhaço Pennywise do filme 'It, a coisa'.",
            ImageUrl = "https://i.imgur.com/y6Afq8p.jpg",
            Price = 59,
            Quantity = 100,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 7,
            Name = "Nosferatu", 
            Description = "Arte do Nosferatu, o vampiro está em um cemitério e há morcegos voando no ambiente.",
            ImageUrl = "https://i.imgur.com/L1gbfcI.jpg",
            Price = 119,
            Quantity = 20,
            CategoryId = 3
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 8,
            Name = "Frankenstein", 
            Description = "Poster do filme clássico do Frankenstein.",
            ImageUrl = "https://i.imgur.com/yHzHNEN.jpg",
            Price = 99,
            Quantity = 25,
            CategoryId = 3
        });
        modelBuilder.Entity<Product>().HasData(data: new Product
        {
            Id = 9,
            Name = "O Vampiro", 
            Description = "arte promocional o filme O Vampiro.",
            ImageUrl = "https://i.imgur.com/7Tamzx5.jpg",
            Price = 79,
            Quantity = 34,
            CategoryId = 3
        });
        
        modelBuilder.Entity<User>().HasData(data: new User
        {
            Id = 1,
            UserName = "Raquel Silva"
        });
        modelBuilder.Entity<User>().HasData(data: new User
        {
            Id = 2,
            UserName = "Rafael Silva"
        });
        modelBuilder.Entity<User>().HasData(data: new User
        {
            Id = 3,
            UserName = "Samuel Silva"
        });
        modelBuilder.Entity<User>().HasData(data: new User
        {
            Id = 4,
            UserName = "Ana Silva"
        });
    }
}