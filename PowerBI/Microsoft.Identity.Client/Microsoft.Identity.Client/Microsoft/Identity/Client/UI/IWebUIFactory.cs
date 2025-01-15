using System;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001DD RID: 477
	internal interface IWebUIFactory
	{
		// Token: 0x0600149D RID: 5277
		IWebUI CreateAuthenticationDialog(CoreUIParent coreUIParent, WebViewPreference webViewPreference, RequestContext requestContext);

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600149E RID: 5278
		bool IsSystemWebViewAvailable { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600149F RID: 5279
		bool IsUserInteractive { get; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060014A0 RID: 5280
		bool IsEmbeddedWebViewAvailable { get; }
	}
}
