//using NHibernate;
//using NHibernate.Mapping.ByCode;
//using NHibernate.Mapping.ByCode.Conformist;
//using NHibernateProject.Model;

//namespace NHibernateProject.Mappings
//{
//    public class EmployeeMap : ClassMapping<Employee>
//    {
//        public EmployeeMap()
//        {
//            Id(x => x.Id, x =>
//            {
//                x.Generator(Generators.Increment);
//                x.Type(NHibernateUtil.Int32);
//                x.Column("Id");
//                x.UnsavedValue(0);
//            });

//            Property(b => b.Name, x =>
//            {
//                x.Type(NHibernateUtil.StringClob);
//            });

//            Property(b => b.Surname, x =>
//            {
//                x.Type(NHibernateUtil.StringClob);
//                x.NotNullable(true);
//            });

//            Property(b => b.BirthDate, x =>
//            {
//                x.Type(NHibernateUtil.DateTime);
//                x.NotNullable(true);
//            });

//            Property(b => b.Gender, x =>
//            {
//                x.Type(NHibernateUtil.Int32);
//                x.NotNullable(true);
//            });

//            Property(b => b.Address, x =>
//            {
//                x.Type(NHibernateUtil.StringClob);
//            });

//            Property(b => b.Salary, x =>
//            {
//                x.Type(NHibernateUtil.Decimal);
//            });

//            ManyToOne(b => b.Department);
    
//            Table("Employees");
//        }
//    }
//}
