using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaLib
{
    public interface IDeviceSettings
    {
        bool InitFrameRate();

        void UpdateDemoWindow(DEMOSTATE DemoState);
    }
}
