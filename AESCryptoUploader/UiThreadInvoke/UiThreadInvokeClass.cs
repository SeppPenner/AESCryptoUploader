using System;
using System.Windows.Forms;

namespace UiThreadInvoke
{
    public static class UiThreadInvokeClass
    {
        public static void UiThreadInvoke(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(code);
                return;
            }
            code.Invoke();
        }
    }
}