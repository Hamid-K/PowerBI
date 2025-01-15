using System;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F5 RID: 245
	internal sealed class AuthenticationOptions
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x000354A4 File Offset: 0x000336A4
		public AuthenticationOptions(IConnectivityOwner owner)
		{
			this.Owner = owner;
			this.Endpoint = AuthenticationEndpoint.AadV2;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x000354BA File Offset: 0x000336BA
		public AuthenticationOptions(IConnectivityOwner owner, AuthenticationEndpoint endpoint)
		{
			this.Owner = owner;
			this.Endpoint = endpoint;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000354D0 File Offset: 0x000336D0
		public IConnectivityOwner Owner { get; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000354D8 File Offset: 0x000336D8
		public AuthenticationEndpoint Endpoint { get; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000354E0 File Offset: 0x000336E0
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x000354E8 File Offset: 0x000336E8
		public bool UseTokenCache { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000354F1 File Offset: 0x000336F1
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x000354F9 File Offset: 0x000336F9
		public SingleSignOnMode SsoMode { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00035502 File Offset: 0x00033702
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0003550A File Offset: 0x0003370A
		public bool BypassAuthInfoValidation { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00035513 File Offset: 0x00033713
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0003551B File Offset: 0x0003371B
		public bool HasServicePrincipalProfile { get; set; }
	}
}
