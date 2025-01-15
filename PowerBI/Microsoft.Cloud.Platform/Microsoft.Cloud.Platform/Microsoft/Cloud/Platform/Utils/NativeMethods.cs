using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000215 RID: 533
	internal static class NativeMethods
	{
		// Token: 0x06000E04 RID: 3588
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetLastError();

		// Token: 0x06000E05 RID: 3589
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern bool CreateDirectory(string directoryPath, IntPtr lpSecurityAttributes);

		// Token: 0x06000E06 RID: 3590
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeFileHandle CreateFile(string lpFileName, NativeMethods.EFileAccess dwDesiredAccess, NativeMethods.EFileShare dwShareMode, IntPtr lpSecurityAttributes, NativeMethods.ECreationDisposition dwCreationDisposition, NativeMethods.EFileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000E07 RID: 3591
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint GetFileAttributes(string lpFileName);

		// Token: 0x06000E08 RID: 3592 RVA: 0x00031993 File Offset: 0x0002FB93
		public static NativeMethods.EFileAccess Map(FileAccess access)
		{
			switch (access)
			{
			case FileAccess.Read:
				return (NativeMethods.EFileAccess)2147483648U;
			case FileAccess.Write:
				return NativeMethods.EFileAccess.GenericWrite;
			case FileAccess.ReadWrite:
				return (NativeMethods.EFileAccess)3221225472U;
			default:
				ExtendedDiagnostics.EnsureInvalidSwitchValue<FileAccess>(access);
				return (NativeMethods.EFileAccess)0U;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000319C4 File Offset: 0x0002FBC4
		public static NativeMethods.EFileShare Map(FileShare share)
		{
			switch (share)
			{
			case FileShare.None:
				return NativeMethods.EFileShare.None;
			case FileShare.Read:
				return NativeMethods.EFileShare.Read;
			case FileShare.Write:
				return NativeMethods.EFileShare.Write;
			case FileShare.ReadWrite:
				return NativeMethods.EFileShare.Read | NativeMethods.EFileShare.Write;
			case FileShare.Delete:
				return NativeMethods.EFileShare.Delete;
			default:
				ExtendedDiagnostics.EnsureInvalidSwitchValue<FileShare>(share);
				return NativeMethods.EFileShare.None;
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000319F3 File Offset: 0x0002FBF3
		public static NativeMethods.ECreationDisposition Map(FileMode mode)
		{
			switch (mode)
			{
			case FileMode.CreateNew:
				return NativeMethods.ECreationDisposition.New;
			case FileMode.Create:
				return NativeMethods.ECreationDisposition.CreateAlways;
			case FileMode.Open:
				return NativeMethods.ECreationDisposition.OpenExisting;
			case FileMode.OpenOrCreate:
				return NativeMethods.ECreationDisposition.OpenAlways;
			case FileMode.Truncate:
				return NativeMethods.ECreationDisposition.TruncateExisting;
			default:
				ExtendedDiagnostics.EnsureInvalidSwitchValue<FileMode>(mode);
				return (NativeMethods.ECreationDisposition)0U;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00031A24 File Offset: 0x0002FC24
		public static string ToLongPath(string path)
		{
			Ensure.ArgSatisfiesCondition("path", path.Length >= 260, "'path' must equal or exceed MAX_PATH");
			if (!path.StartsWith("\\\\?\\", StringComparison.Ordinal))
			{
				return "\\\\?\\{0}".FormatWithInvariantCulture(new object[] { path });
			}
			return path;
		}

		// Token: 0x04000582 RID: 1410
		public const int MaxPath = 260;

		// Token: 0x020006BF RID: 1727
		[Flags]
		public enum EFileAccess : uint
		{
			// Token: 0x04001316 RID: 4886
			GenericRead = 2147483648U,
			// Token: 0x04001317 RID: 4887
			GenericWrite = 1073741824U,
			// Token: 0x04001318 RID: 4888
			GenericExecute = 536870912U,
			// Token: 0x04001319 RID: 4889
			GenericAll = 268435456U
		}

		// Token: 0x020006C0 RID: 1728
		[Flags]
		public enum EFileShare : uint
		{
			// Token: 0x0400131B RID: 4891
			None = 0U,
			// Token: 0x0400131C RID: 4892
			Read = 1U,
			// Token: 0x0400131D RID: 4893
			Write = 2U,
			// Token: 0x0400131E RID: 4894
			Delete = 4U
		}

		// Token: 0x020006C1 RID: 1729
		public enum ECreationDisposition : uint
		{
			// Token: 0x04001320 RID: 4896
			New = 1U,
			// Token: 0x04001321 RID: 4897
			CreateAlways,
			// Token: 0x04001322 RID: 4898
			OpenExisting,
			// Token: 0x04001323 RID: 4899
			OpenAlways,
			// Token: 0x04001324 RID: 4900
			TruncateExisting
		}

		// Token: 0x020006C2 RID: 1730
		[Flags]
		public enum EFileAttributes : uint
		{
			// Token: 0x04001326 RID: 4902
			Readonly = 1U,
			// Token: 0x04001327 RID: 4903
			Hidden = 2U,
			// Token: 0x04001328 RID: 4904
			System = 4U,
			// Token: 0x04001329 RID: 4905
			Directory = 16U,
			// Token: 0x0400132A RID: 4906
			Archive = 32U,
			// Token: 0x0400132B RID: 4907
			Device = 64U,
			// Token: 0x0400132C RID: 4908
			Normal = 128U,
			// Token: 0x0400132D RID: 4909
			Temporary = 256U,
			// Token: 0x0400132E RID: 4910
			SparseFile = 512U,
			// Token: 0x0400132F RID: 4911
			ReparsePoint = 1024U,
			// Token: 0x04001330 RID: 4912
			Compressed = 2048U,
			// Token: 0x04001331 RID: 4913
			Offline = 4096U,
			// Token: 0x04001332 RID: 4914
			NotContentIndexed = 8192U,
			// Token: 0x04001333 RID: 4915
			Encrypted = 16384U,
			// Token: 0x04001334 RID: 4916
			WriteThrough = 2147483648U,
			// Token: 0x04001335 RID: 4917
			Overlapped = 1073741824U,
			// Token: 0x04001336 RID: 4918
			NoBuffering = 536870912U,
			// Token: 0x04001337 RID: 4919
			RandomAccess = 268435456U,
			// Token: 0x04001338 RID: 4920
			SequentialScan = 134217728U,
			// Token: 0x04001339 RID: 4921
			DeleteOnClose = 67108864U,
			// Token: 0x0400133A RID: 4922
			BackupSemantics = 33554432U,
			// Token: 0x0400133B RID: 4923
			PosixSemantics = 16777216U,
			// Token: 0x0400133C RID: 4924
			OpenReparsePoint = 2097152U,
			// Token: 0x0400133D RID: 4925
			OpenNoRecall = 1048576U,
			// Token: 0x0400133E RID: 4926
			FirstPipeInstance = 524288U,
			// Token: 0x0400133F RID: 4927
			NotFound = 4294967295U
		}
	}
}
