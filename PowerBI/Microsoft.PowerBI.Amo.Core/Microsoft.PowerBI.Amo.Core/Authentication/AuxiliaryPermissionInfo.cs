using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F6 RID: 246
	internal struct AuxiliaryPermissionInfo
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00035524 File Offset: 0x00033724
		internal bool IsValid
		{
			get
			{
				return !this.RequireServiceToServiceToken || !string.IsNullOrEmpty(this.ServiceToServiceToken);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003553E File Offset: 0x0003373E
		internal bool RequireServiceToServiceToken
		{
			get
			{
				return this.ApplyAuxiliaryPermission || this.IntendedUsage != 0 || this.SkipThrottling || this.BypassBuildPermission;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00035560 File Offset: 0x00033760
		internal bool SkipThrottling
		{
			get
			{
				return !string.IsNullOrEmpty(this.SourceCapacityObjectId);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00035570 File Offset: 0x00033770
		internal bool HasServicePrincipalProfile
		{
			get
			{
				return !string.IsNullOrEmpty(this.ServicePrincipalProfileId);
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00035580 File Offset: 0x00033780
		internal void Validate()
		{
			if (!this.IsValid)
			{
				throw new InvalidOperationException(AuthenticationSR.Exception_S2STokenMissing);
			}
		}

		// Token: 0x0400084E RID: 2126
		private const int DefaultIntendedUsage = 0;

		// Token: 0x0400084F RID: 2127
		public bool ApplyAuxiliaryPermission;

		// Token: 0x04000850 RID: 2128
		public string AuxiliaryPermissionOwner;

		// Token: 0x04000851 RID: 2129
		public string ServiceToServiceToken;

		// Token: 0x04000852 RID: 2130
		public int IntendedUsage;

		// Token: 0x04000853 RID: 2131
		public string SourceCapacityObjectId;

		// Token: 0x04000854 RID: 2132
		public string ServicePrincipalProfileId;

		// Token: 0x04000855 RID: 2133
		public bool BypassBuildPermission;
	}
}
