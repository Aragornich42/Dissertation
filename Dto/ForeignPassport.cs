using System;

namespace Dto
{
    public class ForeignPassport
    {
        public string CountryName { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
