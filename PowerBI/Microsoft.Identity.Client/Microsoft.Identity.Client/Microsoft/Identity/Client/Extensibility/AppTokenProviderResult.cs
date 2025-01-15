using System;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000296 RID: 662
	public class AppTokenProviderResult
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00052CB8 File Offset: 0x00050EB8
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00052CC0 File Offset: 0x00050EC0
		public string AccessToken { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00052CC9 File Offset: 0x00050EC9
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00052CD1 File Offset: 0x00050ED1
		public long ExpiresInSeconds { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00052CDA File Offset: 0x00050EDA
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x00052CE2 File Offset: 0x00050EE2
		public long? RefreshInSeconds { get; set; }
	}
}
