using System.ComponentModel.DataAnnotations;

namespace DataGenerator
{
    public enum OrmType
    {
        [Display(Name = "EntityFramework")]
        EntityFramework,
        [Display(Name = "NHibernate")]
        NHibernate
    }
}
