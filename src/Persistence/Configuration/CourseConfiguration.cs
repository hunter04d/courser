using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configuration
{
    /// <summary>
    /// Persistence configuration of Course
    /// </summary>
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.DayOfWeek).HasConversion(new EnumToStringConverter<DayOfWeek>());
            builder.OwnsOne(c => c.StartTime);
            builder.OwnsOne(c => c.EndTime);
        }
    }
}
