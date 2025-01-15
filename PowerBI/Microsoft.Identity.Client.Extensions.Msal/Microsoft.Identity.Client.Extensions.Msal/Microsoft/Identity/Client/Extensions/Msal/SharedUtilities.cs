using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x0200001F RID: 31
	public static class SharedUtilities
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003D31 File Offset: 0x00001F31
		public static bool IsWindowsPlatform()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D40 File Offset: 0x00001F40
		public static bool IsMacPlatform()
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D4C File Offset: 0x00001F4C
		public static bool IsLinuxPlatform()
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003D58 File Offset: 0x00001F58
		internal static bool IsMonoPlatform()
		{
			return SharedUtilities.s_isMono.Value;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D64 File Offset: 0x00001F64
		internal static int GetCurrentProcessId()
		{
			if (SharedUtilities.s_processId == 0)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					SharedUtilities.s_processId = currentProcess.Id;
					SharedUtilities.s_processName = currentProcess.ProcessName;
				}
			}
			return SharedUtilities.s_processId;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003DB8 File Offset: 0x00001FB8
		internal static string GetCurrentProcessName()
		{
			if (string.IsNullOrEmpty(SharedUtilities.s_processName))
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					SharedUtilities.s_processName = currentProcess.ProcessName;
					SharedUtilities.s_processId = currentProcess.Id;
				}
			}
			return SharedUtilities.s_processName;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E10 File Offset: 0x00002010
		public static string GetUserRootDirectory()
		{
			if (SharedUtilities.IsWindowsPlatform())
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			}
			return SharedUtilities.GetUserHomeDirOnUnix();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E28 File Offset: 0x00002028
		private static string GetUserHomeDirOnUnix()
		{
			if (SharedUtilities.IsWindowsPlatform())
			{
				throw new NotSupportedException();
			}
			if (!string.IsNullOrEmpty(SharedUtilities.s_homeEnvVar))
			{
				return SharedUtilities.s_homeEnvVar;
			}
			string text = null;
			if (!string.IsNullOrEmpty(SharedUtilities.s_lognameEnvVar))
			{
				text = SharedUtilities.s_lognameEnvVar;
			}
			else if (!string.IsNullOrEmpty(SharedUtilities.s_userEnvVar))
			{
				text = SharedUtilities.s_userEnvVar;
			}
			else if (!string.IsNullOrEmpty(SharedUtilities.s_lNameEnvVar))
			{
				text = SharedUtilities.s_lNameEnvVar;
			}
			else if (!string.IsNullOrEmpty(SharedUtilities.s_usernameEnvVar))
			{
				text = SharedUtilities.s_usernameEnvVar;
			}
			if (SharedUtilities.IsMacPlatform())
			{
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return Path.Combine("/Users", text);
			}
			else
			{
				if (!SharedUtilities.IsLinuxPlatform())
				{
					throw new NotSupportedException();
				}
				if (LinuxNativeMethods.getuid() == 0)
				{
					return "/root";
				}
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return Path.Combine("/home", text);
			}
		}

		// Token: 0x04000070 RID: 112
		private static readonly string s_homeEnvVar = Environment.GetEnvironmentVariable("HOME");

		// Token: 0x04000071 RID: 113
		private static readonly string s_lognameEnvVar = Environment.GetEnvironmentVariable("LOGNAME");

		// Token: 0x04000072 RID: 114
		private static readonly string s_userEnvVar = Environment.GetEnvironmentVariable("USER");

		// Token: 0x04000073 RID: 115
		private static readonly string s_lNameEnvVar = Environment.GetEnvironmentVariable("LNAME");

		// Token: 0x04000074 RID: 116
		private static readonly string s_usernameEnvVar = Environment.GetEnvironmentVariable("USERNAME");

		// Token: 0x04000075 RID: 117
		private static readonly Lazy<bool> s_isMono = new Lazy<bool>(() => Type.GetType("Mono.Runtime") != null);

		// Token: 0x04000076 RID: 118
		private static string s_processName = null;

		// Token: 0x04000077 RID: 119
		private static int s_processId = 0;
	}
}
