﻿// <auto-generated />
using System;
using InclementEmeraldTeamBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InclamentEmeraldTeamBuilder.Migrations
{
    [DbContext(typeof(PokeDbContext))]
    partial class PokeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MMoveToMoveFlag", b =>
                {
                    b.Property<int>("MMoveToMoveFlagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoveFlagId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoveId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MMoveToMoveFlagId");

                    b.HasIndex("MoveFlagId");

                    b.HasIndex("MoveId");

                    b.ToTable("MMoveToMoveFlags");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveEffect", b =>
                {
                    b.Property<int>("MoveEffectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveEffectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MoveEffectSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MoveEffectId");

                    b.ToTable("MoveEffects");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveFlag", b =>
                {
                    b.Property<int>("MoveFlagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveFlagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MoveFlagSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("MoveId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MoveFlagId");

                    b.HasIndex("MoveId");

                    b.ToTable("MoveFlags");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveStatus", b =>
                {
                    b.Property<int>("MoveStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveStatusName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MoveStatusSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MoveStatusId");

                    b.ToTable("MoveStatuses");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveTarget", b =>
                {
                    b.Property<int>("MoveTargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveTargetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MoveTargetSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MoveTargetId");

                    b.ToTable("MoveTargets");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.PokeType", b =>
                {
                    b.Property<int>("PokeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PokeTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PokeTypeSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PokeTypeId");

                    b.ToTable("PokeTypes");
                });

            modelBuilder.Entity("InclementEmeraldTeamBuilder.Components.Classes.Ability", b =>
                {
                    b.Property<int>("AbilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AbilityDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AbilityName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AbilitySourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AbilityId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("InclementEmeraldTeamBuilder.Components.Classes.Move", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Accuracy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MoveEffectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MoveSourceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MoveStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoveTargetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokeTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecondaryEffectChance")
                        .HasColumnType("INTEGER");

                    b.HasKey("MoveId");

                    b.HasIndex("MoveEffectId");

                    b.HasIndex("MoveStatusId");

                    b.HasIndex("MoveTargetId");

                    b.HasIndex("PokeTypeId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MMoveToMoveFlag", b =>
                {
                    b.HasOne("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveFlag", "MoveFlag")
                        .WithMany()
                        .HasForeignKey("MoveFlagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InclementEmeraldTeamBuilder.Components.Classes.Move", "Move")
                        .WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Move");

                    b.Navigation("MoveFlag");
                });

            modelBuilder.Entity("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveFlag", b =>
                {
                    b.HasOne("InclementEmeraldTeamBuilder.Components.Classes.Move", null)
                        .WithMany("MoveFlags")
                        .HasForeignKey("MoveId");
                });

            modelBuilder.Entity("InclementEmeraldTeamBuilder.Components.Classes.Move", b =>
                {
                    b.HasOne("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveEffect", "MoveEffect")
                        .WithMany()
                        .HasForeignKey("MoveEffectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveStatus", "MoveStatus")
                        .WithMany()
                        .HasForeignKey("MoveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo.MoveTarget", "MoveTarget")
                        .WithMany()
                        .HasForeignKey("MoveTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InclamentEmeraldTeamBuilder.Components.Classes.PokeType", "PokeType")
                        .WithMany()
                        .HasForeignKey("PokeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MoveEffect");

                    b.Navigation("MoveStatus");

                    b.Navigation("MoveTarget");

                    b.Navigation("PokeType");
                });

            modelBuilder.Entity("InclementEmeraldTeamBuilder.Components.Classes.Move", b =>
                {
                    b.Navigation("MoveFlags");
                });
#pragma warning restore 612, 618
        }
    }
}
