using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opdrag_3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        protected void Page_Load(object sender, EventArgs e)
        {
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);
           Label1.Text = (memKb / 1024 / 1024) + " GB of RAM installed.";
        }
    }
}//