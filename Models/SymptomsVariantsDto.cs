using System.ComponentModel.DataAnnotations;
namespace VirusDatabaseNew.Models
{
    public class SymptomsVariantsDto
    {
        public int SymptomId { get; set; }

        [Display(Name = "Symptom:")]
        public string Name { get; set; }

        [Display(Name = "Variant")]
        public bool isSymptomVariants { get; set; }


        public SymptomsVariantsDto(int _id, string _name, bool _isBookAuthor)
        {
            Name = _name;
            SymptomId = _id;
            isSymptomVariants = _isBookAuthor;
        }
    }
}
