using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000016 RID: 22
	internal static class Native
	{
		// Token: 0x060000D5 RID: 213
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool GlobalMemoryStatusEx(ref Native.MemoryStatusEx memstatus);

		// Token: 0x060000D6 RID: 214
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LookupAccountName(string lpSystemName, string lpAccountName, SafeLocalFree Sid, [In] [Out] ref uint cbSid, SafeLocalFree DomainName, [In] [Out] ref uint cbDomainName, out uint peUse);

		// Token: 0x060000D7 RID: 215
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LookupAccountSid(string lpSystemName, byte[] pSid, SafeLocalFree lpAccountName, [In] [Out] ref uint cchName, SafeLocalFree DomainName, [In] [Out] ref uint cchDomainName, out uint peUse);

		// Token: 0x060000D8 RID: 216
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CloseHandle(IntPtr hDevice);

		// Token: 0x060000D9 RID: 217
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CheckTokenMembership(IntPtr token, SafeSidPtr sidToCheck, out bool isMember);

		// Token: 0x060000DA RID: 218
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int LogonUser(string userName, string domain, string password, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		// Token: 0x060000DB RID: 219
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int EqualSid(IntPtr sid1, IntPtr sid2);

		// Token: 0x060000DC RID: 220
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetLengthSid(IntPtr pSid);

		// Token: 0x060000DD RID: 221
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string fileName, [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess, [MarshalAs(UnmanagedType.U4)] FileShare fileShare, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] Native.NativeFileAttributes flagsAndAttributes, IntPtr template);

		// Token: 0x060000DE RID: 222 RVA: 0x00006264 File Offset: 0x00004464
		internal static int NativeCreateFile(string fileName, FileMode mode, FileAccess access, FileShare sharing, out SafeFileHandle handle)
		{
			RSTrace.CatalogTrace.Assert(mode != FileMode.Append, "FileMode.Append is not supported");
			RSTrace.CatalogTrace.Assert((sharing & FileShare.Inheritable) != FileShare.Inheritable, "FileShare.Inheritable is not supported");
			int num = 0;
			handle = Native.CreateFile(fileName, access, sharing, IntPtr.Zero, mode, Native.NativeFileAttributes.SequentialScan, IntPtr.Zero);
			if (handle.IsInvalid)
			{
				num = Marshal.GetLastWin32Error();
			}
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000062D0 File Offset: 0x000044D0
		internal static bool IsAdmin(IntPtr userToken)
		{
			SafeSidPtr safeSidPtr = null;
			bool flag = false;
			bool flag2;
			try
			{
				safeSidPtr = Native.GetAdminSid();
				if (!Native.CheckTokenMembership(userToken, safeSidPtr, out flag))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					string text = string.Format("CheckTokenMembership: Win32 error:{0}, can't validate the user with sid:{1} is admin", lastWin32Error, safeSidPtr);
					RSTrace.CatalogTrace.TraceException(TraceLevel.Error, text);
					flag2 = false;
				}
				else
				{
					flag2 = flag;
				}
			}
			finally
			{
				if (safeSidPtr != null)
				{
					safeSidPtr.Close();
				}
			}
			return flag2;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006340 File Offset: 0x00004540
		internal static byte[] NameToSid(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new UnknownUserNameException(name);
			}
			SafeLocalFree safeLocalFree = null;
			int num = 0;
			byte[] array2;
			try
			{
				safeLocalFree = Native.GetSid(name, out num);
				if (safeLocalFree == null)
				{
					throw new UnknownUserNameException(name);
				}
				byte[] array = new byte[num];
				Marshal.Copy(safeLocalFree.DangerousGetHandle(), array, 0, num);
				array2 = array;
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return array2;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000063AC File Offset: 0x000045AC
		private static bool ExpectedLookupAccountError(int err)
		{
			return err == 1332 || err == 1788 || err == 1789 || err == 87 || err == 1727;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000063D8 File Offset: 0x000045D8
		internal static SafeLocalFree GetSid(string name, out int length)
		{
			length = 0;
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree3;
			try
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				if (!Native.LookupAccountName(null, name, SafeLocalFree.Zero, ref num, SafeLocalFree.Zero, ref num2, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (num4 != 122 && !Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num4));
					}
					if (Native.ExpectedLookupAccountError(num4))
					{
						return null;
					}
				}
				safeLocalFree = SafeLocalFree.LocalAlloc((int)(num2 * 2U));
				SafeLocalFree safeLocalFree2 = SafeLocalFree.LocalAlloc((int)num);
				if (!Native.LookupAccountName(null, name, safeLocalFree2, ref num, safeLocalFree, ref num2, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (safeLocalFree2 != null)
					{
						safeLocalFree2.Close();
					}
					if (!Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num4));
					}
					safeLocalFree3 = null;
				}
				else
				{
					length = (int)num;
					safeLocalFree3 = safeLocalFree2;
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return safeLocalFree3;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000064C8 File Offset: 0x000046C8
		private static SafeSidPtr GetAdminSid()
		{
			SafeSidPtr safeSidPtr = null;
			if (!SafeSidPtr.AllocateAndInitializeSid(2, 32U, 544U, 0U, 0U, 0U, 0U, 0U, 0U, out safeSidPtr))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AllocateAndInitializeSid: Win32 error:{0}", lastWin32Error));
			}
			return safeSidPtr;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006510 File Offset: 0x00004710
		internal static string GetSystemUserName()
		{
			return Native.GetUserNameFromSid(Native.GetSystemSid());
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000651C File Offset: 0x0000471C
		internal static string GetUserNameFromSid(byte[] sid)
		{
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree2 = null;
			string text;
			try
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				if (!Native.LookupAccountSid(null, sid, SafeLocalFree.Zero, ref num2, SafeLocalFree.Zero, ref num, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (num4 != 122 && !Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountSid: Win32 error:{0}", num4));
					}
					if (Native.ExpectedLookupAccountError(num4))
					{
						return null;
					}
				}
				safeLocalFree = SafeLocalFree.LocalAlloc((int)(num * 2U));
				safeLocalFree2 = SafeLocalFree.LocalAlloc((int)(num2 * 2U));
				if (!Native.LookupAccountSid(null, sid, safeLocalFree2, ref num2, safeLocalFree, ref num, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (!Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num4));
					}
					text = null;
				}
				else
				{
					string text2 = Marshal.PtrToStringUni(safeLocalFree2.DangerousGetHandle());
					string text3 = Marshal.PtrToStringUni(safeLocalFree.DangerousGetHandle());
					if (string.IsNullOrEmpty(text3))
					{
						text = text2;
					}
					else
					{
						text = text3 + "\\" + text2;
					}
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
			}
			return text;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006640 File Offset: 0x00004840
		internal static byte[] GetSystemSid()
		{
			SafeSidPtr safeSidPtr = null;
			byte[] array2;
			try
			{
				if (!SafeSidPtr.AllocateAndInitializeSid(1, 18U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, out safeSidPtr))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AllocateAndInitializeSid: Win32 error:{0}", lastWin32Error));
				}
				int lengthSid = Native.GetLengthSid(safeSidPtr.DangerousGetHandle());
				byte[] array = new byte[lengthSid];
				Marshal.Copy(safeSidPtr.DangerousGetHandle(), array, 0, lengthSid);
				array2 = array;
			}
			finally
			{
				if (safeSidPtr != null)
				{
					safeSidPtr.Close();
				}
			}
			return array2;
		}

		// Token: 0x040000B9 RID: 185
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x040000BA RID: 186
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x040000BB RID: 187
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040000BC RID: 188
		private const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x040000BD RID: 189
		private const int ERROR_TRUSTED_DOMAIN_FAILURE = 1788;

		// Token: 0x040000BE RID: 190
		private const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x040000BF RID: 191
		private const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x040000C0 RID: 192
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x040000C1 RID: 193
		private const int RPC_S_CALL_FAILED_DNE = 1727;

		// Token: 0x040000C2 RID: 194
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x040000C3 RID: 195
		internal const uint TOKEN_QUERY = 8U;

		// Token: 0x040000C4 RID: 196
		internal const uint TOKEN_IMPERSONATE = 4U;

		// Token: 0x040000C5 RID: 197
		private const uint LMEM_FIXED = 0U;

		// Token: 0x040000C6 RID: 198
		private const uint LMEM_ZEROINIT = 64U;

		// Token: 0x040000C7 RID: 199
		private const uint LPTR = 64U;

		// Token: 0x040000C8 RID: 200
		internal static readonly int SqlConstraintViolationCode = 547;

		// Token: 0x040000C9 RID: 201
		internal static readonly int SqlUniqueIndexViolationCode = 2601;

		// Token: 0x040000CA RID: 202
		internal static readonly int SqlTransactionAbortedCode = 3903;

		// Token: 0x040000CB RID: 203
		internal static readonly int SqlAdHocErrorCode = 50000;

		// Token: 0x040000CC RID: 204
		internal static readonly int SqlUniqueConstraintViolationCode = 2627;

		// Token: 0x040000CD RID: 205
		public const int SIZEOF_MemoryStatusEx = 64;

		// Token: 0x040000CE RID: 206
		public const int LOGON32_LOGON_INTERACTIVE = 2;

		// Token: 0x040000CF RID: 207
		public const int LOGON32_PROVIDER_DEFAULT = 0;

		// Token: 0x0200004F RID: 79
		public struct MemoryStatusEx
		{
			// Token: 0x040001EB RID: 491
			public int dwLength;

			// Token: 0x040001EC RID: 492
			public int dwMemoryLoad;

			// Token: 0x040001ED RID: 493
			public ulong ullTotalPhys;

			// Token: 0x040001EE RID: 494
			public ulong ullAvailPhys;

			// Token: 0x040001EF RID: 495
			public ulong ullTotalPageFile;

			// Token: 0x040001F0 RID: 496
			public ulong ullAvailPageFile;

			// Token: 0x040001F1 RID: 497
			public ulong ullTotalVirtual;

			// Token: 0x040001F2 RID: 498
			public ulong ullAvailVirtual;

			// Token: 0x040001F3 RID: 499
			public ulong ullAvailExtendedVirtual;
		}

		// Token: 0x02000050 RID: 80
		[Flags]
		private enum NativeFileAttributes : uint
		{
			// Token: 0x040001F5 RID: 501
			Readonly = 1U,
			// Token: 0x040001F6 RID: 502
			Hidden = 2U,
			// Token: 0x040001F7 RID: 503
			System = 4U,
			// Token: 0x040001F8 RID: 504
			Directory = 16U,
			// Token: 0x040001F9 RID: 505
			Archive = 32U,
			// Token: 0x040001FA RID: 506
			Device = 64U,
			// Token: 0x040001FB RID: 507
			Normal = 128U,
			// Token: 0x040001FC RID: 508
			Temporary = 256U,
			// Token: 0x040001FD RID: 509
			SparseFile = 512U,
			// Token: 0x040001FE RID: 510
			ReparsePoint = 1024U,
			// Token: 0x040001FF RID: 511
			Compressed = 2048U,
			// Token: 0x04000200 RID: 512
			Offline = 4096U,
			// Token: 0x04000201 RID: 513
			NotContentIndexed = 8192U,
			// Token: 0x04000202 RID: 514
			Encrypted = 16384U,
			// Token: 0x04000203 RID: 515
			Write_Through = 2147483648U,
			// Token: 0x04000204 RID: 516
			Overlapped = 1073741824U,
			// Token: 0x04000205 RID: 517
			NoBuffering = 536870912U,
			// Token: 0x04000206 RID: 518
			RandomAccess = 268435456U,
			// Token: 0x04000207 RID: 519
			SequentialScan = 134217728U,
			// Token: 0x04000208 RID: 520
			DeleteOnClose = 67108864U,
			// Token: 0x04000209 RID: 521
			BackupSemantics = 33554432U,
			// Token: 0x0400020A RID: 522
			PosixSemantics = 16777216U,
			// Token: 0x0400020B RID: 523
			OpenReparsePoint = 2097152U,
			// Token: 0x0400020C RID: 524
			OpenNoRecall = 1048576U,
			// Token: 0x0400020D RID: 525
			FirstPipeInstance = 524288U
		}
	}
}
