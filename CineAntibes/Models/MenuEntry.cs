using System;
namespace CineAntibes.Models
{
    public class MenuEntry
    {
        // Menu item's title
        public string Title { get; set; }

        // Menu item's icon
        public string IconSource { get; set; }

        // Menu item's targeted page
        public Type TargetType { get; set; }
    }
}
