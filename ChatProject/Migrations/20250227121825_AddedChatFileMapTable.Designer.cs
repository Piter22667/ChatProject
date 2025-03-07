﻿// <auto-generated />
using System;
using ChatProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250227121825_AddedChatFileMapTable")]
    partial class AddedChatFileMapTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatProject.Models.Archived", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Archived");
                });

            modelBuilder.Entity("ChatProject.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsClosed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ChatProject.Models.ChatUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatUsers");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFile", b =>
                {
                    b.Property<Guid>("StreamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("DateTimeOffset")
                        .HasColumnName("creation_time");

                    b.Property<byte[]>("FileStream")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("file_stream");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("file_type")
                        .HasComputedColumnSql("[RIGHT(Name; CHARINDEX('.', RESERVE(Name)) -1\r\n]");

                    b.Property<DateTimeOffset>("LastAccessTime")
                        .HasColumnType("DateTimeOffset")
                        .HasColumnName("last_access_time");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("DateTimeOffset")
                        .HasColumnName("last_write_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("StreamId");

                    b.ToTable("ChatFile");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFileConnections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("FileId");

                    b.ToTable("ChatFileConnections");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFileNameMap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HashedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("OriginalName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("MessageId");

                    b.ToTable("ChatFileNameMap");
                });

            modelBuilder.Entity("ChatProject.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ChatProject.Models.Archived", b =>
                {
                    b.HasOne("ChatProject.Models.Chat", "Chat")
                        .WithMany("ArchivedMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatProject.Models.User", "User")
                        .WithMany("Archived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatProject.Models.Chat", b =>
                {
                    b.HasOne("ChatProject.Models.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatProject.Models.ChatUsers", b =>
                {
                    b.HasOne("ChatProject.Models.Chat", "Chat")
                        .WithMany("ChatUsers")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChatProject.Models.User", "User")
                        .WithMany("ChatUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFileConnections", b =>
                {
                    b.HasOne("ChatProject.Models.Chat", "Chat")
                        .WithMany("ChatFileConnections")
                        .HasForeignKey("ChatId")
                        .IsRequired();

                    b.HasOne("ChatProject.Models.Files.ChatFile", "ChatFile")
                        .WithMany("ChatFileConnections")
                        .HasForeignKey("FileId")
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("ChatFile");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFileNameMap", b =>
                {
                    b.HasOne("ChatProject.Models.Files.ChatFile", "ChatFile")
                        .WithMany("ChatFileNameMap")
                        .HasForeignKey("FileId");

                    b.HasOne("ChatProject.Models.Message", "Message")
                        .WithMany("ChatFileNameMap")
                        .HasForeignKey("MessageId")
                        .IsRequired();

                    b.Navigation("ChatFile");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("ChatProject.Models.Message", b =>
                {
                    b.HasOne("ChatProject.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatProject.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatProject.Models.Chat", b =>
                {
                    b.Navigation("ArchivedMessages");

                    b.Navigation("ChatFileConnections");

                    b.Navigation("ChatUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ChatProject.Models.Files.ChatFile", b =>
                {
                    b.Navigation("ChatFileConnections");

                    b.Navigation("ChatFileNameMap");
                });

            modelBuilder.Entity("ChatProject.Models.Message", b =>
                {
                    b.Navigation("ChatFileNameMap");
                });

            modelBuilder.Entity("ChatProject.Models.User", b =>
                {
                    b.Navigation("Archived");

                    b.Navigation("ChatUsers");

                    b.Navigation("Chats");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
