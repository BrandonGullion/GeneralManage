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

        /// <summary>
        /// Animate a fade in 
        /// </summary>
        /// <param name="fromValue">opacity value at start of animation</param>
        /// <param name="toValue">opacity value at the end of animation </param>
        /// <param name="duration">speed to complete animation</param>
        /// <param name="userControl">desired control that will operate animation</param>
        /// <returns></returns>
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


        // Slide animation 
        public static async Task Slide(double fromLeft, double fromTop, double fromRight, double fromBottom,
            double toLeft, double toTop, double toRight, double toBottom, float slideDuration, UserControl userControl)
        {
            // Init storyboard 
            var storyboard = new Storyboard();

            var thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(slideDuration)),
                From = new Thickness(fromLeft, fromTop, fromRight, fromBottom),
                To = new Thickness(toLeft, toTop, toRight, toBottom),
                DecelerationRatio = 0.9f,
            };

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));

            storyboard.Children.Add(thicknessAnimation);

            storyboard.Begin(userControl);

            userControl.Visibility = Visibility.Visible;

            await Task.Delay((int)slideDuration * 1000);
        }
    }
}
