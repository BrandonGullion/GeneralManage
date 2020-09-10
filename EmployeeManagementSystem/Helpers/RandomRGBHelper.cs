using System;
using System.Windows.Media;

namespace EmployeeManagementSystem.Helpers
{
    public class RandomRGBHelper
    {
        public static string GenerateRandomColor()
        {
            // Byte Values 
            byte bR = 0;
            byte bG = 0;
            byte bB = 0;

            Random r = new Random();
            var R = r.Next(125, 255).ToString();
            var G = r.Next(125, 255).ToString();
            var B = r.Next(125, 255).ToString();

            Byte.TryParse(R,out bR);
            Byte.TryParse(G,out bG);
            Byte.TryParse(B,out bB);

            Color color = Color.FromRgb(bR, bG, bB);

            string hex = $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";

            return hex;

        }
    }
}
