using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AF RID: 431
	public class WebBrowserNavigateErrorEventArgs : CancelEventArgs
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x000414F4 File Offset: 0x0003F6F4
		public WebBrowserNavigateErrorEventArgs(string url, string targetFrameName, int statusCode, object webBrowserActiveXInstance)
		{
			this.Url = url;
			this.TargetFrameName = targetFrameName;
			this.StatusCode = statusCode;
			this.WebBrowserActiveXInstance = webBrowserActiveXInstance;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00041519 File Offset: 0x0003F719
		public string TargetFrameName { get; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00041521 File Offset: 0x0003F721
		public string Url { get; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00041529 File Offset: 0x0003F729
		public object WebBrowserActiveXInstance { get; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00041531 File Offset: 0x0003F731
		public int StatusCode { get; }
	}
}
