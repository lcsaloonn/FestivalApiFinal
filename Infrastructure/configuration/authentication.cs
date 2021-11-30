using System.Collections.Generic;

namespace WebAPI.configuration
{
    public class Authentication
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; } // if result is success or not
        public List<string> Errors { get; set; }
    }
}