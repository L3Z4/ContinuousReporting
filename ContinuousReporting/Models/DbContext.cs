using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ContinuousReporting.Models
{
    public class ReportingContext : DbContext
    {
        public ReportingContext()
            : base("ReportingDb")
        {
            var initializing = new CreateDatabaseIfNotExists<ReportingContext>();
            initializing.InitializeDatabase(this);
        }

        public IDbSet<BuildEntity> Builds { get; set; }
        public IDbSet<ChallengeEntity> Challenges { get; set; }

        public IDbSet<ProjectEntity> Projects { get; set; }
    }

    public class ProjectEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Blocks { get; set; }
    }

    public class BuildEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string BuildName { get; set; }
        [Required]
        public string Definition { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string User { get; set; }
        public virtual ICollection<CoverageEntity> CoverageCollection { get; set; }
    }

    public class CoverageEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BlocksCovered { get; set; }
        [Required]
        public int BlocksNotCovered { get; set; }
        [Required]
        public int ComputedAverage { get; set; }

        public int BuildID { get; set; }
        public virtual BuildEntity Build { get; set; }
    }

    public class ChallengeEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public double Points { get; set; }
        public virtual ICollection<ModuleChallengeCoverage> ModuleCoverages { get; set; }
        public virtual BuildEntity Build { get; set; }
    }

    public class ModuleChallengeCoverage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Module { get; set; }
        [Required]
        public double DeltaCoverage { get; set; }
        public int Blocks { get; set; }
    }
}