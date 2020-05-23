using System;
using System.Collections.Generic;
using System.Text;

namespace EHMobile.Models
{
    public enum MenuItemType
    {
        Browse,
        Login,
        Events,
        Votes,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
