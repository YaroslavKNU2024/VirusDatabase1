using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirusDatabaseNew
{
    public partial class Country
    {
        public Country()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Назва країни")]
        public string? CountryName { get; set; }

        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }
    }
}
