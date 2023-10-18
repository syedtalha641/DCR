using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCR.ViewModel.ViewModel
{
    public class MenuListViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; } = null;
        public bool? HasChildren { get; set; }
        public int? SortOrder { get; set; }
        public string? NavigationUrl { get; set; }
        public string? IconClass { get; set; }
    }
}
