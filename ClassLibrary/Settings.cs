using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class UserSettings
    {
        // Props

        // Authority Level that can be used for a business, it will include 2 Levels
        public int AuthorityLevel { get; set; }


        // Constructor 
        public UserSettings(int authority)
        {
            AuthorityLevel =  authority;
        }

        // Methods 
        public bool CheckAuthorityLevel(int requiredLevel, int currentLevel)
        {
            if (requiredLevel >= currentLevel)
                return true;
            else
                return false;
        }
    }
}
