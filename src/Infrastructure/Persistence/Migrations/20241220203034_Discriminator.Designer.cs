﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ExamDbContext))]
    [Migration("20241220203034_Discriminator")]
    partial class Discriminator
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("user_type");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.Users.Admin", b =>
                {
                    b.HasBaseType("Domain.Models.Users.User");

                    b.ToTable("users", (string)null);

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Domain.Models.Users.Guest", b =>
                {
                    b.HasBaseType("Domain.Models.Users.User");

                    b.ToTable("users", (string)null);

                    b.HasDiscriminator().HasValue("Guest");
                });

            modelBuilder.Entity("Domain.Models.Users.User", b =>
                {
                    b.HasOne("Domain.Models.Roles.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Users_Roles_RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Models.Roles.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}