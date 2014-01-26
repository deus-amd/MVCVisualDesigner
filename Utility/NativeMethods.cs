using System;
using System.Runtime.InteropServices;

namespace MVCVisualDesigner
{
    namespace Utility
    {

        internal class NativeMethods
        {
            public const int CF_BITMAP = 2;
            public const int CF_ENHMETAFILE = 14;
            public const int CHILDID_SELF = 0;
            public const int EM_GETMARGINS = 0xd4;
            public const int EN_UPDATE = 0x400;
            public const int EVENT_OBJECT_ACCELERATORCHANGE = 0x8012;
            public const int EVENT_OBJECT_CREATE = 0x8000;
            public const int EVENT_OBJECT_DEFACTIONCHANGE = 0x8011;
            public const int EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800d;
            public const int EVENT_OBJECT_DESTROY = 0x8001;
            public const int EVENT_OBJECT_FOCUS = 0x8005;
            public const int EVENT_OBJECT_HELPCHANGE = 0x8010;
            public const int EVENT_OBJECT_HIDE = 0x8003;
            public const int EVENT_OBJECT_LOCATIONCHANGE = 0x800b;
            public const int EVENT_OBJECT_NAMECHANGE = 0x800c;
            public const int EVENT_OBJECT_PARENTCHANGE = 0x800f;
            public const int EVENT_OBJECT_REORDER = 0x8004;
            public const int EVENT_OBJECT_SELECTION = 0x8006;
            public const int EVENT_OBJECT_SELECTIONADD = 0x8007;
            public const int EVENT_OBJECT_SELECTIONREMOVE = 0x8008;
            public const int EVENT_OBJECT_SELECTIONWITHIN = 0x8009;
            public const int EVENT_OBJECT_SHOW = 0x8002;
            public const int EVENT_OBJECT_STATECHANGE = 0x800a;
            public const int EVENT_OBJECT_VALUECHANGE = 0x800e;
            public const int HC_ACTION = 0;
            public const int HTTRANSPARENT = -1;
            public const int MB_ICONASTERISK = 0x40;
            public const int MB_ICONEXCLAMATION = 0x30;
            public const int MB_ICONHAND = 0x10;
            public const int MB_ICONQUESTION = 0x20;
            public const int MB_OK = 0;
            public const int MK_LBUTTON = 1;
            public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);
            public const int OBJID_CLIENT = -4;
            public const int OBJID_WINDOW = 0;
            public const int S_FALSE = 1;
            public const int S_OK = 0;
            public const int SIC_COMPLEX = 1;
            public const string uuid_IAccessible = "{618736E0-3C3D-11CF-810C-00AA00389B71}";
            public const int WH_MOUSE = 7;
            public const int WM_COMMAND = 0x111;
            public const int WM_CONTEXTMENU = 0x7b;
            public const int WM_GETOBJECT = 0x3d;
            public const int WM_IME_COMPOSITION = 0x10f;
            public const int WM_IME_ENDCOMPOSITION = 270;
            public const int WM_IME_STARTCOMPOSITION = 0x10d;
            public const int WM_KEYDOWN = 0x100;
            public const int WM_KEYUP = 0x101;
            public const int WM_KILLFOCUS = 8;
            public const int WM_LBUTTONDOWN = 0x201;
            public const int WM_LBUTTONUP = 0x202;
            public const int WM_MBUTTONDOWN = 0x207;
            public const int WM_MOUSEACTIVATE = 0x21;
            public const int WM_NCHITTEST = 0x84;
            public const int WM_NCLBUTTONDOWN = 0xa1;
            public const int WM_NCMBUTTONDOWN = 0xa7;
            public const int WM_NCRBUTTONDOWN = 0xa4;
            public const int WM_RBUTTONDOWN = 0x204;
            public const int WM_REFLECT = 0x2000;
            public const int WM_SETFOCUS = 7;
            public const int WM_SYSKEYDOWN = 260;

            private NativeMethods()
            {
            }

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            public static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr CallNextHookEx(HandleRef hhook, int code, IntPtr wParam, IntPtr lParam);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool CloseClipboard();

            [DllImport("gdi32.dll")]
            public static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNull);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr CreateCompatibleDC(HandleRef hDC);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool DeleteDC(HandleRef hDC);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll")]
            public static extern bool DeleteEnhMetaFile(IntPtr hemf);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            public static extern bool DeleteObject(HandleRef hObject);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool EmptyClipboard();

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern int GetCurrentThreadId();

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetDC(HandleRef hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern IntPtr GetDesktopWindow();

            [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern IntPtr GetFocus();

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern short GetKeyState(int keyCode);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetParent(HandleRef hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);

            [DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr LresultFromObject(ref Guid refiid, IntPtr wParam, HandleRef pAcc);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objId);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool OpenClipboard(IntPtr hWndNewOwner);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32", CharSet = CharSet.Auto)]
            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool ReleaseDC(HandleRef hWnd, HandleRef hDC);

            [DllImport("usp10.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern int ScriptIsComplex(string scriptText, int cInChars, int dwFlags);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

            [DllImport("user32", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, HandleRef hinst, int threadid);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool UnhookWindowsHookEx(HandleRef hhook);

            public static int SignedHIWORD(int n)
            {
                int num = (short)((n >> 0x10) & 0xffff);
                num = num << 0x10;
                return (num >> 0x10);
            }

            public static int SignedHIWORD(IntPtr n)
            {
                return SignedHIWORD((int)n);
            }

            public static int SignedLOWORD(int n)
            {
                int num = (short)(n & 0xffff);
                num = num << 0x10;
                return (num >> 0x10);
            }

            public static int SignedLOWORD(IntPtr n)
            {
                return SignedLOWORD((int)n);
            }

            public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

            [StructLayout(LayoutKind.Sequential)]
            public class MouseHookStruct
            {
                public int x;
                public int y;
                public IntPtr handle;
                public int wHitTestCode;
                public int extraInfo;
            }
        }
    }
}


