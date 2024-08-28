using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace videoprep.Models;

[Table("diseases")]
public partial class Disease
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("Disease")]
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}
