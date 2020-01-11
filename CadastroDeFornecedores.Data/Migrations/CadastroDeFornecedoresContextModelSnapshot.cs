﻿// <auto-generated />
using System;
using CadastroDeFornecedores.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CadastroDeFornecedores.Data.Migrations
{
    [DbContext(typeof(CadastroDeFornecedoresContext))]
    partial class CadastroDeFornecedoresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CadastroDeFornecedores.Domain.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CNPJ")
                        .HasColumnType("int")
                        .HasMaxLength(14);

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UF")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("CadastroDeFornecedores.Domain.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CPFouCNPJ")
                        .HasColumnType("int")
                        .HasMaxLength(14);

                    b.Property<DateTime?>("DataAniversarioPF")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegistroGeralPF")
                        .HasColumnType("int")
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("CadastroDeFornecedores.Domain.Models.FornecedorTelefones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("FornecedoresTelefones");
                });

            modelBuilder.Entity("CadastroDeFornecedores.Domain.Models.Fornecedor", b =>
                {
                    b.HasOne("CadastroDeFornecedores.Domain.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CadastroDeFornecedores.Domain.Models.FornecedorTelefones", b =>
                {
                    b.HasOne("CadastroDeFornecedores.Domain.Models.Fornecedor", "Fornecedor")
                        .WithMany("Telefones")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
