using System;
using Microsoft.AnalysisServices.AdomdClient.Hosting;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000100 RID: 256
	internal sealed class AuthenticationOptions
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00032B87 File Offset: 0x00030D87
		public AuthenticationOptions(IConnectivityOwner owner)
		{
			this.Owner = owner;
			this.Endpoint = AuthenticationEndpoint.AadV2;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00032B9D File Offset: 0x00030D9D
		public AuthenticationOptions(IConnectivityOwner owner, AuthenticationEndpoint endpoint)
		{
			this.Owner = owner;
			this.Endpoint = endpoint;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00032BB3 File Offset: 0x00030DB3
		public IConnectivityOwner Owner { get; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00032BBB File Offset: 0x00030DBB
		public AuthenticationEndpoint Endpoint { get; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00032BC3 File Offset: 0x00030DC3
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00032BCB File Offset: 0x00030DCB
		public bool UseTokenCache { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00032BD4 File Offset: 0x00030DD4
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00032BDC File Offset: 0x00030DDC
		public SingleSignOnMode SsoMode { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00032BE5 File Offset: 0x00030DE5
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00032BED File Offset: 0x00030DED
		public bool BypassAuthInfoValidation { get; set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00032BF6 File Offset: 0x00030DF6
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x00032BFE File Offset: 0x00030DFE
		public bool HasServicePrincipalProfile { get; set; }
	}
}
