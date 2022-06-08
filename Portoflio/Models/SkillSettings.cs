using System.Collections.Generic;

namespace Portoflio.Models
{   
    public class SkillSettings
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int SkillsId { get; set; }
        public Skills Skills { get; set; }
    }
}
