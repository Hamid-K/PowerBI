using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000214 RID: 532
	public static class ExtendedFileStream
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x000318DC File Offset: 0x0002FADC
		public static FileStream Create(string path, FileMode mode, FileAccess access, FileShare share)
		{
			FileStream fileStream;
			if (path.Length >= 260)
			{
				path = NativeMethods.ToLongPath(path);
				SafeFileHandle safeFileHandle = NativeMethods.CreateFile(path, NativeMethods.Map(access), NativeMethods.Map(share), IntPtr.Zero, NativeMethods.Map(mode), (NativeMethods.EFileAttributes)0U, IntPtr.Zero);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (safeFileHandle.IsInvalid)
				{
					if (lastWin32Error == 2)
					{
						throw new FileNotFoundException("file not found", path);
					}
					if (lastWin32Error != 5)
					{
						throw new Win32Exception(lastWin32Error, "Cannot access file: '{0}'".FormatWithInvariantCulture(new object[] { path }));
					}
					throw new UnauthorizedAccessException("Cannot access file: '{0}'".FormatWithInvariantCulture(new object[] { path }));
				}
				else
				{
					fileStream = new FileStream(safeFileHandle, access);
				}
			}
			else
			{
				fileStream = new FileStream(path, mode, access, share);
			}
			return fileStream;
		}
	}
}
