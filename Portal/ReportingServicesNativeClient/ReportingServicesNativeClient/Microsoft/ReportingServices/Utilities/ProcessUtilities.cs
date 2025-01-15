using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Utilities
{
	// Token: 0x02000028 RID: 40
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class ProcessUtilities
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000099BC File Offset: 0x00008DBC
		public unsafe static int LP2PP()
		{
			ulong num = 0UL;
			num = ((<Module>.GetProcessorInformation(&num) < 0) ? 1UL : num);
			return (int)num;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000099E4 File Offset: 0x00008DE4
		public unsafe static long GetProcessTimes()
		{
			int num = 0;
			void* currentProcess = <Module>.GetCurrentProcess();
			_FILETIME filetime;
			_FILETIME filetime2;
			_FILETIME filetime3;
			_FILETIME filetime4;
			long num3;
			if (<Module>.GetProcessTimes(currentProcess, &filetime, &filetime2, &filetime3, &filetime4) == null)
			{
				uint lastError = <Module>.GetLastError();
				num = ((lastError > 0) ? ((lastError & 65535) | -2147024896) : lastError);
			}
			else
			{
				long num2 = ProcessUtilities.CombineFileTime(filetime3);
				num3 = ProcessUtilities.CombineFileTime(filetime4) + num2;
			}
			<Module>.CloseHandle(currentProcess);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return num3;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000098EC File Offset: 0x00008CEC
		public unsafe static ulong GetProcessAffinity()
		{
			int num = 0;
			ulong num2 = 0UL;
			ulong num3 = 0UL;
			void* currentProcess = <Module>.GetCurrentProcess();
			if (<Module>.GetProcessAffinityMask(currentProcess, &num2, &num3) == null)
			{
				uint lastError = <Module>.GetLastError();
				num = ((lastError > 0) ? ((lastError & 65535) | -2147024896) : lastError);
			}
			<Module>.CloseHandle(currentProcess);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return num2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009948 File Offset: 0x00008D48
		public unsafe static void SetProcessAffinity(ulong affinityMask)
		{
			int num = 0;
			void* currentProcess = <Module>.GetCurrentProcess();
			if (<Module>.SetProcessAffinityMask(currentProcess, affinityMask) == null)
			{
				uint lastError = <Module>.GetLastError();
				num = ((lastError > 0) ? ((lastError & 65535) | -2147024896) : lastError);
			}
			<Module>.CloseHandle(currentProcess);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00009998 File Offset: 0x00008D98
		private unsafe static long CombineFileTime(_FILETIME fileTime)
		{
			return (long)(((ulong)(*((ref fileTime) + 4)) << 32) | fileTime);
		}
	}
}
