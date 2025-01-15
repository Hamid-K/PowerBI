using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.UI;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D8 RID: 728
	internal class AcquireTokenInteractiveParameters : IAcquireTokenParameters
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0005710A File Offset: 0x0005530A
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x00057112 File Offset: 0x00055312
		public Prompt Prompt { get; set; } = Prompt.NotSpecified;

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0005711B File Offset: 0x0005531B
		public CoreUIParent UiParent { get; } = new CoreUIParent();

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00057123 File Offset: 0x00055323
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0005712B File Offset: 0x0005532B
		public IEnumerable<string> ExtraScopesToConsent { get; set; } = CollectionHelpers.GetEmptyReadOnlyList<string>();

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00057134 File Offset: 0x00055334
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0005713C File Offset: 0x0005533C
		public WebViewPreference UseEmbeddedWebView { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x00057145 File Offset: 0x00055345
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0005714D File Offset: 0x0005534D
		public string LoginHint { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00057156 File Offset: 0x00055356
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0005715E File Offset: 0x0005535E
		public IAccount Account { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x00057167 File Offset: 0x00055367
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0005716F File Offset: 0x0005536F
		public ICustomWebUi CustomWebUi { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00057178 File Offset: 0x00055378
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x00057180 File Offset: 0x00055380
		public string CodeVerifier { get; set; }

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005718C File Offset: 0x0005538C
		public void LogParameters(ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				SystemWebViewOptions systemWebViewOptions = this.UiParent.SystemWebViewOptions;
				if (systemWebViewOptions != null)
				{
					systemWebViewOptions.LogParameters(logger);
				}
				logger.Info(string.Format("=== InteractiveParameters Data ===\r\nLoginHint provided: {0}\r\nUser provided: {1}\r\nUseEmbeddedWebView: {2}\r\nExtraScopesToConsent: {3}\r\nPrompt: {4}\r\nHasCustomWebUi: {5}", new object[]
				{
					!string.IsNullOrEmpty(this.LoginHint),
					this.Account != null,
					this.UseEmbeddedWebView,
					string.Join(";", this.ExtraScopesToConsent ?? CollectionHelpers.GetEmptyReadOnlyList<string>()),
					this.Prompt.PromptValue,
					this.CustomWebUi != null
				}));
			}
		}
	}
}
