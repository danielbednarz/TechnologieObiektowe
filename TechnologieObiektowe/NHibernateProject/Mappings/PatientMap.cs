using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.BirthDate);
            Map(x => x.Gender);
            Map(x => x.Address);
            HasMany(x => x.Visits)
              .Inverse()
              .KeyColumn("PatientId")
              .Cascade.All();

            Table("Patients");
        }
    }
}
