using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Agent_SSO
{

    public class clsautom
    {

        public const int WM_CLOSE = 16;
        public const int BN_CLICKED = 245;
        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        public const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        public const int WM_APPCOMMAND = 0x319;
        public const int WM_COMMAND = 0x111;
        public const Int32 CURSOR_SHOWING = 0x00000001;
        public const int MYACTION_HOTKEY_ID = 1;


        [DllImport("user32.dll")]
        static extern bool BlockInput(bool fBlockIt);

        // DLL libraries
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);


        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }


        [StructLayout(LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }


        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// GetCursorPosition
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }


        /// <summary>
        /// MouseMove
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseMove(int x, int y, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;

            System.Drawing.Point pt = Cursor.Position;
            Cursor.Position = new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// MouseClick
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseClick(int x, int y, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }


        /// <summary>
        /// BloquearInput
        /// </summary>
        /// <param name="bloq"></param>
        /// <param name="hashcode"></param>
        public void BloquearInput(bool bloq, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;

            BlockInput(bloq);
        }

        /// <summary>
        /// Wait
        /// </summary>
        /// <param name="milsec"></param>
        public void Wait(int milsec)
        {
            Thread.Sleep(milsec);
        }

        /// <summary>
        /// MouseDblClick
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseDblClick(int x, int y, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            Thread.Sleep(150);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }


        /// <summary>
        /// SendKeys
        /// </summary>
        /// <param name="keys"></param>
        public void SendKeys(string keys, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;
            System.Windows.Forms.SendKeys.SendWait(keys);
        }

        /// <summary>
        /// HandleWindow
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public IntPtr HandleWindow(string window, string hashcode)
        {



            IntPtr hwnd = IntPtr.Zero;

            if (hashcode != "xx0000Y127858#xF") return hwnd;


            hwnd = FindWindow(null, window);

            return hwnd;
        }

        /// <summary>
        /// StateWindow
        /// </summary>
        /// <param name="window"></param>
        /// <param name="state"></param>
        public void StateWindow(string window, int state, string hashcode)
        {


            IntPtr hwnd = IntPtr.Zero;

            if (hashcode != "xx0000Y127858#xF") return;

            hwnd = FindWindow(null, window);

            ShowWindow(hwnd, state);

            //The bring the application to focus
            SetForegroundWindow(hwnd);
        }

        /// <summary>
        /// PrintScreen
        /// </summary>
        /// <param name="path"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tx"></param>
        /// <param name="ty"></param>
        public void PrintScreen(string path, int x, int y, int tx, int ty, string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF") return;

            System.Drawing.Size obj = new System.Drawing.Size();

            obj.Width = tx;
            obj.Height = ty;

            Bitmap printscreen = new Bitmap(obj.Width, obj.Height);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(x, y, 0, 0, obj);

            printscreen.Save(path, ImageFormat.Jpeg);
        }

        /// <summary>
        /// CursorPosition
        /// </summary>
        /// <returns></returns>
        public string CursorPosition(string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF") return "";

            string sret = GetCursorPosition().X.ToString() + "," + GetCursorPosition().Y.ToString();
            return sret;
        }

        /// <summary>
        /// PixelPosition
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string PixelPosition(int x, int y, string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF") return "";

            Point loc = new Point(x, y);

            Color c = GetColorAt(loc);

            string spixel = c.R.ToString() + c.G.ToString() + c.B.ToString();

            return spixel;

        }

        /// <summary>
        /// GetColorAt
        /// </summary>
        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color GetColorAt(Point location)
        {

            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        /// <summary>
        /// GetProcess
        /// </summary>
        /// <returns></returns>
        public string GetProcess(string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF") return "";

            string sprocess = "";
            string svig = "";
            Process[] processlist = Process.GetProcesses();


            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {

                    sprocess = sprocess + svig + process.MainWindowTitle;
                    svig = ",";

                }
            }

            return sprocess;
        }


        /// <summary>
        /// GetCursorPointer
        /// </summary>
        /// <returns></returns>
        public IntPtr GetCursorPointer(string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF") return IntPtr.Zero;

            CURSORINFO pci = new CURSORINFO();
            pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

            if (GetCursorInfo(out pci))
            {
                if (pci.flags == CURSOR_SHOWING)
                {
                    return pci.hCursor;
                }
            }


            return pci.hCursor;



        }


        /// <summary>
        /// CloseWindow
        /// </summary>
        /// <param name="window"></param>
        public void CloseWindow(string window)
        {

            const UInt32 WM_CLOSE = 0x0010;

            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, window);

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);


        }


        /// <summary>
        /// CaptureScreenFull
        /// </summary>
        /// <returns></returns>
        public byte[] CaptureScreenFull(string hashcode)
        {

            if (hashcode != "xx0000Y127858#xF")
            {
                Bitmap bmp = null;
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
            }
            else
            {
                Bitmap bmp = CaptureScreen.CaptureDesktopWithCursor();

                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
            }


        }

        /// <summary>
        /// CaptureScreenFullPath
        /// </summary>
        /// <returns></returns>
        public void CaptureScreenFullPath(string path, string hashcode)
        {
            if (hashcode != "xx0000Y127858#xF") return;

            Bitmap bmp = CaptureScreen.CaptureDesktopWithCursor();
            bmp.Save(path, ImageFormat.Jpeg);
        }




    }

}