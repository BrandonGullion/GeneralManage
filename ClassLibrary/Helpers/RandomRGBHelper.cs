using System;

namespace ClassLibrary
{
    public class RandomRGBHelper
    {
        public static string GenerateRandomColor()
        {
            // Assigning Byte Variables 
            byte bR = 0, bG = 0, bB = 0;

            /// Creating instance of random and selecting RGB int values to generate standard 
            /// RGB colors with lighter hues and converting to string for formatting 
            Random r = new Random();
            var R = r.Next(125, 255).ToString();
            var G = r.Next(125, 255).ToString();
            var B = r.Next(125, 255).ToString();

           // Formating the Byte values 
            Byte.TryParse(R,out bR);
            Byte.TryParse(G,out bG);
            Byte.TryParse(B,out bB);

            // Using string formatting to 
            string hex = $"#{bR:X2}{bG:X2}{bB:X2}";

            return hex;
        }
    }
}
