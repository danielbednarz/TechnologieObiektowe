namespace DataGenerator
{
    public static class DepartmentsGenerator
    {
        public static List<DepartmentVM> GenerateDepartments()
        {
            int i = 1;

            List<DepartmentVM> medicaments = new()
            {
                new DepartmentVM { Id = i++, Name = "Oddział Anestezjologii i Intensywnej Terapii", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Chirurgii Ogólnej", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Chorób Płuc i Chemioterapii", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Chorób Wewnętrznych i Geriatrii", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Kardiologiczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Nefrologiczny i Chorób Wewnętrznych", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Neurologiczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Pediatryczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Położniczo – Ginekologiczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Psychiatryczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Rehabilitacyjny Ogólny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Urologiczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Szpitalny Oddział Ratunkowy", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
                new DepartmentVM { Id = i++, Name = "Oddział Dzienny Psychiatryczny", PhoneNumber = GeneratorHelper.GeneratePhoneNumber()},
            };

            return medicaments;
        }
    }
}
