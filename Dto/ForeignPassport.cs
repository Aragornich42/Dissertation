using System;

namespace Dto
{
    public class ForeignPassport
    {
        public long Id { get; set; }

        public Country Country { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
