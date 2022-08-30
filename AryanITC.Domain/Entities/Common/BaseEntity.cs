using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AryanITC.Domain.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
