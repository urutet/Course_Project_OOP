using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.Windows.Interfaces
{
    interface ICloseWindows
    {
        Action Close { get; set; }
    }
}
