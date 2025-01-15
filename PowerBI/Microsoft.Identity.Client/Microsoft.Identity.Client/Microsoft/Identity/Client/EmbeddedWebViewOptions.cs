using System;
using System.ComponentModel;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000128 RID: 296
	public class EmbeddedWebViewOptions
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00038281 File Offset: 0x00036481
		public EmbeddedWebViewOptions()
		{
			EmbeddedWebViewOptions.ValidatePlatformAvailability();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003828E File Offset: 0x0003648E
		internal static EmbeddedWebViewOptions GetDefaultOptions()
		{
			return new EmbeddedWebViewOptions();
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00038295 File Offset: 0x00036495
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0003829D File Offset: 0x0003649D
		public string Title { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000382A6 File Offset: 0x000364A6
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x000382AE File Offset: 0x000364AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("In case when WebView2 is not available, MSAL.NET will fallback to legacy WebView.", true)]
		public string WebView2BrowserExecutableFolder { get; set; }

		// Token: 0x06000E96 RID: 3734 RVA: 0x000382B7 File Offset: 0x000364B7
		internal void LogParameters(ILoggerAdapter logger)
		{
			logger.Info("WebView2Options configured");
			logger.Info(() => "Title: " + this.Title);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000382D6 File Offset: 0x000364D6
		internal static void ValidatePlatformAvailability()
		{
		}
	}
}
