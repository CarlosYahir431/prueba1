﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VelazquezYahir.Context;

#nullable disable

namespace VelazquezYahir.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250312151715_prueba")]
    partial class prueba
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Book", b =>
                {
                    b.Property<int>("PkBook")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkBook"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PkBook");

                    b.HasIndex("Categoria");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Categoria", b =>
                {
                    b.Property<int>("PkCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkCategoria"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PkCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Comentario", b =>
                {
                    b.Property<int>("PkComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkComentario"));

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PkBook")
                        .HasColumnType("int");

                    b.Property<int>("PkUsuario")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioPkUsuario")
                        .HasColumnType("int");

                    b.HasKey("PkComentario");

                    b.HasIndex("PkBook");

                    b.HasIndex("UsuarioPkUsuario");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Role", b =>
                {
                    b.Property<int>("PkRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkRole"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PkRole");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            PkRole = 1,
                            Nombre = "Admin"
                        },
                        new
                        {
                            PkRole = 2,
                            Nombre = "Usuario"
                        });
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Usuario", b =>
                {
                    b.Property<int>("PkUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkUsuario"));

                    b.Property<int>("FkRole")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PkUsuario");

                    b.HasIndex("FkRole");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            PkUsuario = 1,
                            FkRole = 1,
                            Nombre = "Carlos",
                            Password = "caritademiel123xd",
                            UserName = "Arrozquemado49"
                        });
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Book", b =>
                {
                    b.HasOne("VelazquezYahir.Models.Domain.Categoria", "Categorias")
                        .WithMany()
                        .HasForeignKey("Categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorias");
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Comentario", b =>
                {
                    b.HasOne("VelazquezYahir.Models.Domain.Book", "Book")
                        .WithMany()
                        .HasForeignKey("PkBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VelazquezYahir.Models.Domain.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioPkUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("VelazquezYahir.Models.Domain.Usuario", b =>
                {
                    b.HasOne("VelazquezYahir.Models.Domain.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("FkRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
