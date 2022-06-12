using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class VisitMap : ClassMap<Visit>
    {
        public VisitMap()
        {
            Id(x => x.Id);
            Map(x => x.VisitDate);
            Map(x => x.Diagnosis);
            Map(x => x.Description);
            Map(x => x.Cost);
            HasMany(x => x.Recipes)
              .Inverse()
              .KeyColumn("VisitId")
              .Cascade.All();
            Map(x => x.PatientId);
            References(x => x.Patient).Column("PatientId").Not.Insert().Not.Update();
            Map(x => x.DoctorId);
            References(x => x.Doctor).Column("DoctorId").Not.Insert().Not.Update();

            Table("Visits");
        }
    }
}
