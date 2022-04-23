using System;
using System.Collections.Generic;

namespace Dto
{
    public class Employee
    {
        public long Id { get; set; }

        public string INN { get; set; }

        public string SNILS { get; set; }

        public string Position { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Nationality { get; set; }

        public string RegAddress { get; set; }

        public string FactAddress { get; set; }

        public string InMarriage { get; set; }

        public string Phone { get; set; }

        public string Additional { get; set; }

        public Relative Relative { get; set; }

        public List<Profession> Professions { get; set; }

        public List<Education> Educations { get; set; }

        public List<ForeignPassport> ForeignPassports { get; set; }

        public List<Language> Languages { get; set; }
    }
}
