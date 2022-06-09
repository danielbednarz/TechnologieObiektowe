using System.ComponentModel.DataAnnotations;

namespace DataGenerator
{
    public enum TableType
    {
        [Display(Name = "Departments")]
        Departments,
        [Display(Name = "Doctors")]
        Doctors,
        [Display(Name = "Employees")]
        Employees,
        [Display(Name = "Medicaments")]
        Medicaments,
        [Display(Name = "Nurses")]
        Nurses,
        [Display(Name = "Patients")]
        Patients,
        [Display(Name = "Recipes")]
        Recipes,
        [Display(Name = "RecipeMedicaments")]
        RecipeMedicaments,
        [Display(Name = "TechnicalWorkers")]
        TechnicalWorkers,
        [Display(Name = "Visits")]
        Visits,
        [Display(Name = "MultipleTables")]
        MultipleTables,
    }
}
