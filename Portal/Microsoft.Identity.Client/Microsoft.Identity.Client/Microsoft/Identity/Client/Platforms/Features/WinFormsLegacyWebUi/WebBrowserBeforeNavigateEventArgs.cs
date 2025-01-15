using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AD RID: 429
	public class WebBrowserBeforeNavigateEventArgs : CancelEventArgs
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x00041487 File Offset: 0x0003F687
		public WebBrowserBeforeNavigateEventArgs(string url, byte[] postData, string headers, int flags, string targetFrameName, object webBrowserActiveXInstance)
		{
			this.Url = url;
			this.PostData = postData;
			this.Headers = headers;
			this.Flags = flags;
			this.TargetFrameName = targetFrameName;
			this.WebBrowserActiveXInstance = webBrowserActiveXInstance;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000414BC File Offset: 0x0003F6BC
		public string Url { get; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public byte[] PostData { get; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000414CC File Offset: 0x0003F6CC
		public string Headers { get; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x000414D4 File Offset: 0x0003F6D4
		public int Flags { get; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000414DC File Offset: 0x0003F6DC
		public string TargetFrameName { get; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public object WebBrowserActiveXInstance { get; }
	}
}
