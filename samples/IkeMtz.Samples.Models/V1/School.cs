using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IkeMtz.Samples.Models.V1
{
  // Generated by the SQL POCO Class Generator Script
  // Script is available at:
  // https://raw.githubusercontent.com/ikemtz/NRSRx/master/tools/sql-poco-class-generator.sql

  public partial class School
  : IkeMtz.NRSRx.Core.Models.IIdentifiable, IkeMtz.NRSRx.Core.Models.IAuditable
  {
    public School()
    {
      SchoolCourses = new HashSet<SchoolCourse>();
      StudentSchools = new HashSet<StudentSchool>();
    }

    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(250)]
    public string FullName { get; set; }
    [Required]
    [MaxLength(5)]
    public string TenantId { get; set; }
    [Required]
    [MaxLength(250)]
    public string CreatedBy { get; set; }
    [Required]
    public DateTimeOffset CreatedOnUtc { get; set; }
    [MaxLength(250)]
    public string UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
    public virtual ICollection<SchoolCourse> SchoolCourses { get; }
    public virtual ICollection<StudentSchool> StudentSchools { get; }
  }

}
