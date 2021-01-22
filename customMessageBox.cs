using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace BD_test
{
    public class SilentMessageBox
    {
        
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) {

            Volume.Off();
            DialogResult temp = MessageBox.Show(text, caption, buttons, icon);
            Volume.On();
            return temp;
        }

    }
    public class Volume //класс который может менять громкость
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr h, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr h, uint dwVolume);

        private static uint _savedVolumeLevel;
        private static Boolean VolumeLevelSaved = false;

        // ----------------------------------------------------------------------------
        public static void On()
        {
            if (VolumeLevelSaved)
            {
                waveOutSetVolume(IntPtr.Zero, _savedVolumeLevel);
            }
        }

        // ----------------------------------------------------------------------------
        public static void Off()
        {
            waveOutGetVolume(IntPtr.Zero, out _savedVolumeLevel);
            VolumeLevelSaved = true;

            waveOutSetVolume(IntPtr.Zero, 0);
        }
    }

    public class CMB
    {
        static void ShowMessage(string str)
        {
            Volume.Off();
            MessageBox.Show("TEXT");
            Volume.Off();
        }
    }
}
