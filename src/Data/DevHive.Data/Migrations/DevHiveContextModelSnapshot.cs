﻿// <auto-generated />
using System;
using DevHive.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DevHive.Data.Migrations
{
    [DbContext(typeof(DevHiveContext))]
    partial class DevHiveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DevHive.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DevHive.Data.Models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("DevHive.Data.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DevHive.Data.Models.ProfilePicture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PictureURL")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProfilePicture");
                });

            modelBuilder.Entity("DevHive.Data.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("DevHive.Data.Models.Relational.PostAttachments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileUrl")
                        .HasColumnType("text");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostAttachments");
                });

            modelBuilder.Entity("DevHive.Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DevHive.Data.Models.Technology", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("DevHive.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("LanguageUser", b =>
                {
                    b.Property<Guid>("LanguagesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("LanguagesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LanguageUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("TechnologyUser", b =>
                {
                    b.Property<Guid>("TechnologiesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("TechnologiesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TechnologyUser");
                });

            modelBuilder.Entity("DevHive.Data.Models.Comment", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("CreatorId");

                    b.HasOne("DevHive.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.Navigation("Creator");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DevHive.Data.Models.Post", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DevHive.Data.Models.ProfilePicture", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", "User")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("DevHive.Data.Models.ProfilePicture", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DevHive.Data.Models.Rating", b =>
                {
                    b.HasOne("DevHive.Data.Models.Post", "Post")
                        .WithMany("Ratings")
                        .HasForeignKey("PostId");

                    b.HasOne("DevHive.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DevHive.Data.Models.Relational.PostAttachments", b =>
                {
                    b.HasOne("DevHive.Data.Models.Post", "Post")
                        .WithMany("Attachments")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DevHive.Data.Models.User", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LanguageUser", b =>
                {
                    b.HasOne("DevHive.Data.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("DevHive.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("DevHive.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("DevHive.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TechnologyUser", b =>
                {
                    b.HasOne("DevHive.Data.Models.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHive.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevHive.Data.Models.Post", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("DevHive.Data.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Friends");

                    b.Navigation("Posts");

                    b.Navigation("ProfilePicture");
                });
#pragma warning restore 612, 618
        }
    }
}
