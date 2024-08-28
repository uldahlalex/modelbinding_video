using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace videoprep.Models;

[Table("patients")]
public partial class Patient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("Patient")]
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}
