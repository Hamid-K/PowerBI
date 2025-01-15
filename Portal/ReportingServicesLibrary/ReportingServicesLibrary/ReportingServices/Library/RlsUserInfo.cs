using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000047 RID: 71
	public sealed class RlsUserInfo
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000FA87 File Offset: 0x0000DC87
		public bool IsCloudRoleLevelSecurityMembershipEnabled { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000FA90 File Offset: 0x0000DC90
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000FA98 File Offset: 0x0000DC98
		public bool IsCloudRlsEnabled { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000FAA1 File Offset: 0x0000DCA1
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000FAA9 File Offset: 0x0000DCA9
		public string UserPrincipalName { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000FAB2 File Offset: 0x0000DCB2
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000FABA File Offset: 0x0000DCBA
		public string TenantId { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000FAC3 File Offset: 0x0000DCC3
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000FACB File Offset: 0x0000DCCB
		public string ImpersonatedUserPrincipalName { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public string ImpersonatedRoles { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public static RlsUserInfo Default
		{
			get
			{
				return new RlsUserInfo
				{
					IsCloudRoleLevelSecurityMembershipEnabled = false,
					IsCloudRlsEnabled = false,
					UserPrincipalName = string.Empty,
					TenantId = string.Empty,
					ImpersonatedUserPrincipalName = string.Empty,
					ImpersonatedRoles = string.Empty
				};
			}
		}
	}
}
