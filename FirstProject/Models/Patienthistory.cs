using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Patienthistory
    {
        public decimal Patienthistoryid { get; set; }
        public string ChronicDiseases { get; set; }
        public string MedicinesOnRegularBasis { get; set; }
        public string PreviousVisitsToTheClinicForTheSameReason { get; set; }
        public string MainComplaint { get; set; }
        public string SensitivityToAnything { get; set; }
        public string PreviousExaminations { get; set; }
        public decimal? Patientid { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
