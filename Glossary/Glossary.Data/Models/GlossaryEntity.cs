using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Data.Models
{
    public class GlossaryEntity : BaseEntity
    {
        [Key]
        public int GlossaryEntityId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Term { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Definition { get; set; }
    }
}
