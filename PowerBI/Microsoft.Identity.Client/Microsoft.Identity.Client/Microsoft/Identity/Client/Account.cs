using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000114 RID: 276
	internal sealed class Account : IAccount
	{
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00036D7E File Offset: 0x00034F7E
		public Account(string homeAccountId, string username, string environment, IDictionary<string, string> wamAccountIds = null, IEnumerable<TenantProfile> tenantProfiles = null)
		{
			this.Username = username;
			this.Environment = environment;
			this.HomeAccountId = AccountId.ParseFromString(homeAccountId);
			this.WamAccountIds = wamAccountIds;
			this.TenantProfiles = tenantProfiles;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00036DB0 File Offset: 0x00034FB0
		public string Username { get; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00036DB8 File Offset: 0x00034FB8
		public string Environment { get; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00036DC0 File Offset: 0x00034FC0
		public AccountId HomeAccountId { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00036DC8 File Offset: 0x00034FC8
		public IEnumerable<TenantProfile> TenantProfiles { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00036DD0 File Offset: 0x00034FD0
		internal IDictionary<string, string> WamAccountIds { get; }

		// Token: 0x06000DDE RID: 3550 RVA: 0x00036DD8 File Offset: 0x00034FD8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Account username: {0} environment {1} home account id: {2}", this.Username, this.Environment, this.HomeAccountId);
		}
	}
}
