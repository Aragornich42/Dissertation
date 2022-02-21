using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class ProfessionLink
    {
        public long Id { get; set; }

        public Profession ProfessionId { get; set; }

        public bool IsMain { get; set; }
    }
}
