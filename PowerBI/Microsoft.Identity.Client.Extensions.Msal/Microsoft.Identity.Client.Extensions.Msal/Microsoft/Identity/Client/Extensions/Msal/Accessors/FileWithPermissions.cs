using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Identity.Client.Extensions.Msal.Accessors
{
	// Token: 0x02000024 RID: 36
	internal static class FileWithPermissions
	{
		// Token: 0x060000A6 RID: 166
		[DllImport("libc", EntryPoint = "creat", SetLastError = true)]
		private static extern int PosixCreate([MarshalAs(UnmanagedType.LPStr)] string pathname, int mode);

		// Token: 0x060000A7 RID: 167
		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		private static extern int PosixChmod([MarshalAs(UnmanagedType.LPStr)] string pathname, int mode);

		// Token: 0x060000A8 RID: 168 RVA: 0x0000470C File Offset: 0x0000290C
		public static void WriteToNewFileWithOwnerRWPermissions(string path, byte[] data)
		{
			if (SharedUtilities.IsWindowsPlatform())
			{
				FileWithPermissions.WriteToNewFileWithOwnerRWPermissionsWindows(path, data);
				return;
			}
			if (SharedUtilities.IsMacPlatform() || SharedUtilities.IsLinuxPlatform())
			{
				FileWithPermissions.WriteToNewFileWithOwnerRWPermissionsUnix(path, data);
				return;
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004738 File Offset: 0x00002938
		private static void WriteToNewFileWithOwnerRWPermissionsUnix(string path, byte[] data)
		{
			int num = Convert.ToInt32("600", 8);
			int num2 = FileWithPermissions.PosixCreate(path, num);
			if (num2 == -1)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				using (File.Create(path))
				{
				}
				File.Delete(path);
				throw new InvalidOperationException(string.Format("libc creat() failed with last error code {0}, but File.Create did not", lastWin32Error));
			}
			using (FileStream fileStream2 = new FileStream(new SafeFileHandle((IntPtr)num2, true), FileAccess.ReadWrite))
			{
				fileStream2.Write(data, 0, data.Length);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000047DC File Offset: 0x000029DC
		private static void WriteToNewFileWithOwnerRWPermissionsWindows(string filePath, byte[] data)
		{
			FileSecurity fileSecurity = new FileSecurity();
			FileSystemRights fileSystemRights = FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.ReadPermissions;
			fileSecurity.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, fileSystemRights, 0, 1, 0));
			fileSecurity.SetAccessRuleProtection(true, false);
			FileStream fileStream = null;
			try
			{
				fileStream = new FileInfo(filePath).Create(FileMode.Create, fileSystemRights, FileShare.Read, data.Length, FileOptions.None, fileSecurity);
				fileStream.Write(data, 0, data.Length);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
			}
		}
	}
}
