using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EmployeeManagementSystem.UserControls
{
    public class BaseControl : UserControl
    {
        // Allow for setting of the desired animation 
        public ControlAnimationEnum SelectedAnimation { get; set; }

        // Animation check to determine which one to play 
        public async Task Animate()
        {
            if (SelectedAnimation == ControlAnimationEnum.None)
                return;

            switch (SelectedAnimation)
            {
                case ControlAnimationEnum.SlideControlFromRight:
                    await ControlAnimations.Slide(this.ActualWidth, 0, -this.ActualHeight, 0, 0, 0, 0, 0, 0.3f, this);
                    break;

                case ControlAnimationEnum.SlideControlFromLeft:
                    await ControlAnimations.Slide(-Width, 0, Width, 0, 0, 0, 0, 0,0.3f, this);
                    break;

                case ControlAnimationEnum.SmallFadeIn:
                    await ControlAnimations.FadeInAnimation(0, 1, 0.3f, this);
                    break;
            }
            
        }
    }
}
