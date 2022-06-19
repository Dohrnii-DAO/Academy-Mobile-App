using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.MockData
{
    [AddINotifyPropertyChangedInterface]
    public class LeaderBoard
    {
        public string Username { get; set; }
        public string Profile { get; set; }
        public int XP { get; set; }
        public string Badge { get; set; }
    }
}
