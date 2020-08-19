using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EmployeeManagementSystem"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EmployeeManagementSystem;assembly=EmployeeManagementSystem"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:control/>
    ///
    /// </summary>
    public class DashboardControl : Control
    {
        static DashboardControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DashboardControl), new FrameworkPropertyMetadata(typeof(DashboardControl)));
        }
        

        /// <summary>
        /// Templated binding for the textbox
        /// </summary>
        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTextProperty =
            DependencyProperty.Register("ItemText", typeof(string), typeof(DashboardControl), new PropertyMetadata(""));


        /// <summary>
        /// To control the corener radius of control 
        /// </summary>
        public CornerRadius DesiredCornerRadius
        {
            get { return (CornerRadius)GetValue(DesiredCornerRadiusProperty); }
            set { SetValue(DesiredCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesiredCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesiredCornerRadiusProperty =
            DependencyProperty.Register("DesiredCornerRadius", typeof(CornerRadius), typeof(DashboardControl), new PropertyMetadata(null));

        /// <summary>
        /// Add image in ui front end 
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(DashboardControl), new PropertyMetadata(null));


        public MouseEventArgs Clicked
        {
            get { return (MouseEventArgs)GetValue(ClickedProperty); }
            set { SetValue(ClickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickedProperty =
            DependencyProperty.Register("Clicked", typeof(MouseEventArgs), typeof(DashboardControl), new PropertyMetadata(null));

        


    }
}
