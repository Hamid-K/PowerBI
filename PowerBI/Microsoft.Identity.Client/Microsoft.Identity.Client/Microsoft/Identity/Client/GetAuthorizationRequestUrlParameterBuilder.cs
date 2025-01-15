using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000129 RID: 297
	public sealed class GetAuthorizationRequestUrlParameterBuilder : AbstractConfidentialClientAcquireTokenParameterBuilder<GetAuthorizationRequestUrlParameterBuilder>
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000382EA File Offset: 0x000364EA
		private GetAuthorizationRequestUrlParameters Parameters { get; } = new GetAuthorizationRequestUrlParameters();

		// Token: 0x06000E9A RID: 3738 RVA: 0x000382F2 File Offset: 0x000364F2
		internal GetAuthorizationRequestUrlParameterBuilder(IConfidentialClientApplicationExecutor confidentialClientApplicationexecutor)
			: base(confidentialClientApplicationexecutor)
		{
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00038306 File Offset: 0x00036506
		internal static GetAuthorizationRequestUrlParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes)
		{
			return new GetAuthorizationRequestUrlParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00038314 File Offset: 0x00036514
		public GetAuthorizationRequestUrlParameterBuilder WithRedirectUri(string redirectUri)
		{
			this.Parameters.RedirectUri = redirectUri;
			return this;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00038323 File Offset: 0x00036523
		public GetAuthorizationRequestUrlParameterBuilder WithLoginHint(string loginHint)
		{
			this.Parameters.LoginHint = loginHint;
			return this;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00038332 File Offset: 0x00036532
		public GetAuthorizationRequestUrlParameterBuilder WithAccount(IAccount account)
		{
			this.Parameters.Account = account;
			return this;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00038341 File Offset: 0x00036541
		public GetAuthorizationRequestUrlParameterBuilder WithExtraScopesToConsent(IEnumerable<string> extraScopesToConsent)
		{
			this.Parameters.ExtraScopesToConsent = extraScopesToConsent;
			return this;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00038350 File Offset: 0x00036550
		public GetAuthorizationRequestUrlParameterBuilder WithPkce(out string codeVerifier)
		{
			GetAuthorizationRequestUrlParameters parameters = this.Parameters;
			string text;
			codeVerifier = (text = base.ServiceBundle.PlatformProxy.CryptographyManager.GenerateCodeVerifier());
			parameters.CodeVerifier = text;
			return this;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00038383 File Offset: 0x00036583
		public GetAuthorizationRequestUrlParameterBuilder WithCcsRoutingHint(string userObjectIdentifier, string tenantIdentifier)
		{
			if (string.IsNullOrEmpty(userObjectIdentifier) || string.IsNullOrEmpty(tenantIdentifier))
			{
				return this;
			}
			this.Parameters.CcsRoutingHint = new KeyValuePair<string, string>?(new KeyValuePair<string, string>(userObjectIdentifier, tenantIdentifier));
			return this;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000383AF File Offset: 0x000365AF
		public GetAuthorizationRequestUrlParameterBuilder WithPrompt(Prompt prompt)
		{
			this.Parameters.Prompt = prompt;
			return this;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000383BE File Offset: 0x000365BE
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			throw new InvalidOperationException("This is a developer BUG.  This should never get executed.");
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000383CA File Offset: 0x000365CA
		public new Task<Uri> ExecuteAsync(CancellationToken cancellationToken)
		{
			base.ValidateAndCalculateApiId();
			return base.ConfidentialClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000383EA File Offset: 0x000365EA
		public new Task<Uri> ExecuteAsync()
		{
			return this.ExecuteAsync(CancellationToken.None);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000383F7 File Offset: 0x000365F7
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.GetAuthorizationRequestUrl;
		}
	}
}
