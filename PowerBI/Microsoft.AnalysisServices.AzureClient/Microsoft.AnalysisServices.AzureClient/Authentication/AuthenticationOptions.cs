using System;
using Microsoft.AnalysisServices.AzureClient.Hosting;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001A RID: 26
	internal sealed class AuthenticationOptions
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003CC6 File Offset: 0x00001EC6
		public AuthenticationOptions(IConnectivityOwner owner)
		{
			this.Owner = owner;
			this.Endpoint = AuthenticationEndpoint.AadV2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003CDC File Offset: 0x00001EDC
		public AuthenticationOptions(IConnectivityOwner owner, AuthenticationEndpoint endpoint)
		{
			this.Owner = owner;
			this.Endpoint = endpoint;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003CF2 File Offset: 0x00001EF2
		public IConnectivityOwner Owner { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003CFA File Offset: 0x00001EFA
		public AuthenticationEndpoint Endpoint { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003D02 File Offset: 0x00001F02
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003D0A File Offset: 0x00001F0A
		public bool UseTokenCache { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003D13 File Offset: 0x00001F13
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003D1B File Offset: 0x00001F1B
		public SingleSignOnMode SsoMode { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003D24 File Offset: 0x00001F24
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003D2C File Offset: 0x00001F2C
		public bool BypassAuthInfoValidation { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003D35 File Offset: 0x00001F35
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003D3D File Offset: 0x00001F3D
		public bool HasServicePrincipalProfile { get; set; }
	}
}
