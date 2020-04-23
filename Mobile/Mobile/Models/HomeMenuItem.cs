using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public enum MenuItemType
    {
        News,
        Profile,
        Messages,
        Votes,
        Users,
        Docks,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
