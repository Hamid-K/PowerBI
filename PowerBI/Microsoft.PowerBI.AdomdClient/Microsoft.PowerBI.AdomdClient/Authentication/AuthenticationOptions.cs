using System;
using Microsoft.AnalysisServices.AdomdClient.Hosting;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000100 RID: 256
	internal sealed class AuthenticationOptions
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x00032857 File Offset: 0x00030A57
		public AuthenticationOptions(IConnectivityOwner owner)
		{
			this.Owner = owner;
			this.Endpoint = AuthenticationEndpoint.AadV2;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003286D File Offset: 0x00030A6D
		public AuthenticationOptions(IConnectivityOwner owner, AuthenticationEndpoint endpoint)
		{
			this.Owner = owner;
			this.Endpoint = endpoint;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00032883 File Offset: 0x00030A83
		public IConnectivityOwner Owner { get; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003288B File Offset: 0x00030A8B
		public AuthenticationEndpoint Endpoint { get; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00032893 File Offset: 0x00030A93
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0003289B File Offset: 0x00030A9B
		public bool UseTokenCache { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000328A4 File Offset: 0x00030AA4
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x000328AC File Offset: 0x00030AAC
		public SingleSignOnMode SsoMode { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000328B5 File Offset: 0x00030AB5
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x000328BD File Offset: 0x00030ABD
		public bool BypassAuthInfoValidation { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000328C6 File Offset: 0x00030AC6
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x000328CE File Offset: 0x00030ACE
		public bool HasServicePrincipalProfile { get; set; }
	}
}
