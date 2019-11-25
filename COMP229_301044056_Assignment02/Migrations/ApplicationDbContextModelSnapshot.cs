﻿// <auto-generated />
using System;
using COMP229_301044056_Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace COMP229_301044056_Assignment02.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IngredientName");

                    b.HasKey("IngredientID");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.IngredientLine", b =>
                {
                    b.Property<int>("IngredientLineID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredientID");

                    b.Property<int>("MeasureID");

                    b.Property<int>("Quantity");

                    b.Property<int>("RecipeID");

                    b.HasKey("IngredientLineID");

                    b.HasIndex("RecipeID");

                    b.ToTable("IngredientLine");
                });

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.Measure", b =>
                {
                    b.Property<int>("MeasureID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MeasureDesc");

                    b.HasKey("MeasureID");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Cuisine");

                    b.Property<string>("Date");

                    b.Property<string>("Instructions");

                    b.Property<string>("Name");

                    b.Property<string>("Photo");

                    b.Property<string>("UserId");

                    b.HasKey("RecipeID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.RecipeComment", b =>
                {
                    b.Property<int>("RecipeCommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CommentDateTime");

                    b.Property<string>("Comments");

                    b.Property<int>("RecipeID");

                    b.Property<string>("UserId");

                    b.HasKey("RecipeCommentId");

                    b.ToTable("RecipeComments");
                });

            modelBuilder.Entity("COMP229_301044056_Assignment02.Models.IngredientLine", b =>
                {
                    b.HasOne("COMP229_301044056_Assignment02.Models.Recipe")
                        .WithMany("Lines")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
