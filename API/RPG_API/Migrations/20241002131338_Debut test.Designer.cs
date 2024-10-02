﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPG_API.Data.Context;

#nullable disable

namespace RPG_API.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20241002131338_Debut test")]
    partial class Debuttest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CharacterItem", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.HasKey("CharactersId", "InventoryId");

                    b.HasIndex("InventoryId");

                    b.ToTable("CharacterItem");
                });

            modelBuilder.Entity("CharacterQuest", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.Property<int>("QuestsId")
                        .HasColumnType("int");

                    b.HasKey("CharactersId", "QuestsId");

                    b.HasIndex("QuestsId");

                    b.ToTable("CharacterQuest");
                });

            modelBuilder.Entity("MonsterQuest", b =>
                {
                    b.Property<int>("MonsterId")
                        .HasColumnType("int");

                    b.Property<int>("QuestId")
                        .HasColumnType("int");

                    b.HasKey("MonsterId", "QuestId");

                    b.HasIndex("QuestId");

                    b.ToTable("MonsterQuest");
                });

            modelBuilder.Entity("RPG_API.Models.Base.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("BoostAttack")
                        .HasColumnType("double");

                    b.Property<double>("BoostDefence")
                        .HasColumnType("double");

                    b.Property<int>("HealthRestoration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("RPG_API.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Armor")
                        .HasColumnType("int");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Lives")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Xp")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("RPG_API.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("BoostAttack")
                        .HasColumnType("double");

                    b.Property<double>("BoostDefence")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("RPG_API.Models.JonctionItemCharacter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ItemId");

                    b.ToTable("JonctionItemCharacter");
                });

            modelBuilder.Entity("RPG_API.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Map");
                });

            modelBuilder.Entity("RPG_API.Models.Monster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Armor")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("XpGiven")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("Monster");
                });

            modelBuilder.Entity("RPG_API.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Reward")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Quest");
                });

            modelBuilder.Entity("RPG_API.Models.Tile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("Tile");
                });

            modelBuilder.Entity("CharacterItem", b =>
                {
                    b.HasOne("RPG_API.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPG_API.Models.Base.Item", null)
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterQuest", b =>
                {
                    b.HasOne("RPG_API.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPG_API.Models.Quest", null)
                        .WithMany()
                        .HasForeignKey("QuestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MonsterQuest", b =>
                {
                    b.HasOne("RPG_API.Models.Monster", null)
                        .WithMany()
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPG_API.Models.Quest", null)
                        .WithMany()
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG_API.Models.Character", b =>
                {
                    b.HasOne("RPG_API.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("RPG_API.Models.JonctionItemCharacter", b =>
                {
                    b.HasOne("RPG_API.Models.Character", null)
                        .WithMany("Equipment")
                        .HasForeignKey("CharacterId");

                    b.HasOne("RPG_API.Models.Base.Item", null)
                        .WithMany("Equipment")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("RPG_API.Models.Map", b =>
                {
                    b.HasOne("RPG_API.Models.Character", "Character")
                        .WithOne("Map")
                        .HasForeignKey("RPG_API.Models.Map", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("RPG_API.Models.Monster", b =>
                {
                    b.HasOne("RPG_API.Models.Map", "Map")
                        .WithMany("Monster")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");
                });

            modelBuilder.Entity("RPG_API.Models.Quest", b =>
                {
                    b.HasOne("RPG_API.Models.Base.Item", "item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.Navigation("item");
                });

            modelBuilder.Entity("RPG_API.Models.Tile", b =>
                {
                    b.HasOne("RPG_API.Models.Map", "Map")
                        .WithMany("Coordinates")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");
                });

            modelBuilder.Entity("RPG_API.Models.Base.Item", b =>
                {
                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("RPG_API.Models.Character", b =>
                {
                    b.Navigation("Equipment");

                    b.Navigation("Map");
                });

            modelBuilder.Entity("RPG_API.Models.Map", b =>
                {
                    b.Navigation("Coordinates");

                    b.Navigation("Monster");
                });
#pragma warning restore 612, 618
        }
    }
}
