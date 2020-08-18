using EmployeeManagementSystem.Animations;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using EmployeeManagementSystem.Pages;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// A base page for every page inherits from 
    /// </summary>
    public class BasePage : Page
    {
        #region Properties

        public PageAnimationEnum SelectedPageAnimation { get; set; } 
        public float SlideDuration { get; set; } = 0.8f;

        #endregion

        #region Constructor

        public BasePage()
        {
            // If an animation is programmed, collapse to begin fade 
            if (SelectedPageAnimation != PageAnimationEnum.None)
                this.Visibility = Visibility.Collapsed;

            // Listen for the page loading 
            this.Loaded += BasePage_Loaded;
        }

        #endregion

        #region Methods

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
        }

        public async Task Animate()
        {
            // Another check to make sure that there is no un needed firing
            if (SelectedPageAnimation == PageAnimationEnum.None)
                return;

            switch (SelectedPageAnimation)
            {
                case PageAnimationEnum.SlideFromRight:

                    await PageAnimations.Slide(WindowWidth, 0, -WindowWidth, 0, 0, 0, 0, 0, 0.8f, this);

                    break;
                   
                case PageAnimationEnum.SlideToLeft:

                    await PageAnimations.Slide(0, 0, 0, 0, -WindowWidth,0, WindowWidth,0, 0.8f, this);

                    break;

                case PageAnimationEnum.FadeIn:

                    await PageAnimations.Fade(0, 1, 0.9f, this);

                    break;
            }
        }

        #endregion
    }
}

