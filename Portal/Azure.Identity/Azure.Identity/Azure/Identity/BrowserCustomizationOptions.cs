using System;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000038 RID: 56
	public class BrowserCustomizationOptions
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000063A8 File Offset: 0x000045A8
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000063B0 File Offset: 0x000045B0
		public bool? UseEmbeddedWebView { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000063B9 File Offset: 0x000045B9
		private SystemWebViewOptions systemWebViewOptions
		{
			get
			{
				if (this.SystemBrowserOptions == null)
				{
					this.SystemBrowserOptions = new SystemWebViewOptions();
				}
				return this.SystemBrowserOptions;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000063D4 File Offset: 0x000045D4
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000063E1 File Offset: 0x000045E1
		public string SuccessMessage
		{
			get
			{
				return this.systemWebViewOptions.HtmlMessageSuccess;
			}
			set
			{
				this.systemWebViewOptions.HtmlMessageSuccess = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000063EF File Offset: 0x000045EF
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000063FC File Offset: 0x000045FC
		public string ErrorMessage
		{
			get
			{
				return this.systemWebViewOptions.HtmlMessageError;
			}
			set
			{
				this.systemWebViewOptions.HtmlMessageError = value;
			}
		}

		// Token: 0x04000119 RID: 281
		internal SystemWebViewOptions SystemBrowserOptions;
	}
}
