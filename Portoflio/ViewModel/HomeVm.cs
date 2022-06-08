using Portoflio.Models;
using System.Collections.Generic;

namespace Portoflio.ViewModel
{
    public class HomeVm
    {
        public List<Skills> Skills { get; set; }
        public List<SkillSettings> SkillSettings { get; set; }

        public List<Social> Socials { get; set; }
        public List<Interest> Interests { get; set; }
        public List<About> Abouts { get; set; }
        public List<Expeirence> Expeirences { get; set; }
        public List<Project> Projects { get; set; }
    }
}
