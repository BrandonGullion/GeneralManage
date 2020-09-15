using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ClassLibrary
{
    public static class StringToDoubleConverter
    {
        public static char[] DisallowedSymbols { get; set; } = { '!', '@','#', '$','%', '^','&', '*','(',')','_', '-',
        '+','=','{', '}' , '[' , ']' , ':' , ';' , '"' , ',' , '<' , '>' , '/' , '?' , ',' , '|'};

        public static double ConvertToDouble (string value)
        {
            // Iterates over the disallowed symbols and checks if there are any letters present 
            foreach(var symbol in DisallowedSymbols)
                if(value.Contains(symbol) || value.Any(x => char.IsLetter(x)))
                {
                    Console.WriteLine("Invalid Symbol entered");
                    return 0;
                }
            
            return Convert.ToDouble(value);
        }
    }
}
