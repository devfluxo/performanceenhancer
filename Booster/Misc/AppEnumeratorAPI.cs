using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Booster.Misc
{
    [SuppressUnmanagedCodeSecurity]
    public sealed class AppEnumeratorApi : IAppEnumerator, IDisposable
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public void Init(List<string> apps)
        {
            if (apps == null || apps.Count == 0)
            {
                throw new ArgumentException("Should supply names of apps", "apps");
            }
            this.apps = apps;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
        public IEnumerable<uint> Enumerate()
        {
            IntPtr peBuffer = IntPtr.Zero;
            IntPtr hProcessSnap = AppEnumeratorApi.CreateToolhelp32Snapshot(2U, 0U);
            if (hProcessSnap == AppEnumeratorApi.INVALID_HANDLE_VALUE)
            {
                throw new Win32Exception();
            }
            try
            {
                int peBufferSize = Marshal.SizeOf(typeof(AppEnumeratorApi.PROCESSENTRY32)) + 520;
                peBuffer = Marshal.AllocHGlobal(peBufferSize);
                IntPtr szExeFilePtr = peBuffer + Marshal.OffsetOf(typeof(AppEnumeratorApi.PROCESSENTRY32), "dwFlags").ToInt32() + 4;
                IntPtr th32ProcessIDPtr = peBuffer + Marshal.OffsetOf(typeof(AppEnumeratorApi.PROCESSENTRY32), "th32ProcessID").ToInt32();
                Marshal.WriteInt32(peBuffer, 0, peBufferSize);
                if (!AppEnumeratorApi.Process32First(hProcessSnap, peBuffer))
                {
                    throw new Win32Exception();
                }
                do
                {
                    string szExeFile = Marshal.PtrToStringUni(szExeFilePtr);
                    if (!string.IsNullOrWhiteSpace(szExeFile))
                    {
                        szExeFile = szExeFile.ToLower();
                        if (this.apps.Contains(szExeFile))
                        {
                            uint id = (uint)Marshal.ReadInt32(th32ProcessIDPtr);
                            yield return id;
                        }
                    }
                }
                while (AppEnumeratorApi.Process32Next(hProcessSnap, peBuffer));
            }
            finally
            {
                if (hProcessSnap != AppEnumeratorApi.INVALID_HANDLE_VALUE)
                {
                    AppEnumeratorApi.CloseHandle(hProcessSnap);
                }
                if (peBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(peBuffer);
                }
            }
            yield break;
            yield break;
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
        public void Dispose()
        {
        }

        // Token: 0x06000004 RID: 4
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot([In] uint dwFlags, [In] uint th32ProcessID);

        // Token: 0x06000005 RID: 5
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool Process32First([In] IntPtr hSnapshot, [In] IntPtr lppe);

        // Token: 0x06000006 RID: 6
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool Process32Next([In] IntPtr hSnapshot, [In] IntPtr lppe);

        // Token: 0x06000007 RID: 7
        [DllImport("kernel32.dll")]
        private static extern int CloseHandle([In] IntPtr Handle);

        // Token: 0x04000001 RID: 1
        private List<string> apps;

        // Token: 0x04000002 RID: 2
        private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        // Token: 0x04000003 RID: 3
        private const uint TH32CS_SNAPPROCESS = 2U;

        // Token: 0x02000024 RID: 36
        private struct PROCESSENTRY32
        {
            // Token: 0x04000098 RID: 152
            internal uint dwSize;

            // Token: 0x04000099 RID: 153
            internal uint cntUsage;

            // Token: 0x0400009A RID: 154
            internal uint th32ProcessID;

            // Token: 0x0400009B RID: 155
            internal IntPtr th32DefaultHeapID;

            // Token: 0x0400009C RID: 156
            internal uint th32ModuleID;

            // Token: 0x0400009D RID: 157
            internal uint cntThreads;

            // Token: 0x0400009E RID: 158
            internal uint th32ParentProcessID;

            // Token: 0x0400009F RID: 159
            internal int pcPriClassBase;

            // Token: 0x040000A0 RID: 160
            internal uint dwFlags;
        }
    }
}
