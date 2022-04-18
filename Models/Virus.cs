using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirusDatabaseNew
{
    public partial class Virus
    {
        public Virus()
        {
            Variants = new HashSet<Variant>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name ="Назва вірусу")]
        public string? VirusName { get; set; }
        [Display(Name = "Дата відкриття")]
        public DateTime? VirusDateDiscovered { get; set; }
        public int? GroupId { get; set; }
        [Display(Name = "Група")]
        public virtual VirusGroup? Group { get; set; }
        public virtual ICollection<Variant> Variants { get; set; }
    }
}
