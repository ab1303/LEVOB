using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Services.DTO
{
    public class GlossaryDto
    {
        public int GlossaryId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
