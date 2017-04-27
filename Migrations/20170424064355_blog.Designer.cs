//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using AngularDemo.Domain;

//namespace AngularDemo.Migrations
//{
//    [DbContext(typeof(BlogContext))]
//    [Migration("20170424064355_blog")]
//    partial class blog
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//            modelBuilder
//                .HasAnnotation("ProductVersion", "1.1.1")
//                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

//            modelBuilder.Entity("AngularDemo.Models.Blog", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<int>("CategoryId");

//                    b.Property<string>("Content")
//                        .IsRequired();

//                    b.Property<DateTime>("CreatedOn");

//                    b.Property<string>("Title")
//                        .IsRequired();

//                    b.HasKey("Id");

//                    b.HasIndex("CategoryId");

//                    b.ToTable("Blog");
//                });

//            modelBuilder.Entity("AngularDemo.Models.BlogCategory", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("Name")
//                        .IsRequired();

//                    b.HasKey("Id");

//                    b.ToTable("BlogCategory");
//                });

//            modelBuilder.Entity("AngularDemo.Models.Comment", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<int?>("BlogId");

//                    b.Property<string>("Content");

//                    b.HasKey("Id");

//                    b.HasIndex("BlogId");

//                    b.ToTable("Comment");
//                });

//            modelBuilder.Entity("AngularDemo.Models.Blog", b =>
//                {
//                    b.HasOne("AngularDemo.Models.BlogCategory", "Category")
//                        .WithMany()
//                        .HasForeignKey("CategoryId")
//                        .OnDelete(DeleteBehavior.Cascade);
//                });

//            modelBuilder.Entity("AngularDemo.Models.Comment", b =>
//                {
//                    b.HasOne("AngularDemo.Models.Blog")
//                        .WithMany("Comments")
//                        .HasForeignKey("BlogId");
//                });
//        }
//    }
//}
