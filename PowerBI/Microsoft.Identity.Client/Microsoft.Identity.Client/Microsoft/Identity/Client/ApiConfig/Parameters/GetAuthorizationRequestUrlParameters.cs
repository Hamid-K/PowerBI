using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002DC RID: 732
	internal class GetAuthorizationRequestUrlParameters : IAcquireTokenParameters
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000574A0 File Offset: 0x000556A0
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x000574A8 File Offset: 0x000556A8
		public string RedirectUri { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000574B1 File Offset: 0x000556B1
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x000574B9 File Offset: 0x000556B9
		public IAccount Account { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000574C2 File Offset: 0x000556C2
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x000574CA File Offset: 0x000556CA
		public IEnumerable<string> ExtraScopesToConsent { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000574D3 File Offset: 0x000556D3
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x000574DB File Offset: 0x000556DB
		public string LoginHint { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x000574E4 File Offset: 0x000556E4
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x000574EC File Offset: 0x000556EC
		public string CodeVerifier { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000574F5 File Offset: 0x000556F5
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x000574FD File Offset: 0x000556FD
		public KeyValuePair<string, string>? CcsRoutingHint { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00057506 File Offset: 0x00055706
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0005750E File Offset: 0x0005570E
		public Prompt Prompt { get; set; } = Prompt.SelectAccount;

		// Token: 0x06001B43 RID: 6979 RVA: 0x00057518 File Offset: 0x00055718
		public AcquireTokenInteractiveParameters ToInteractiveParameters()
		{
			return new AcquireTokenInteractiveParameters
			{
				Account = this.Account,
				ExtraScopesToConsent = this.ExtraScopesToConsent,
				LoginHint = this.LoginHint,
				Prompt = this.Prompt,
				UseEmbeddedWebView = WebViewPreference.NotSpecified,
				CodeVerifier = this.CodeVerifier
			};
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0005756D File Offset: 0x0005576D
		public void LogParameters(ILoggerAdapter logger)
		{
		}
	}
}
