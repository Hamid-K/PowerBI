using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.AuthScheme.Bearer;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D5 RID: 725
	internal class AcquireTokenCommonParameters
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00056F43 File Offset: 0x00055143
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x00056F4B File Offset: 0x0005514B
		public ApiEvent.ApiIds ApiId { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00056F54 File Offset: 0x00055154
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00056F5C File Offset: 0x0005515C
		public Guid CorrelationId { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00056F65 File Offset: 0x00055165
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x00056F6D File Offset: 0x0005516D
		public Guid UserProvidedCorrelationId { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00056F76 File Offset: 0x00055176
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x00056F7E File Offset: 0x0005517E
		public bool UseCorrelationIdFromUser { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00056F87 File Offset: 0x00055187
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00056F8F File Offset: 0x0005518F
		public IEnumerable<string> Scopes { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00056F98 File Offset: 0x00055198
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00056FA0 File Offset: 0x000551A0
		public IDictionary<string, string> ExtraQueryParameters { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00056FA9 File Offset: 0x000551A9
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00056FB1 File Offset: 0x000551B1
		public string Claims { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00056FBA File Offset: 0x000551BA
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00056FC2 File Offset: 0x000551C2
		public AuthorityInfo AuthorityOverride { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00056FCB File Offset: 0x000551CB
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x00056FD3 File Offset: 0x000551D3
		public IAuthenticationScheme AuthenticationScheme { get; set; } = new BearerAuthenticationScheme();

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00056FDC File Offset: 0x000551DC
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x00056FE4 File Offset: 0x000551E4
		public IDictionary<string, string> ExtraHttpHeaders { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00056FED File Offset: 0x000551ED
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x00056FF5 File Offset: 0x000551F5
		public PoPAuthenticationConfiguration PopAuthenticationConfiguration { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00056FFE File Offset: 0x000551FE
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x00057006 File Offset: 0x00055206
		public Func<OnBeforeTokenRequestData, Task> OnBeforeTokenRequestHandler { get; internal set; }
	}
}
