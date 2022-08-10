using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model
{
    internal class ProjectModel
    {
        public int PrgId { get; set; }
        public string? PrgName { get; set; }
        public string? PrgOrganization { get; set; }
        public DateOnly? PrgDate { get; set; }

      

    }
}
