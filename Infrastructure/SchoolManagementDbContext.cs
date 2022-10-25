using FirstApi_Project.Authentication.AuthModels;
using FirstApi_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FirstApi_Project.Authentication.AuthModels;*/

namespace FirstApi_Project.Infrastructure
{
    public class SchoolManagementDbContext : IdentityDbContext<User>
    {
        private DbSet<User> users;

        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LoginModel>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("LoginModel");
                    x.Property(v => v.Username).HasColumnName("username");
                });

            builder.Entity<LoginUser>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("LoginUser");
                    x.Property(m => m.UserName).HasColumnName("Username");
                });

            builder.Entity<RegisterModel>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("RegisterModel");
                    x.Property(m => m.Username).HasColumnName("Username");
                    /*x.Property(m => m.Password).HasColumnName("Password");*/
                });
            //another way to add the guid for user registration.
            /*  modelBuilder
  .Entity<Blog>()
  .Property(e => e.Id)
  .ValueGeneratedOnAdd();*/
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Arm> Arms { get; set; }
        public DbSet<Book> Bookss { get; set; }
        public DbSet<ClassArm> ClassArms { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<User> Users { get => users; set => users = value; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<RegisterModel> RegisterModel { get; set; }
        /*        public DbSet<UserRoles> UserRoless { get; set; } 
        */
    }
}