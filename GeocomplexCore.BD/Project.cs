using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Проект
    /// </summary>
    public partial class Project
    {
        public Project()
        {
            Districts = new HashSet<District>();
        }

        public int PrgId { get; set; }
        public string PrgName { get; set; } = null!;
        public int PrgOrganization { get; set; }
        public DateOnly? PrgDate { get; set; }

        public virtual Organization PrgOrganizationNavigation { get; set; } = null!;
        public virtual ICollection<District> Districts { get; set; }
    }
}
