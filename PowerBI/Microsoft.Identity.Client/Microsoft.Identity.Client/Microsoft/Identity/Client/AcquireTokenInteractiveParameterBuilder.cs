using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000122 RID: 290
	public sealed class AcquireTokenInteractiveParameterBuilder : AbstractPublicClientAcquireTokenParameterBuilder<AcquireTokenInteractiveParameterBuilder>
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000378E5 File Offset: 0x00035AE5
		private AcquireTokenInteractiveParameters Parameters { get; } = new AcquireTokenInteractiveParameters();

		// Token: 0x06000E45 RID: 3653 RVA: 0x000378ED File Offset: 0x00035AED
		internal AcquireTokenInteractiveParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
			: base(publicClientApplicationExecutor)
		{
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00037901 File Offset: 0x00035B01
		internal void SetCustomWebUi(ICustomWebUi customWebUi)
		{
			this.Parameters.CustomWebUi = customWebUi;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003790F File Offset: 0x00035B0F
		internal static AcquireTokenInteractiveParameterBuilder Create(IPublicClientApplicationExecutor publicClientApplicationExecutor, IEnumerable<string> scopes)
		{
			return new AcquireTokenInteractiveParameterBuilder(publicClientApplicationExecutor).WithCurrentSynchronizationContext().WithScopes(scopes);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00037922 File Offset: 0x00035B22
		internal AcquireTokenInteractiveParameterBuilder WithCurrentSynchronizationContext()
		{
			this.Parameters.UiParent.SynchronizationContext = SynchronizationContext.Current;
			return this;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003793A File Offset: 0x00035B3A
		internal AcquireTokenInteractiveParameterBuilder WithParentActivityOrWindowFunc(Func<object> parentActivityOrWindowFunc)
		{
			if (parentActivityOrWindowFunc != null)
			{
				this.WithParentActivityOrWindow(parentActivityOrWindowFunc());
			}
			return this;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003794D File Offset: 0x00035B4D
		public AcquireTokenInteractiveParameterBuilder WithUseEmbeddedWebView(bool useEmbeddedWebView)
		{
			this.Parameters.UseEmbeddedWebView = (useEmbeddedWebView ? WebViewPreference.Embedded : WebViewPreference.System);
			return this;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00037962 File Offset: 0x00035B62
		public AcquireTokenInteractiveParameterBuilder WithSystemWebViewOptions(SystemWebViewOptions options)
		{
			SystemWebViewOptions.ValidatePlatformAvailability();
			this.Parameters.UiParent.SystemWebViewOptions = options;
			return this;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003797B File Offset: 0x00035B7B
		public AcquireTokenInteractiveParameterBuilder WithEmbeddedWebViewOptions(EmbeddedWebViewOptions options)
		{
			EmbeddedWebViewOptions.ValidatePlatformAvailability();
			this.Parameters.UiParent.EmbeddedWebviewOptions = options;
			return this;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00037994 File Offset: 0x00035B94
		public AcquireTokenInteractiveParameterBuilder WithLoginHint(string loginHint)
		{
			this.Parameters.LoginHint = loginHint;
			return this;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000379A3 File Offset: 0x00035BA3
		public AcquireTokenInteractiveParameterBuilder WithAccount(IAccount account)
		{
			this.Parameters.Account = account;
			return this;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000379B2 File Offset: 0x00035BB2
		public AcquireTokenInteractiveParameterBuilder WithExtraScopesToConsent(IEnumerable<string> extraScopesToConsent)
		{
			this.Parameters.ExtraScopesToConsent = extraScopesToConsent;
			return this;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000379C1 File Offset: 0x00035BC1
		public AcquireTokenInteractiveParameterBuilder WithPrompt(Prompt prompt)
		{
			this.Parameters.Prompt = prompt;
			return this;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000379D0 File Offset: 0x00035BD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AcquireTokenInteractiveParameterBuilder WithParentActivityOrWindow(object parent)
		{
			return this.WithParentObject(parent);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000379DC File Offset: 0x00035BDC
		private AcquireTokenInteractiveParameterBuilder WithParentObject(object parent)
		{
			IWin32Window win32Window = parent as IWin32Window;
			if (win32Window != null)
			{
				this.Parameters.UiParent.OwnerWindow = win32Window.Handle;
				return this;
			}
			if (parent is IntPtr)
			{
				IntPtr intPtr = (IntPtr)parent;
				this.Parameters.UiParent.OwnerWindow = intPtr;
			}
			return this;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00037A36 File Offset: 0x00035C36
		[CLSCompliant(false)]
		public AcquireTokenInteractiveParameterBuilder WithParentActivityOrWindow(IWin32Window window)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			return this.WithParentObject(window);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00037A4D File Offset: 0x00035C4D
		[CLSCompliant(false)]
		public AcquireTokenInteractiveParameterBuilder WithParentActivityOrWindow(IntPtr window)
		{
			return this.WithParentObject(window);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00037A5C File Offset: 0x00035C5C
		public AcquireTokenInteractiveParameterBuilder WithProofOfPossession(string nonce, HttpMethod httpMethod, Uri requestUri)
		{
			ApplicationBase.GuardMobileFrameworks();
			if (!base.ServiceBundle.Config.IsBrokerEnabled)
			{
				throw new MsalClientException("broker_required_for_pop", "The request has Proof-of-Possession configured but does not have broker enabled. Broker is required to use Proof-of-Possession on public clients. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.");
			}
			if (!base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null).IsPopSupported)
			{
				throw new MsalClientException("broker_does_not_support_pop", "The broker does not support Proof-of-Possession on the current platform.");
			}
			PoPAuthenticationConfiguration poPAuthenticationConfiguration = new PoPAuthenticationConfiguration(requestUri);
			if (string.IsNullOrEmpty(nonce))
			{
				throw new ArgumentNullException("nonce");
			}
			poPAuthenticationConfiguration.Nonce = nonce;
			poPAuthenticationConfiguration.HttpMethod = httpMethod;
			base.CommonParameters.PopAuthenticationConfiguration = poPAuthenticationConfiguration;
			base.CommonParameters.AuthenticationScheme = new PopBrokerAuthenticationScheme();
			return this;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00037B08 File Offset: 0x00035D08
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.UiParent.SystemWebViewOptions != null && this.Parameters.UseEmbeddedWebView == WebViewPreference.Embedded)
			{
				throw new MsalClientException("embedded_webview_not_compatible_default_browser", "You configured MSAL interactive authentication to use an embedded WebView and you also configured system WebView options. These are mutually exclusive. See https://aka.ms/msal-net-os-browser. ");
			}
			if (this.Parameters.UiParent.SystemWebViewOptions != null && this.Parameters.UseEmbeddedWebView == WebViewPreference.NotSpecified)
			{
				this.WithUseEmbeddedWebView(false);
			}
			AcquireTokenInteractiveParameters parameters = this.Parameters;
			string text;
			if (!string.IsNullOrWhiteSpace(this.Parameters.LoginHint))
			{
				text = this.Parameters.LoginHint;
			}
			else
			{
				IAccount account = this.Parameters.Account;
				text = ((account != null) ? account.Username : null);
			}
			parameters.LoginHint = text;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00037BB3 File Offset: 0x00035DB3
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.PublicClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00037BCD File Offset: 0x00035DCD
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenInteractive;
		}
	}
}
