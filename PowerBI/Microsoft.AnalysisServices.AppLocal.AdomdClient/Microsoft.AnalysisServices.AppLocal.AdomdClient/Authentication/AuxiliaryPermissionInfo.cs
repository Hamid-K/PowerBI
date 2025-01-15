using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000101 RID: 257
	internal struct AuxiliaryPermissionInfo
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00032C07 File Offset: 0x00030E07
		internal bool IsValid
		{
			get
			{
				return !this.RequireServiceToServiceToken || !string.IsNullOrEmpty(this.ServiceToServiceToken);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00032C21 File Offset: 0x00030E21
		internal bool RequireServiceToServiceToken
		{
			get
			{
				return this.ApplyAuxiliaryPermission || this.IntendedUsage != 0 || this.SkipThrottling || this.BypassBuildPermission;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00032C43 File Offset: 0x00030E43
		internal bool SkipThrottling
		{
			get
			{
				return !string.IsNullOrEmpty(this.SourceCapacityObjectId);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00032C53 File Offset: 0x00030E53
		internal bool HasServicePrincipalProfile
		{
			get
			{
				return !string.IsNullOrEmpty(this.ServicePrincipalProfileId);
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00032C63 File Offset: 0x00030E63
		internal void Validate()
		{
			if (!this.IsValid)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_S2STokenMissing);
			}
		}

		// Token: 0x04000895 RID: 2197
		private const int DefaultIntendedUsage = 0;

		// Token: 0x04000896 RID: 2198
		public bool ApplyAuxiliaryPermission;

		// Token: 0x04000897 RID: 2199
		public string AuxiliaryPermissionOwner;

		// Token: 0x04000898 RID: 2200
		public string ServiceToServiceToken;

		// Token: 0x04000899 RID: 2201
		public int IntendedUsage;

		// Token: 0x0400089A RID: 2202
		public string SourceCapacityObjectId;

		// Token: 0x0400089B RID: 2203
		public string ServicePrincipalProfileId;

		// Token: 0x0400089C RID: 2204
		public bool BypassBuildPermission;
	}
}
