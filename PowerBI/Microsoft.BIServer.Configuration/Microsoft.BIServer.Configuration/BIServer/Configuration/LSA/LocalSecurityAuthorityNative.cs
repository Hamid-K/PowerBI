using System;
using System.Runtime.InteropServices;

namespace Microsoft.BIServer.Configuration.LSA
{
	// Token: 0x02000034 RID: 52
	internal static class LocalSecurityAuthorityNative
	{
		// Token: 0x060001CC RID: 460
		[DllImport("advapi32.dll")]
		public static extern uint LsaOpenPolicy(IntPtr systemName, ref LocalSecurityAuthorityNative.LSA_OBJECT_ATTRIBUTES objectAttributes, uint desiredAccess, out IntPtr policyHandle);

		// Token: 0x060001CD RID: 461
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern uint LsaAddAccountRights(IntPtr policyHandle, [MarshalAs(UnmanagedType.LPArray)] byte[] sid, LocalSecurityAuthorityNative.LSA_UNICODE_STRING[] userRights, uint countOfRights);

		// Token: 0x060001CE RID: 462
		[DllImport("advapi32.dll")]
		public static extern long LsaClose(IntPtr objectHandle);

		// Token: 0x060001CF RID: 463
		[DllImport("advapi32.dll")]
		public static extern uint LsaNtStatusToWinError(uint status);

		// Token: 0x02000059 RID: 89
		[Flags]
		public enum LSA_ACCESS_POLICY : uint
		{
			// Token: 0x0400020F RID: 527
			POLICY_VIEW_LOCAL_INFORMATION = 1U,
			// Token: 0x04000210 RID: 528
			POLICY_VIEW_AUDIT_INFORMATION = 2U,
			// Token: 0x04000211 RID: 529
			POLICY_GET_PRIVATE_INFORMATION = 4U,
			// Token: 0x04000212 RID: 530
			POLICY_TRUST_ADMIN = 8U,
			// Token: 0x04000213 RID: 531
			POLICY_CREATE_ACCOUNT = 16U,
			// Token: 0x04000214 RID: 532
			POLICY_CREATE_SECRET = 32U,
			// Token: 0x04000215 RID: 533
			POLICY_CREATE_PRIVILEGE = 64U,
			// Token: 0x04000216 RID: 534
			POLICY_SET_DEFAULT_QUOTA_LIMITS = 128U,
			// Token: 0x04000217 RID: 535
			POLICY_SET_AUDIT_REQUIREMENTS = 256U,
			// Token: 0x04000218 RID: 536
			POLICY_AUDIT_LOG_ADMIN = 512U,
			// Token: 0x04000219 RID: 537
			POLICY_SERVER_ADMIN = 1024U,
			// Token: 0x0400021A RID: 538
			POLICY_LOOKUP_NAMES = 2048U,
			// Token: 0x0400021B RID: 539
			POLICY_NOTIFICATION = 4096U
		}

		// Token: 0x0200005A RID: 90
		public struct LSA_OBJECT_ATTRIBUTES
		{
			// Token: 0x0400021C RID: 540
			public int Length;

			// Token: 0x0400021D RID: 541
			public IntPtr RootDirectory;

			// Token: 0x0400021E RID: 542
			public readonly LocalSecurityAuthorityNative.LSA_UNICODE_STRING ObjectName;

			// Token: 0x0400021F RID: 543
			public uint Attributes;

			// Token: 0x04000220 RID: 544
			public IntPtr SecurityDescriptor;

			// Token: 0x04000221 RID: 545
			public IntPtr SecurityQualityOfService;
		}

		// Token: 0x0200005B RID: 91
		public struct LSA_UNICODE_STRING
		{
			// Token: 0x04000222 RID: 546
			public ushort Length;

			// Token: 0x04000223 RID: 547
			public ushort MaximumLength;

			// Token: 0x04000224 RID: 548
			public IntPtr Buffer;
		}
	}
}
