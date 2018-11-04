﻿// <auto-generated />
using System;
using MembershipWebApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MembershipWebApp.Domain.Migrations
{
    [DbContext(typeof(MembershipContext))]
    [Migration("20181103023357_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MembershipWebApp.Domain.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountStatus");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("ID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MembershipWebApp.Domain.MemberAddress", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<int>("MemberID");

                    b.Property<string>("PostCode");

                    b.Property<string>("State");

                    b.Property<string>("Suburb");

                    b.HasKey("ID");

                    b.HasIndex("MemberID")
                        .IsUnique();

                    b.ToTable("MemberAddresses");
                });

            modelBuilder.Entity("MembershipWebApp.Domain.MemberDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateJoined");

                    b.Property<string>("DaysOfWeekAttend");

                    b.Property<decimal>("MemberFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MemberID");

                    b.Property<string>("MemberLevel");

                    b.Property<bool>("ReceiveNewsLetter");

                    b.HasKey("ID");

                    b.HasIndex("MemberID")
                        .IsUnique();

                    b.ToTable("MemberDetails");
                });

            modelBuilder.Entity("MembershipWebApp.Domain.MemberAddress", b =>
                {
                    b.HasOne("MembershipWebApp.Domain.Member", "Member")
                        .WithOne("MemberAddress")
                        .HasForeignKey("MembershipWebApp.Domain.MemberAddress", "MemberID")
                        .HasConstraintName("ForeignKey_MemberAddress_Member")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MembershipWebApp.Domain.MemberDetails", b =>
                {
                    b.HasOne("MembershipWebApp.Domain.Member", "Member")
                        .WithOne("MemberDetails")
                        .HasForeignKey("MembershipWebApp.Domain.MemberDetails", "MemberID")
                        .HasConstraintName("ForeignKey_MemberDetails_Member")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
