﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using book_collection.Context;

#nullable disable

namespace book_collection.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220722182248_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("book_collection.Models.Author", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PublishingCompanieid")
                        .HasColumnType("int")
                        .HasColumnName("publishing_companies_id");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.HasIndex("PublishingCompanieid");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("book_collection.Models.Book", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PublishingCompanieId")
                        .HasColumnType("int")
                        .HasColumnName("publishing_companies_id");

                    b.Property<string>("edition")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime>("release_date")
                        .HasMaxLength(80)
                        .HasColumnType("datetime");

                    b.HasKey("id");

                    b.HasIndex("PublishingCompanieId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("book_collection.Models.BookAuthor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.HasKey("id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("books_authors");
                });

            modelBuilder.Entity("book_collection.Models.ImageAuthor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<byte[]>("image")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.HasIndex("AuthorId");

                    b.ToTable("image_author");
                });

            modelBuilder.Entity("book_collection.Models.ImageBook", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("author_id")
                        .HasColumnType("int");

                    b.Property<int>("authorid")
                        .HasColumnType("int");

                    b.Property<byte[]>("image_byte")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.HasIndex("authorid");

                    b.ToTable("image_book");
                });

            modelBuilder.Entity("book_collection.Models.ImageProfile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnName("profile_id");

                    b.Property<byte[]>("image_byte")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.HasIndex("ProfileId");

                    b.ToTable("images_profile");
                });

            modelBuilder.Entity("book_collection.Models.Profile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("birth_date")
                        .HasColumnType("datetime");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("book_collection.Models.PublishingCompanie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnName("profile_id");

                    b.Property<string>("adress")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("cep")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("cnpj")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("id");

                    b.HasIndex("ProfileId");

                    b.ToTable("publishing_companies");
                });

            modelBuilder.Entity("book_collection.Models.Author", b =>
                {
                    b.HasOne("book_collection.Models.PublishingCompanie", null)
                        .WithMany("Authors")
                        .HasForeignKey("PublishingCompanieid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("book_collection.Models.Book", b =>
                {
                    b.HasOne("book_collection.Models.PublishingCompanie", null)
                        .WithMany("Books")
                        .HasForeignKey("PublishingCompanieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("book_collection.Models.BookAuthor", b =>
                {
                    b.HasOne("book_collection.Models.Author", "author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("book_collection.Models.Book", "book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("book");
                });

            modelBuilder.Entity("book_collection.Models.ImageAuthor", b =>
                {
                    b.HasOne("book_collection.Models.Author", null)
                        .WithMany("ImageAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("book_collection.Models.ImageBook", b =>
                {
                    b.HasOne("book_collection.Models.Author", "author")
                        .WithMany()
                        .HasForeignKey("authorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");
                });

            modelBuilder.Entity("book_collection.Models.ImageProfile", b =>
                {
                    b.HasOne("book_collection.Models.Profile", null)
                        .WithMany("ImageProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("book_collection.Models.PublishingCompanie", b =>
                {
                    b.HasOne("book_collection.Models.Profile", null)
                        .WithMany("PublishingCompanies")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("book_collection.Models.Author", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("ImageAuthors");
                });

            modelBuilder.Entity("book_collection.Models.Book", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("book_collection.Models.Profile", b =>
                {
                    b.Navigation("ImageProfiles");

                    b.Navigation("PublishingCompanies");
                });

            modelBuilder.Entity("book_collection.Models.PublishingCompanie", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
