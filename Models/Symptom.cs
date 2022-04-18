using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirusDatabaseNew
{
    public partial class Symptom
    {
        public Symptom()
        {
            SymptomsVariants = new HashSet<SymptomsVariant>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Симптом")]
        public string? SymptomName { get; set; }

        public virtual ICollection<SymptomsVariant> SymptomsVariants { get; set; }
    }
}
