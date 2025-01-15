using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000101 RID: 257
	internal struct AuxiliaryPermissionInfo
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000328D7 File Offset: 0x00030AD7
		internal bool IsValid
		{
			get
			{
				return !this.RequireServiceToServiceToken || !string.IsNullOrEmpty(this.ServiceToServiceToken);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000328F1 File Offset: 0x00030AF1
		internal bool RequireServiceToServiceToken
		{
			get
			{
				return this.ApplyAuxiliaryPermission || this.IntendedUsage != 0 || this.SkipThrottling || this.BypassBuildPermission;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00032913 File Offset: 0x00030B13
		internal bool SkipThrottling
		{
			get
			{
				return !string.IsNullOrEmpty(this.SourceCapacityObjectId);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00032923 File Offset: 0x00030B23
		internal bool HasServicePrincipalProfile
		{
			get
			{
				return !string.IsNullOrEmpty(this.ServicePrincipalProfileId);
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00032933 File Offset: 0x00030B33
		internal void Validate()
		{
			if (!this.IsValid)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_S2STokenMissing);
			}
		}

		// Token: 0x04000888 RID: 2184
		private const int DefaultIntendedUsage = 0;

		// Token: 0x04000889 RID: 2185
		public bool ApplyAuxiliaryPermission;

		// Token: 0x0400088A RID: 2186
		public string AuxiliaryPermissionOwner;

		// Token: 0x0400088B RID: 2187
		public string ServiceToServiceToken;

		// Token: 0x0400088C RID: 2188
		public int IntendedUsage;

		// Token: 0x0400088D RID: 2189
		public string SourceCapacityObjectId;

		// Token: 0x0400088E RID: 2190
		public string ServicePrincipalProfileId;

		// Token: 0x0400088F RID: 2191
		public bool BypassBuildPermission;
	}
}
