using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FirstProject.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<DepartmentAndEmployee> DepartmentAndEmployees { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Navbar> Navbars { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Patienthistory> Patienthistories { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User146; PASSWORD=Mahmoud!185;DATA SOURCE= 94.56.229.181:3488/traindb ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER146")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutid)
                    .HasName("SYS_C00198335");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTID");

                entity.Property(e => e.Numberofsurgery)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFSURGERY");

                entity.Property(e => e.Ourgoal)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OURGOAL");

                entity.Property(e => e.Ourmission)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OURMISSION");

                entity.Property(e => e.Patientperyear)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTPERYEAR");

                entity.Property(e => e.Qualifieddoctor)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUALIFIEDDOCTOR");

                entity.Property(e => e.Whatwedo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WHATWEDO");

                entity.Property(e => e.Yearofexperience)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("YEAROFEXPERIENCE");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENT");

                entity.Property(e => e.Appointmentid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("APPOINTMENTID");

                entity.Property(e => e.Appointmenttime)
                    .HasPrecision(6)
                    .HasColumnName("APPOINTMENTTIME");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EMPID");

                entity.Property(e => e.Healthcase)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HEALTHCASE");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("FK_EMPID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("FK_PATIENTID");
            });
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Attenid)
                    .HasName("SYS_C00201241");

                entity.ToTable("ATTENDANCE");

                entity.Property(e => e.Attenid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ATTENID");

                entity.Property(e => e.Checkin)
                    .HasPrecision(6)
                    .HasColumnName("CHECKIN");

                entity.Property(e => e.Checkout)
                    .HasPrecision(6)
                    .HasColumnName("CHECKOUT");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EMPID");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("FK_EMPID_ATTEN");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Billid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BILLID");

                entity.Property(e => e.Appointmentid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("APPOINTMENTID");

                entity.Property(e => e.Billtime)
                    .HasPrecision(6)
                    .HasColumnName("BILLTIME");

                entity.Property(e => e.Cost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COST");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.Paymentid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PAYMENTID");

                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Subid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SUBID");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Appointmentid)
                    .HasConstraintName("FK_APPOINTMENTID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Paymentid)
                    .HasConstraintName("FK_PAYMENTID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("SYS_C00201267");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Subid)
                    .HasConstraintName("SYS_C00200156");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("CLINIC");

                entity.Property(e => e.Clinicid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CLINICID");

                entity.Property(e => e.Clincname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CLINCNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.ClinicImg)
                    .HasMaxLength(1250)
                    .IsUnicode(false)
                    .HasColumnName("CLINICIMG");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Contactid)
                    .HasName("SYS_C00198337");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Locationmap)
                    .HasMaxLength(3900)
                    .IsUnicode(false)
                    .HasColumnName("LOCATIONMAP");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<DepartmentAndEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEPARTMENT_AND_EMPLOYEE");

                entity.Property(e => e.Depname)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("DEPNAME");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EMPID");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Salary)
                    .HasColumnType("FLOAT")
                    .HasColumnName("SALARY");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("SYS_C00197547");

                entity.ToTable("EMPLOYEE");

                entity.HasIndex(e => e.Email, "SYS_C00197548")
                    .IsUnique();

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMPID");

                entity.Property(e => e.Abouthim)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ABOUTHIM");

                entity.Property(e => e.Clinicid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CLINICID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Hiredate)
                    .HasColumnType("DATE")
                    .HasColumnName("HIREDATE");

                entity.Property(e => e.Imgpath)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMGPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Salary)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALARY");

                entity.Property(e => e.Specialist)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("SPECIALIST");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Clinicid)
                    .HasConstraintName("FK_CLINICID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("FK_ROLEID");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("ENTITY");

                entity.Property(e => e.Entityid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ENTITYID");

                entity.Property(e => e.Href)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HREF");

                entity.Property(e => e.Navbarid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("NAVBARID");

                entity.HasOne(d => d.Navbar)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.Navbarid)
                    .HasConstraintName("FK_NAVBARID");
            });

            modelBuilder.Entity<Footer>(entity =>
            {
                entity.ToTable("FOOTER");

                entity.Property(e => e.Footerid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FOOTERID");

                entity.Property(e => e.Facebooklink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOKLINK");

                entity.Property(e => e.Instagramlink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAMLINK");

                entity.Property(e => e.Textabout)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TEXTABOUT");

                entity.Property(e => e.Twitterlink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TWITTERLINK");

                entity.Property(e => e.Youtubelink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("YOUTUBELINK");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Imgslider)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMGSLIDER");

                entity.Property(e => e.Logo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LOGO");

                entity.Property(e => e.Openingday)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPENINGDAY");

                entity.Property(e => e.Openinghour)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPENINGHOUR");

                entity.Property(e => e.Textimg)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TEXTIMG");

                entity.Property(e => e.Websitename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WEBSITENAME");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.HasIndex(e => e.Email, "SYS_C00197590")
                    .IsUnique();

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Passwordd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDD");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("SYS_C00198738")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00198737");
            });

            modelBuilder.Entity<Navbar>(entity =>
            {
                entity.ToTable("NAVBAR");

                entity.Property(e => e.Navbarid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NAVBARID");

                entity.Property(e => e.Entityname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ENTITYNAME");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("PATIENT");

                entity.HasIndex(e => e.Email, "SYS_C00197559")
                    .IsUnique();

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PATIENTID");
                  

                entity.Property(e => e.Age)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AGE");

                entity.Property(e => e.Imgpath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMGPATH");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Lname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Disease)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISEASE");
            });

            modelBuilder.Entity<Patienthistory>(entity =>
            {
                entity.ToTable("PATIENTHISTORY");

                entity.Property(e => e.Patienthistoryid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PATIENTHISTORYID");

                entity.Property(e => e.ChronicDiseases)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CHRONIC_DISEASES");

                entity.Property(e => e.MainComplaint)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MAIN_COMPLAINT");

                entity.Property(e => e.MedicinesOnRegularBasis)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MEDICINES_ON_REGULAR_BASIS");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.PreviousExaminations)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PREVIOUS_EXAMINATIONS");

                entity.Property(e => e.PreviousVisitsToTheClinicForTheSameReason)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PREVIOUS_VISITS_TO_THE_CLINIC_FOR_THE_SAME_REASON");

                entity.Property(e => e.SensitivityToAnything)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SENSITIVITY_TO_ANYTHING");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Patienthistories)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("FK_PATIENT_ID_HIS");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENT");

                entity.HasIndex(e => e.Paymentnumber, "SYS_C00197570")
                    .IsUnique();

                entity.Property(e => e.Paymentid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYMENTID");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CVV");

                entity.Property(e => e.Mm)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("MM");

                entity.Property(e => e.Money)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("MONEY");

                entity.Property(e => e.Paymentnumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PAYMENTNUMBER");

                entity.Property(e => e.Placeholder)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PLACEHOLDER");

                entity.Property(e => e.Yy)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("YY");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Revid)
                    .HasName("SYS_C00197586");

                entity.ToTable("REVIEWS");

                entity.Property(e => e.Revid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVID");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.Reviewcontent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REVIEWCONTENT");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("FK_PATIENT_ID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICES");

                entity.Property(e => e.Serviceid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SERVICEID");

                entity.Property(e => e.Aboutservice)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ABOUTSERVICE");

                entity.Property(e => e.Servicename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SERVICENAME");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Subid)
                    .HasName("SYS_C00200155");

                entity.ToTable("SUBSCRIPTION");

                entity.Property(e => e.Subid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SUBID");

                entity.Property(e => e.Cost)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("COST");

                entity.Property(e => e.Service1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE1");

                entity.Property(e => e.Service2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE2");

                entity.Property(e => e.Service3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE3");

                entity.Property(e => e.Service4)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE4");

                entity.Property(e => e.Service5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SERVICE5");

                entity.Property(e => e.SubType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUB_TYPE");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(e => e.Testytid)
                    .HasName("SYS_C00197583");

                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Testytid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTYTID");

                entity.Property(e => e.Patientid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PATIENTID");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Testimonialcontent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMONIALCONTENT");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Patientid)
                    .HasConstraintName("FK_PATIENTIDTESTY");
            });

            modelBuilder.HasSequence("BANK_ACCOUNT_SEQ").IncrementsBy(10);

            modelBuilder.HasSequence("BANK_BRANCH_SEQ");

            modelBuilder.HasSequence("EMP_ID_SEQ");

            modelBuilder.HasSequence("ID_DEP");

            modelBuilder.HasSequence("ID_SEQ");

            modelBuilder.HasSequence("ROLE_ID_SEQ");

            modelBuilder.HasSequence("TRAN_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
