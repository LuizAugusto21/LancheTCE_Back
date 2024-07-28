﻿// <auto-generated />
using System;
using LancheTCE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LancheTCE.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LancheTCE_Back.models.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EnderecoId"));

                    b.Property<string>("Andar")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Departamento")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Sala")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("EnderecoId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PedidoId"));

                    b.Property<int>("IdUsuarioCliente")
                        .HasColumnType("integer");

                    b.Property<int>("IdUsuarioVendedor")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("PedidoId");

                    b.HasIndex("IdUsuarioCliente");

                    b.HasIndex("IdUsuarioVendedor");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProdutoId"));

                    b.Property<string>("Categoria")
                        .HasColumnType("text");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("IdUsuarioVendedor")
                        .HasColumnType("integer");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("ProdutoId");

                    b.HasIndex("IdUsuarioVendedor");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("LancheTCE_Back.models.ProdutoPedido", b =>
                {
                    b.Property<int>("Pedido_ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Pedido_ProdutoId"));

                    b.Property<int>("IdPedido")
                        .HasColumnType("integer");

                    b.Property<int>("IdProduto")
                        .HasColumnType("integer");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("Pedido_ProdutoId");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProduto");

                    b.ToTable("Pedidos_Produtos");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Contato")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("IdEndereco")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Perfil")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("IdEndereco");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Pedido", b =>
                {
                    b.HasOne("LancheTCE_Back.models.Usuario", "UsuarioCliente")
                        .WithMany("PedidosComoCliente")
                        .HasForeignKey("IdUsuarioCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LancheTCE_Back.models.Usuario", "UsuarioVendedor")
                        .WithMany("PedidosComoVendedor")
                        .HasForeignKey("IdUsuarioVendedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioCliente");

                    b.Navigation("UsuarioVendedor");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Produto", b =>
                {
                    b.HasOne("LancheTCE_Back.models.Usuario", "UsuarioVendedor")
                        .WithMany("ProdutosVendidos")
                        .HasForeignKey("IdUsuarioVendedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioVendedor");
                });

            modelBuilder.Entity("LancheTCE_Back.models.ProdutoPedido", b =>
                {
                    b.HasOne("LancheTCE_Back.models.Pedido", "Pedido")
                        .WithMany("PedidosProdutos")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LancheTCE_Back.models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Usuario", b =>
                {
                    b.HasOne("LancheTCE_Back.models.Endereco", "Endereco")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Endereco", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Pedido", b =>
                {
                    b.Navigation("PedidosProdutos");
                });

            modelBuilder.Entity("LancheTCE_Back.models.Usuario", b =>
                {
                    b.Navigation("PedidosComoCliente");

                    b.Navigation("PedidosComoVendedor");

                    b.Navigation("ProdutosVendidos");
                });
#pragma warning restore 612, 618
        }
    }
}
