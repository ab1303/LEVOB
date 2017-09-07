using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Data.Models
{
    public class BaseEntity
    {
        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        [Required, MaxLength(100)]
        public string CreateLogin { get; set; }

        [MaxLength(100)]
        public string UpdateLogin { get; set; }
    }
}
