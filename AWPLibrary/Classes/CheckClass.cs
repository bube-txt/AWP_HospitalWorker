using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AWPLibrary.Classes
{
    public class CheckClass
    {
        public bool InRange(int value, int min, int max)
        {
            if (min <= value && value <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DateCheck(string date)
        {
            if (Regex.Matches(date.Trim(), @"[0-9]{4}[-./\\][0-9]{2}[-./\\][0-9]{2}").Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TimeCheck(string time)
        {
            if (Regex.Matches(time.Trim(), @"[0-9]{2}:[0-9]{2}:[0-9]{2}").Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
