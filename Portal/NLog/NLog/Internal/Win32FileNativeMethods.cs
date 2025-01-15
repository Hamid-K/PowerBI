using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NLog.Targets;

namespace NLog.Internal
{
	// Token: 0x0200014E RID: 334
	internal static class Win32FileNativeMethods
	{
		// Token: 0x06001002 RID: 4098
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeFileHandle CreateFile(string lpFileName, Win32FileNativeMethods.FileAccess dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, Win32FileNativeMethods.CreationDisposition dwCreationDisposition, Win32FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06001003 RID: 4099
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetFileInformationByHandle(IntPtr hFile, out Win32FileNativeMethods.BY_HANDLE_FILE_INFORMATION lpFileInformation);

		// Token: 0x0400044B RID: 1099
		public const int FILE_SHARE_READ = 1;

		// Token: 0x0400044C RID: 1100
		public const int FILE_SHARE_WRITE = 2;

		// Token: 0x0400044D RID: 1101
		public const int FILE_SHARE_DELETE = 4;

		// Token: 0x0200028A RID: 650
		[Flags]
		public enum FileAccess : uint
		{
			// Token: 0x04000715 RID: 1813
			GenericRead = 2147483648U,
			// Token: 0x04000716 RID: 1814
			GenericWrite = 1073741824U,
			// Token: 0x04000717 RID: 1815
			GenericExecute = 536870912U,
			// Token: 0x04000718 RID: 1816
			GenericAll = 268435456U
		}

		// Token: 0x0200028B RID: 651
		public enum CreationDisposition : uint
		{
			// Token: 0x0400071A RID: 1818
			New = 1U,
			// Token: 0x0400071B RID: 1819
			CreateAlways,
			// Token: 0x0400071C RID: 1820
			OpenExisting,
			// Token: 0x0400071D RID: 1821
			OpenAlways,
			// Token: 0x0400071E RID: 1822
			TruncateExisting
		}

		// Token: 0x0200028C RID: 652
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BY_HANDLE_FILE_INFORMATION
		{
			// Token: 0x0400071F RID: 1823
			public uint dwFileAttributes;

			// Token: 0x04000720 RID: 1824
			public long ftCreationTime;

			// Token: 0x04000721 RID: 1825
			public long ftLastAccessTime;

			// Token: 0x04000722 RID: 1826
			public long ftLastWriteTime;

			// Token: 0x04000723 RID: 1827
			public uint dwVolumeSerialNumber;

			// Token: 0x04000724 RID: 1828
			public uint nFileSizeHigh;

			// Token: 0x04000725 RID: 1829
			public uint nFileSizeLow;

			// Token: 0x04000726 RID: 1830
			public uint nNumberOfLinks;

			// Token: 0x04000727 RID: 1831
			public uint nFileIndexHigh;

			// Token: 0x04000728 RID: 1832
			public uint nFileIndexLow;
		}
	}
}
