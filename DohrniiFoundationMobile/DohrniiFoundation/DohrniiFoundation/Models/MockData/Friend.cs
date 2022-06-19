using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.MockData
{
    [AddINotifyPropertyChangedInterface]
    public class Friend
    {
        public string Username { get; set; }
        public string Profile { get; set; }
        public int XP { get; set; }
        public int Jelly { get; set; }
        public int DHN { get; set; }
        public int Score { get; set; }

        public string ScoreImg { 
            get 
            {
                if(Score >= 1)
                {
                    return "BlueUp.png";
                }
                else
                {
                    return "RedDown.png";
                }
            } 
        }
    }
}
