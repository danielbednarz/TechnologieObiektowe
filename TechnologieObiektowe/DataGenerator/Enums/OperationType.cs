using System.ComponentModel.DataAnnotations;

namespace DataGenerator
{
    public enum OperationType
    {
        [Display(Name = "Add")]
        Add,
        [Display(Name = "AddRange")]
        AddRange,
        [Display(Name = "Update")]
        Update,
        [Display(Name = "UpdateRange")]
        UpdateRange,
        [Display(Name = "Delete")]
        Delete,
        [Display(Name = "DeleteRange")]
        DeleteRange,
        [Display(Name = "Select")]
        Select,
        [Display(Name = "Create")]
        Create,
        [Display(Name = "SaveChanges")]
        SaveChanges
    }
}
