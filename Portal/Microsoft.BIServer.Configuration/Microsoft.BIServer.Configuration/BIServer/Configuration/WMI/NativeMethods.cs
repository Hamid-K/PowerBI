using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x0200002E RID: 46
	[CLSCompliant(false)]
	public class NativeMethods
	{
		// Token: 0x0600018C RID: 396
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor([In] string StringSecurityDescriptor, [In] uint StringSDRevision, out IntPtr SecurityDescriptor, out int SecurityDescriptorSize);

		// Token: 0x0600018D RID: 397 RVA: 0x0000664C File Offset: 0x0000484C
		public static void ConvertStringSDtoSD(string stringSecurityDescriptor, out IntPtr securityDescriptorPtr, out int securityDescriptorSize)
		{
			if (!NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, 1U, out securityDescriptorPtr, out securityDescriptorSize))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600018E RID: 398
		[DllImport("advapi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetSecurityDescriptorControl(byte[] pSecurityDescriptor, out ushort pControl, out uint lpdwRevision);

		// Token: 0x0600018F RID: 399
		[DllImport("advapi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetSecurityDescriptorControl(IntPtr pSecurityDescriptor, out ushort pControl, out uint lpdwRevision);

		// Token: 0x06000190 RID: 400
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetSecurityDescriptorControl(IntPtr pSecurityDescriptor, SecurityDescriptorControlFlags ControlBitsOfInterest, SecurityDescriptorControlFlags ControlBitsToSet);

		// Token: 0x06000191 RID: 401 RVA: 0x00006664 File Offset: 0x00004864
		public static void ConvertSDtoStringSD(byte[] securityDescriptor, out IntPtr stringSecurityDescriptorPtr, out int stringSecurityDescriptorSize)
		{
			if (!NativeMethods.ConvertSecurityDescriptorToStringSecurityDescriptor(securityDescriptor, 1, (NativeMethods.SecurityInformation)15U, out stringSecurityDescriptorPtr, out stringSecurityDescriptorSize))
			{
				Console.WriteLine("Fail to convert SD to string SD:");
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06000192 RID: 402
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor([In] byte[] SecurityDescriptor, [In] int RequestedStringSDRevision, [In] NativeMethods.SecurityInformation SecurityInformation, out IntPtr StringSecurityDescriptor, out int StringSecurityDescriptorLen);

		// Token: 0x06000193 RID: 403
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref NativeMethods.SYSTEM_INFO sysInfo);

		// Token: 0x06000194 RID: 404
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool GetSecurityDescriptorDacl(IntPtr pSecurityDescriptor, out bool daclPresent, out IntPtr pDacl, out bool daclDefaulted);

		// Token: 0x06000195 RID: 405
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint GetExplicitEntriesFromAcl(IntPtr pacl, out uint nACE, out IntPtr ppEA);

		// Token: 0x06000196 RID: 406
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint SetEntriesInAcl(uint cCountOfExplicitEntries, IntPtr pListOfExplicitEntries, IntPtr OldAcl, out IntPtr NewAcl);

		// Token: 0x06000197 RID: 407
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool InitializeSecurityDescriptor(IntPtr pSecurityDescriptor, uint dwRevision);

		// Token: 0x06000198 RID: 408
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool SetSecurityDescriptorDacl(IntPtr pSD, bool daclPresent, IntPtr dacl, bool daclDefaulted);

		// Token: 0x06000199 RID: 409 RVA: 0x00006688 File Offset: 0x00004888
		public static string ConvertSDtoStringSD(IntPtr pSD)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			if (!NativeMethods.ConvertSecurityDescriptorToStringSecurityDescriptor(pSD, 1, (NativeMethods.SecurityInformation)15U, out zero, out num))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return Marshal.PtrToStringAuto(zero);
		}

		// Token: 0x0600019A RID: 410
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor([In] IntPtr pSecurityDescriptor, [In] int RequestedStringSDRevision, [In] NativeMethods.SecurityInformation SecurityInformation, out IntPtr StringSecurityDescriptor, out int StringSecurityDescriptorLen);

		// Token: 0x04000126 RID: 294
		public const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;

		// Token: 0x04000127 RID: 295
		public const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;

		// Token: 0x04000128 RID: 296
		public const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;

		// Token: 0x04000129 RID: 297
		public const int ERROR_SUCCESS = 0;

		// Token: 0x0400012A RID: 298
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400012B RID: 299
		public const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400012C RID: 300
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400012D RID: 301
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400012E RID: 302
		public const int ERROR_INVALID_NAME = 123;

		// Token: 0x0400012F RID: 303
		public const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x04000130 RID: 304
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000131 RID: 305
		public const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000132 RID: 306
		public const int ERROR_MORE_DATA = 234;

		// Token: 0x04000133 RID: 307
		public const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000134 RID: 308
		public const uint SECURITY_DESCRIPTOR_REVISION = 1U;

		// Token: 0x02000054 RID: 84
		internal enum SecurityInformation : uint
		{
			// Token: 0x040001EC RID: 492
			OWNER_SECURITY_INFORMATION = 1U,
			// Token: 0x040001ED RID: 493
			GROUP_SECURITY_INFORMATION,
			// Token: 0x040001EE RID: 494
			DACL_SECURITY_INFORMATION = 4U,
			// Token: 0x040001EF RID: 495
			SACL_SECURITY_INFORMATION = 8U,
			// Token: 0x040001F0 RID: 496
			PROTECTED_DACL_SECURITY_INFORMATION = 2147483648U,
			// Token: 0x040001F1 RID: 497
			PROTECTED_SACL_SECURITY_INFORMATION = 1073741824U,
			// Token: 0x040001F2 RID: 498
			UNPROTECTED_DACL_SECURITY_INFORMATION = 536870912U,
			// Token: 0x040001F3 RID: 499
			UNPROTECTED_SACL_SECURITY_INFORMATION = 268435456U
		}

		// Token: 0x02000055 RID: 85
		internal struct SYSTEM_INFO
		{
			// Token: 0x040001F4 RID: 500
			public PROCESSOR_INFO uProcessorInfo;

			// Token: 0x040001F5 RID: 501
			public uint dwPageSize;

			// Token: 0x040001F6 RID: 502
			public IntPtr lpMinimumApplicationAddress;

			// Token: 0x040001F7 RID: 503
			public IntPtr lpMaximumApplicationAddress;

			// Token: 0x040001F8 RID: 504
			public IntPtr dwActiveProcessorMask;

			// Token: 0x040001F9 RID: 505
			public uint dwNumberOfProcessors;

			// Token: 0x040001FA RID: 506
			public uint dwProcessorType;

			// Token: 0x040001FB RID: 507
			public uint dwAllocationGranularity;

			// Token: 0x040001FC RID: 508
			public uint dwProcessorLevel;

			// Token: 0x040001FD RID: 509
			public uint dwProcessorRevision;
		}

		// Token: 0x02000056 RID: 86
		internal struct SECURITY_DESCRIPTOR
		{
			// Token: 0x040001FE RID: 510
			private byte Revision;

			// Token: 0x040001FF RID: 511
			private byte Sbz1;

			// Token: 0x04000200 RID: 512
			private ushort Control;

			// Token: 0x04000201 RID: 513
			private IntPtr Owner;

			// Token: 0x04000202 RID: 514
			private IntPtr Group;

			// Token: 0x04000203 RID: 515
			private IntPtr Sacl;

			// Token: 0x04000204 RID: 516
			private IntPtr Dacl;
		}
	}
}
