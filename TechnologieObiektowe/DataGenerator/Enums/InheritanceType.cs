using System.ComponentModel.DataAnnotations;

namespace DataGenerator
{
    public enum InheritanceType
    {
        [Display(Name = "TPT")]
        TPT,
        [Display(Name = "TPH")]
        TPH,
        [Display(Name = "TPC")]
        TPC,
    }
}
