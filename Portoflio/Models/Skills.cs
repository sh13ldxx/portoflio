using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portoflio.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SkillSettings> SkillSettings { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
