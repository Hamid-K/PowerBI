using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000003 RID: 3
	[Guid("96A43E91-1969-406E-BED8-7CAB51F11810")]
	[ComVisible(true)]
	public struct AuxiliaryPermissionInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		internal bool IsValid
		{
			get
			{
				return !this.RequireServiceToServiceToken || !string.IsNullOrEmpty(this.ServiceToServiceToken);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002176 File Offset: 0x00000376
		internal bool RequireServiceToServiceToken
		{
			get
			{
				return this.ApplyAuxiliaryPermission || this.IntendedUsage != 0 || this.SkipThrottling || this.BypassBuildPermission;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002198 File Offset: 0x00000398
		internal bool SkipThrottling
		{
			get
			{
				return !string.IsNullOrEmpty(this.SourceCapacityObjectId);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
		internal bool HasServicePrincipalProfile
		{
			get
			{
				return !string.IsNullOrEmpty(this.ServicePrincipalProfileId);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
		internal void Validate()
		{
			if (!this.IsValid)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_S2STokenMissing);
			}
		}

		// Token: 0x04000003 RID: 3
		private const int DefaultIntendedUsage = 0;

		// Token: 0x04000004 RID: 4
		[MarshalAs(UnmanagedType.Bool)]
		public bool ApplyAuxiliaryPermission;

		// Token: 0x04000005 RID: 5
		[MarshalAs(UnmanagedType.BStr)]
		public string AuxiliaryPermissionOwner;

		// Token: 0x04000006 RID: 6
		[MarshalAs(UnmanagedType.BStr)]
		public string ServiceToServiceToken;

		// Token: 0x04000007 RID: 7
		[MarshalAs(UnmanagedType.I4)]
		public int IntendedUsage;

		// Token: 0x04000008 RID: 8
		[MarshalAs(UnmanagedType.BStr)]
		public string SourceCapacityObjectId;

		// Token: 0x04000009 RID: 9
		[MarshalAs(UnmanagedType.BStr)]
		public string ServicePrincipalProfileId;

		// Token: 0x0400000A RID: 10
		[MarshalAs(UnmanagedType.Bool)]
		public bool BypassBuildPermission;
	}
}
