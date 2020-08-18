using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System;
using System.Windows.Controls;

namespace EmployeeManagementSystem.Animations
{
    public static class ControlAnimations
    {
        public static async Task FadeInAnimation(double fromValue, double toValue, float duration, UserControl userControl)
        {
            // Create the storyBoard 
            var storyboard = new Storyboard();

            // Convert the duration
            var Duration = new Duration(TimeSpan.FromSeconds((int)duration));

            // Set the fade in values if needed 
            var fadeIn = new DoubleAnimation(fromValue, toValue, Duration)
            {
                From = fromValue,
                To = toValue,
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
            };

            // Sets the target property to be opacity for a fade in 
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));

            // Adds the fade in to the storyboard
            storyboard.Children.Add(fadeIn);

            // Binds the storyboard to the current control and starts 
            storyboard.Begin(userControl);

            // Await the same time as the animation 
            await Task.Delay((int)duration * 1000);
        }
    }
}
