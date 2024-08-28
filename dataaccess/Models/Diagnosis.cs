using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace videoprep.Models;

[Table("diagnoses")]
public partial class Diagnosis
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("disease_id")]
    public int DiseaseId { get; set; }

    [Column("diagnosis_date", TypeName = "TIMESTAMP")]
    public DateTime? DiagnosisDate { get; set; }

    [ForeignKey("DiseaseId")]
    [InverseProperty("Diagnoses")]
    public virtual Disease Disease { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Diagnoses")]
    public virtual Patient Patient { get; set; } = null!;
}
