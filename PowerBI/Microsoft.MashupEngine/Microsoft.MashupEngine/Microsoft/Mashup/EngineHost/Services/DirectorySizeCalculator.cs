using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019B1 RID: 6577
	internal static class DirectorySizeCalculator
	{
		// Token: 0x0600A6AA RID: 42666 RVA: 0x0022778C File Offset: 0x0022598C
		public static long Compute(string directoryPath, string searchPattern, out int count)
		{
			long num = 0L;
			count = 0;
			DirectorySizeCalculator.NativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
			using (DirectorySizeCalculator.SafeFindHandle safeFindHandle = DirectorySizeCalculator.NativeMethods.FindFirstFile(Path.Combine(directoryPath, searchPattern), out win32_FIND_DATA))
			{
				if (!safeFindHandle.IsInvalid)
				{
					do
					{
						long num2 = (long)((ulong)win32_FIND_DATA.nFileSizeLow + ((ulong)win32_FIND_DATA.nFileSizeHigh << 32));
						num += DirectorySize.PhysicalSize(num2);
						count++;
					}
					while (DirectorySizeCalculator.NativeMethods.FindNextFile(safeFindHandle, out win32_FIND_DATA));
				}
			}
			return num;
		}

		// Token: 0x020019B2 RID: 6578
		private class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600A6AB RID: 42667 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
			public SafeFindHandle()
				: base(true)
			{
			}

			// Token: 0x0600A6AC RID: 42668 RVA: 0x00227800 File Offset: 0x00225A00
			protected override bool ReleaseHandle()
			{
				return DirectorySizeCalculator.NativeMethods.FindClose(this.handle);
			}
		}

		// Token: 0x020019B3 RID: 6579
		private static class NativeMethods
		{
			// Token: 0x0600A6AD RID: 42669
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern DirectorySizeCalculator.SafeFindHandle FindFirstFile(string lpFileName, out DirectorySizeCalculator.NativeMethods.WIN32_FIND_DATA lpFindFileData);

			// Token: 0x0600A6AE RID: 42670
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FindNextFile([In] DirectorySizeCalculator.SafeFindHandle hFindFile, out DirectorySizeCalculator.NativeMethods.WIN32_FIND_DATA lpFindFileData);

			// Token: 0x0600A6AF RID: 42671
			[DllImport("kernel32.dll", BestFitMapping = false)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FindClose([In] IntPtr hFindFile);

			// Token: 0x020019B4 RID: 6580
			[Serializable]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct WIN32_FIND_DATA
			{
				// Token: 0x040056B6 RID: 22198
				public uint dwFileAttributes;

				// Token: 0x040056B7 RID: 22199
				public uint ftCreationTimeLow;

				// Token: 0x040056B8 RID: 22200
				public uint ftCreationTimeHigh;

				// Token: 0x040056B9 RID: 22201
				public uint ftLastAccessTimeLow;

				// Token: 0x040056BA RID: 22202
				public uint ftLastAccessTimeHigh;

				// Token: 0x040056BB RID: 22203
				public uint ftLastWriteTimeLow;

				// Token: 0x040056BC RID: 22204
				public uint ftLastWriteTimeHigh;

				// Token: 0x040056BD RID: 22205
				public uint nFileSizeHigh;

				// Token: 0x040056BE RID: 22206
				public uint nFileSizeLow;

				// Token: 0x040056BF RID: 22207
				public uint dwReserved0;

				// Token: 0x040056C0 RID: 22208
				public uint dwReserved1;

				// Token: 0x040056C1 RID: 22209
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
				public string cFileName;

				// Token: 0x040056C2 RID: 22210
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
				public string cAlternateFileName;
			}
		}
	}
}
