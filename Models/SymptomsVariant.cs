using System;
using System.Collections.Generic;

namespace VirusDatabaseNew
{
    public partial class SymptomsVariant
    {
        public int VariantId { get; set; }
        public int SymptomId { get; set; }
        public string? Severity { get; set; }

        public virtual Symptom Symptom { get; set; } = null!;
        public virtual Variant Variant { get; set; } = null!;
    }
}
