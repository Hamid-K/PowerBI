using System;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.netdesktop
{
	// Token: 0x02000188 RID: 392
	internal class NetDesktopWebUIFactory : IWebUIFactory
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0003FDCB File Offset: 0x0003DFCB
		public bool IsSystemWebViewAvailable
		{
			get
			{
				return this.IsUserInteractive;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0003FDD3 File Offset: 0x0003DFD3
		public bool IsUserInteractive
		{
			get
			{
				return DesktopOsHelper.IsUserInteractive();
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0003FDDA File Offset: 0x0003DFDA
		public bool IsEmbeddedWebViewAvailable
		{
			get
			{
				return this.IsUserInteractive;
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003FDE4 File Offset: 0x0003DFE4
		public IWebUI CreateAuthenticationDialog(CoreUIParent coreUIParent, WebViewPreference useEmbeddedWebView, RequestContext requestContext)
		{
			if (coreUIParent.UseHiddenBrowser)
			{
				return new SilentWebUI(coreUIParent, requestContext);
			}
			if (useEmbeddedWebView == WebViewPreference.System)
			{
				requestContext.Logger.Info("Using system browser.");
				return new DefaultOsBrowserWebUi(requestContext.ServiceBundle.PlatformProxy, requestContext.Logger, coreUIParent.SystemWebViewOptions, null);
			}
			requestContext.Logger.Info("Using legacy embedded browser.");
			return new InteractiveWebUI(coreUIParent, requestContext);
		}
	}
}
