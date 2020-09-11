using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VisibilityController : PropChanged
    {
        #region Properties 

        private bool clockControlVisibility;

        public bool ClockControlVisibility
        {
            get { return clockControlVisibility; }
            set { clockControlVisibility = value; OnPropertyChanged(nameof(ClockControlVisibility)); }
        }

        private bool hourClockVisibility;

        public bool HourClockVisibility
        {
            get { return hourClockVisibility; }
            set { hourClockVisibility = value; OnPropertyChanged(nameof(HourClockVisibility)); }
        }

        private bool minClockVisibility;

        public bool MinClockVisibility
        {
            get { return minClockVisibility; }
            set { minClockVisibility = value; OnPropertyChanged(nameof(minClockVisibility)); }
        }

        private bool confirmationVisibility;

        public bool ConfirmationVisiblity
        {
            get { return confirmationVisibility; }
            set { confirmationVisibility = value; OnPropertyChanged(nameof(ConfirmationVisiblity)); }
        }

        public VisibilityController()
        {
            ConfirmationVisiblity = true;
            MinClockVisibility = true;
            HourClockVisibility = true;
            ClockControlVisibility = true;
        }

        #endregion
    }
}
