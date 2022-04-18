using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirusDatabaseNew
{
    public partial class VirusGroup
    {
        public VirusGroup()
        {
            Viruses = new HashSet<Virus>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не може бути пустим!")]
        [Display(Name = "Група вірусів")]
        public string? GroupName { get; set; }
        [Display(Name = "Інформація про групу вірусів")]
        public string? GroupInfo { get; set; }
        [Display(Name = "Дата відкриття")]
        public DateTime? GroupDateDiscovered { get; set; }

        public virtual ICollection<Virus> Viruses { get; set; }
    }
}
