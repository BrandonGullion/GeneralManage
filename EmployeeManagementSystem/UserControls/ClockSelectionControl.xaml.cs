using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for ClockSelectionControl.xaml
    /// </summary>
    public partial class ClockSelectionControl : UserControl
    {
        #region Properties



        #endregion

        #region Constructor

        public ClockSelectionControl()
        {
            InitializeComponent();
        }

        #endregion

        #region INotifyPropChanged

        #endregion

        // Change the line size to match what button is being hovered
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            // Access the content for the user control button press and convert to int
            var content = ((Button)sender).Content;
            int intContent = 0;
            Int32.TryParse((string)content, out intContent);

            // Switch line information pertaining to specific button 
            switch (intContent)
            {
                case 1:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 220;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 80;
                    break;
                case 2:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 250;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 120;
                    break;
                case 3:
                    HourClockHand.X1 = 170;
                    HourClockHand.X2 = 270;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 165;
                    break;
                case 4:
                    HourClockHand.X1 = 170;
                    HourClockHand.X2 = 250;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 215;
                    break;
                case 5:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 220;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 250;
                    break;
                case 6:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 168;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 270;
                    break;
                case 7:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 120;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 250;
                    break;
                case 8:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 80;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 220;
                    break;
                case 9:
                    HourClockHand.X1 = 170;
                    HourClockHand.X2 = 60;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 165;
                    break;
                case 10:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 80;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 120;
                    break;
                case 11:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 115;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 80;
                    break;
                case 12:
                    HourClockHand.X1 = 168;
                    HourClockHand.X2 = 168;
                    HourClockHand.Y1 = 165;
                    HourClockHand.Y2 = 60;
                    break;
            }

        }
    }
}
