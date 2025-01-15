using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C14 RID: 7188
	public static class ProcessHelpers
	{
		// Token: 0x0600B361 RID: 45921 RVA: 0x00247A71 File Offset: 0x00245C71
		static ProcessHelpers()
		{
			IntPtr currentProcess = ProcessHelpers.GetCurrentProcess();
			ProcessHelpers.DuplicateHandle(currentProcess, currentProcess, currentProcess, out ProcessHelpers.processHandle, 0U, true, 2U);
		}

		// Token: 0x17002CF0 RID: 11504
		// (get) Token: 0x0600B362 RID: 45922 RVA: 0x00247A92 File Offset: 0x00245C92
		public static SafeHandle ProcessHandle
		{
			get
			{
				return ProcessHelpers.processHandle;
			}
		}

		// Token: 0x17002CF1 RID: 11505
		// (get) Token: 0x0600B363 RID: 45923 RVA: 0x00247A99 File Offset: 0x00245C99
		public static IntPtr CurrentProcess
		{
			get
			{
				return ProcessHelpers.GetCurrentProcess();
			}
		}

		// Token: 0x17002CF2 RID: 11506
		// (get) Token: 0x0600B364 RID: 45924 RVA: 0x00247AA0 File Offset: 0x00245CA0
		public static uint CurrentProcessId
		{
			get
			{
				return ProcessHelpers.GetCurrentProcessId();
			}
		}

		// Token: 0x0600B365 RID: 45925 RVA: 0x00247AA8 File Offset: 0x00245CA8
		public static SafeHandle GetThreadToken()
		{
			ProcessHelpers.Handle handle;
			if (ProcessHelpers.OpenThreadToken(ProcessHelpers.GetCurrentThread(), 131086U, false, out handle))
			{
				return handle;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 1008)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return null;
		}

		// Token: 0x0600B366 RID: 45926 RVA: 0x00247AE4 File Offset: 0x00245CE4
		public static SafeHandle DuplicateHandle(SafeHandle handle)
		{
			if (handle == null)
			{
				return handle;
			}
			IntPtr currentProcess = ProcessHelpers.GetCurrentProcess();
			ProcessHelpers.Handle handle2;
			if (!ProcessHelpers.DuplicateHandle(currentProcess, handle.DangerousGetHandle(), currentProcess, out handle2, 0U, false, 2U))
			{
				throw new Win32Exception();
			}
			return handle2;
		}

		// Token: 0x0600B367 RID: 45927 RVA: 0x00247B18 File Offset: 0x00245D18
		public static SafeHandle DuplicateRemoteHandle(IntPtr remoteProcess, IntPtr remoteHandle)
		{
			ProcessHelpers.Handle handle;
			if (!ProcessHelpers.DuplicateHandle(remoteProcess, remoteHandle, ProcessHelpers.GetCurrentProcess(), out handle, 0U, false, 2U))
			{
				throw new Win32Exception();
			}
			return handle;
		}

		// Token: 0x0600B368 RID: 45928 RVA: 0x00247B40 File Offset: 0x00245D40
		public static SafeHandle DuplicateRemoteHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle)
		{
			ProcessHelpers.Handle handle;
			if (!ProcessHelpers.DuplicateHandle(hSourceProcessHandle, hSourceHandle, hTargetProcessHandle, out handle, 0U, false, 2U))
			{
				throw new Win32Exception();
			}
			return handle;
		}

		// Token: 0x0600B369 RID: 45929 RVA: 0x00247B64 File Offset: 0x00245D64
		public static SafeHandle LogonUser(string username, string password)
		{
			string domain = ProcessHelpers.GetDomain(ref username);
			ProcessHelpers.Handle handle;
			if (!ProcessHelpers.LogonUser(username, domain, password, 8, 3, out handle))
			{
				throw new Win32Exception();
			}
			return handle;
		}

		// Token: 0x0600B36A RID: 45930 RVA: 0x00247B90 File Offset: 0x00245D90
		private static string GetDomain(ref string username)
		{
			int num = username.IndexOf('\\');
			string text;
			if (num > 0)
			{
				text = username.Substring(0, num);
				username = username.Substring(num + 1);
			}
			else if (username.IndexOf('@') > 0)
			{
				text = null;
			}
			else
			{
				text = Environment.MachineName;
			}
			return text;
		}

		// Token: 0x0600B36B RID: 45931 RVA: 0x00247BDA File Offset: 0x00245DDA
		public static SafeHandle OpenProcess(uint processId)
		{
			return ProcessHelpers.OpenProcess(ProcessHelpers.PROCESS_INFO, false, processId);
		}

		// Token: 0x0600B36C RID: 45932
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool OpenThreadToken(IntPtr ThreadHandle, uint DesiredAccess, bool OpenAsSelf, out ProcessHelpers.Handle TokenHandle);

		// Token: 0x0600B36D RID: 45933
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentThread();

		// Token: 0x0600B36E RID: 45934
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x0600B36F RID: 45935
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentProcessId();

		// Token: 0x0600B370 RID: 45936
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern ProcessHelpers.Handle OpenProcess(uint DesiredAccess, bool InheritHandle, uint ProcessId);

		// Token: 0x0600B371 RID: 45937
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600B372 RID: 45938
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out ProcessHelpers.Handle lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

		// Token: 0x0600B373 RID: 45939
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword, int dwLogonType, int dwLogonProvider, out ProcessHelpers.Handle phToken);

		// Token: 0x04005B7A RID: 23418
		private const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;

		// Token: 0x04005B7B RID: 23419
		private const int LOGON32_PROVIDER_WINNT50 = 3;

		// Token: 0x04005B7C RID: 23420
		private const int TOKEN_READ = 131080;

		// Token: 0x04005B7D RID: 23421
		private const int TOKEN_DUPLICATE = 2;

		// Token: 0x04005B7E RID: 23422
		private const int TOKEN_IMPERSONATE = 4;

		// Token: 0x04005B7F RID: 23423
		private const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04005B80 RID: 23424
		private const uint DUPLICATE_SAME_ACCESS = 2U;

		// Token: 0x04005B81 RID: 23425
		private const uint PROCESS_DUP_HANDLE = 64U;

		// Token: 0x04005B82 RID: 23426
		private const uint PROCESS_QUERY_LIMITED_INFORMATION = 4096U;

		// Token: 0x04005B83 RID: 23427
		private static uint PROCESS_INFO = 4160U;

		// Token: 0x04005B84 RID: 23428
		private static readonly ProcessHelpers.Handle processHandle;

		// Token: 0x02001C15 RID: 7189
		private sealed class Handle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600B374 RID: 45940 RVA: 0x00247BE8 File Offset: 0x00245DE8
			private Handle()
				: base(true)
			{
				this.handle = IntPtr.Zero;
			}

			// Token: 0x0600B375 RID: 45941 RVA: 0x00247BFC File Offset: 0x00245DFC
			protected override bool ReleaseHandle()
			{
				return ProcessHelpers.CloseHandle(this.handle);
			}
		}
	}
}
