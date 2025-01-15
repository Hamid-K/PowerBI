using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Azure.Identity
{
	// Token: 0x0200008C RID: 140
	internal static class WindowsNativeMethods
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public static IntPtr CredRead(string target, WindowsNativeMethods.CRED_TYPE type)
		{
			IntPtr intPtr;
			WindowsNativeMethods.ThrowIfFailed(WindowsNativeMethods.Imports.CredRead(target, type, 0, out intPtr), "CredRead");
			return intPtr;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000E0CE File Offset: 0x0000C2CE
		public static void CredWrite(IntPtr userCredential)
		{
			WindowsNativeMethods.ThrowIfFailed(WindowsNativeMethods.Imports.CredWrite(userCredential, 0), "CredWrite");
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E0E1 File Offset: 0x0000C2E1
		public static void CredDelete(string target, WindowsNativeMethods.CRED_TYPE type)
		{
			WindowsNativeMethods.ThrowIfFailed(WindowsNativeMethods.Imports.CredDelete(target, type, 0), "CredDelete");
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000E0F5 File Offset: 0x0000C2F5
		public static void CredFree(IntPtr userCredential)
		{
			if (userCredential != IntPtr.Zero)
			{
				WindowsNativeMethods.Imports.CredFree(userCredential);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E10C File Offset: 0x0000C30C
		private static void ThrowIfFailed(bool isSucceeded, [CallerMemberName] string methodName = null)
		{
			if (isSucceeded)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			throw new InvalidOperationException((lastWin32Error == 1168) ? (methodName + " has failed but error is unknown.") : WindowsNativeMethods.MessageFromErrorCode(lastWin32Error));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E144 File Offset: 0x0000C344
		private static string MessageFromErrorCode(int errorCode)
		{
			uint num = 4864U;
			IntPtr zero = IntPtr.Zero;
			string text = null;
			try
			{
				if (WindowsNativeMethods.Imports.FormatMessage(num, IntPtr.Zero, errorCode, 0U, ref zero, 0U, IntPtr.Zero) == 0U)
				{
					return new Win32Exception(WindowsNativeMethods.Imports.RtlNtStatusToDosError(errorCode)).Message;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(zero);
					Marshal.FreeHGlobal(zero);
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x04000291 RID: 657
		public const int ERROR_NOT_FOUND = 1168;

		// Token: 0x04000292 RID: 658
		public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256U;

		// Token: 0x04000293 RID: 659
		public const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

		// Token: 0x04000294 RID: 660
		public const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

		// Token: 0x02000134 RID: 308
		public enum CRED_PERSIST : uint
		{
			// Token: 0x040006B2 RID: 1714
			CRED_PERSIST_SESSION = 1U,
			// Token: 0x040006B3 RID: 1715
			CRED_PERSIST_LOCAL_MACHINE,
			// Token: 0x040006B4 RID: 1716
			CRED_PERSIST_ENTERPRISE
		}

		// Token: 0x02000135 RID: 309
		public enum CRED_TYPE
		{
			// Token: 0x040006B6 RID: 1718
			GENERIC = 1,
			// Token: 0x040006B7 RID: 1719
			DOMAIN_PASSWORD,
			// Token: 0x040006B8 RID: 1720
			DOMAIN_CERTIFICATE,
			// Token: 0x040006B9 RID: 1721
			DOMAIN_VISIBLE_PASSWORD,
			// Token: 0x040006BA RID: 1722
			MAXIMUM
		}

		// Token: 0x02000136 RID: 310
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CredentialData
		{
			// Token: 0x040006BB RID: 1723
			public uint Flags;

			// Token: 0x040006BC RID: 1724
			public WindowsNativeMethods.CRED_TYPE Type;

			// Token: 0x040006BD RID: 1725
			public string TargetName;

			// Token: 0x040006BE RID: 1726
			public string Comment;

			// Token: 0x040006BF RID: 1727
			public FILETIME LastWritten;

			// Token: 0x040006C0 RID: 1728
			public uint CredentialBlobSize;

			// Token: 0x040006C1 RID: 1729
			public IntPtr CredentialBlob;

			// Token: 0x040006C2 RID: 1730
			public WindowsNativeMethods.CRED_PERSIST Persist;

			// Token: 0x040006C3 RID: 1731
			public uint AttributeCount;

			// Token: 0x040006C4 RID: 1732
			public IntPtr Attributes;

			// Token: 0x040006C5 RID: 1733
			public string TargetAlias;

			// Token: 0x040006C6 RID: 1734
			public string UserName;
		}

		// Token: 0x02000137 RID: 311
		private static class Imports
		{
			// Token: 0x06000637 RID: 1591
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, IntPtr pArguments);

			// Token: 0x06000638 RID: 1592
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("ntdll.dll")]
			public static extern int RtlNtStatusToDosError(int Status);

			// Token: 0x06000639 RID: 1593
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredReadW", SetLastError = true)]
			public static extern bool CredRead(string target, WindowsNativeMethods.CRED_TYPE type, int reservedFlag, out IntPtr userCredential);

			// Token: 0x0600063A RID: 1594
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredWriteW", SetLastError = true)]
			public static extern bool CredWrite(IntPtr userCredential, int reservedFlag);

			// Token: 0x0600063B RID: 1595
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredDeleteW", SetLastError = true)]
			public static extern bool CredDelete(string target, WindowsNativeMethods.CRED_TYPE type, int reservedFlag);

			// Token: 0x0600063C RID: 1596
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern void CredFree([In] IntPtr buffer);
		}
	}
}
