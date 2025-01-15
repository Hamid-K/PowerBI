using System;
using System.Threading;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001A7 RID: 423
	internal class InteractiveWebUI : WebUI
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x00040AD0 File Offset: 0x0003ECD0
		public InteractiveWebUI(CoreUIParent parent, RequestContext requestContext)
		{
			base.OwnerWindow = ((parent != null) ? parent.OwnerWindow : null);
			base.SynchronizationContext = ((parent != null) ? parent.SynchronizationContext : null);
			base.RequestContext = requestContext;
			this.EmbeddedWebViewOptions = ((parent != null) ? parent.EmbeddedWebviewOptions : null);
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00040B20 File Offset: 0x0003ED20
		public EmbeddedWebViewOptions EmbeddedWebViewOptions { get; }

		// Token: 0x06001345 RID: 4933 RVA: 0x00040B28 File Offset: 0x0003ED28
		protected override AuthorizationResult OnAuthenticate(CancellationToken cancellationToken)
		{
			WindowsFormsWebAuthenticationDialog windowsFormsWebAuthenticationDialog = new WindowsFormsWebAuthenticationDialog(base.OwnerWindow, this.EmbeddedWebViewOptions);
			windowsFormsWebAuthenticationDialog.RequestContext = base.RequestContext;
			WindowsFormsWebAuthenticationDialog windowsFormsWebAuthenticationDialog2 = windowsFormsWebAuthenticationDialog;
			this._dialog = windowsFormsWebAuthenticationDialog;
			AuthorizationResult authorizationResult;
			using (windowsFormsWebAuthenticationDialog2)
			{
				authorizationResult = this._dialog.AuthenticateAAD(base.RequestUri, base.CallbackUri, cancellationToken);
			}
			return authorizationResult;
		}

		// Token: 0x040007AC RID: 1964
		private WindowsFormsWebAuthenticationDialog _dialog;
	}
}
