using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.UserModels
{
    public class LoginResponse
    {
        public string Refresh { get; set; }
        public string Access { get; set; }
        public string Detail { get; set; }
    }
}
