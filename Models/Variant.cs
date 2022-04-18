using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirusDatabaseNew
{
    public partial class Variant
    {
        public Variant()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
            SymptomsVariants = new HashSet<SymptomsVariant>();
        }
        //public int SymptomId { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name ="Назва штаму")]
        public string? VariantName { get; set; }
        [Display(Name = "Походження штаму")]
        public string? VariantOrigin { get; set; }
        [Display(Name = "Дата появи штаму")]
        public DateTime VariantDateDiscovered { get; set; }
        public int? VirusId { get; set; }

        public virtual Virus? Virus { get; set; }
        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }
        public virtual ICollection<SymptomsVariant> SymptomsVariants { get; set; }
    }
}
