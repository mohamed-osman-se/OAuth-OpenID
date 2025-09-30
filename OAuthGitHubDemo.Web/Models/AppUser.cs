using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthGitHubDemo.Web.Models
{
    public class AppUser
    {

        public AppUser? user { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }

        public bool here = false;



    }
}