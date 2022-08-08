using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Буровая организация
    /// </summary>
    public partial class Organization
    {
        public Organization()
        {
            Projects = new HashSet<Project>();
            WellOtherUses = new HashSet<WellOtherUse>();
        }

        public int OrgId { get; set; }
        public string OrgName { get; set; } = null!;
        public string? OrgNote { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<WellOtherUse> WellOtherUses { get; set; }
    }
}
