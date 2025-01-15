using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Advanced;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000123 RID: 291
	public sealed class AcquireTokenOnBehalfOfParameterBuilder : AbstractConfidentialClientAcquireTokenParameterBuilder<AcquireTokenOnBehalfOfParameterBuilder>
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00037BD4 File Offset: 0x00035DD4
		internal AcquireTokenOnBehalfOfParameters Parameters { get; } = new AcquireTokenOnBehalfOfParameters();

		// Token: 0x06000E5A RID: 3674 RVA: 0x00037BDC File Offset: 0x00035DDC
		internal AcquireTokenOnBehalfOfParameterBuilder(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor)
			: base(confidentialClientApplicationExecutor)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00037BF0 File Offset: 0x00035DF0
		internal static AcquireTokenOnBehalfOfParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes, UserAssertion userAssertion)
		{
			return new AcquireTokenOnBehalfOfParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes).WithUserAssertion(userAssertion);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00037C04 File Offset: 0x00035E04
		internal static AcquireTokenOnBehalfOfParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes, UserAssertion userAssertion, string cacheKey)
		{
			return new AcquireTokenOnBehalfOfParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes).WithUserAssertion(userAssertion).WithCacheKey(cacheKey);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00037C1E File Offset: 0x00035E1E
		internal static AcquireTokenOnBehalfOfParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes, string cacheKey)
		{
			return new AcquireTokenOnBehalfOfParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes).WithCacheKey(cacheKey);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00037C32 File Offset: 0x00035E32
		private AcquireTokenOnBehalfOfParameterBuilder WithUserAssertion(UserAssertion userAssertion)
		{
			this.Parameters.UserAssertion = userAssertion;
			return this;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00037C41 File Offset: 0x00035E41
		private AcquireTokenOnBehalfOfParameterBuilder WithCacheKey(string cacheKey)
		{
			AcquireTokenOnBehalfOfParameters parameters = this.Parameters;
			if (cacheKey == null)
			{
				throw new ArgumentNullException("cacheKey");
			}
			parameters.LongRunningOboCacheKey = cacheKey;
			return this;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00037C5F File Offset: 0x00035E5F
		public AcquireTokenOnBehalfOfParameterBuilder WithSendX5C(bool withSendX5C)
		{
			this.Parameters.SendX5C = new bool?(withSendX5C);
			return this;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00037C73 File Offset: 0x00035E73
		public AcquireTokenOnBehalfOfParameterBuilder WithForceRefresh(bool forceRefresh)
		{
			this.Parameters.ForceRefresh = forceRefresh;
			return this;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00037C84 File Offset: 0x00035E84
		public AcquireTokenOnBehalfOfParameterBuilder WithCcsRoutingHint(string userObjectIdentifier, string tenantIdentifier)
		{
			if (string.IsNullOrEmpty(userObjectIdentifier) || string.IsNullOrEmpty(tenantIdentifier))
			{
				return this;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"x-anchormailbox",
				CoreHelpers.GetCcsClientInfoHint(userObjectIdentifier, tenantIdentifier)
			} };
			this.WithExtraHttpHeaders(dictionary);
			return this;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00037CC4 File Offset: 0x00035EC4
		public AcquireTokenOnBehalfOfParameterBuilder WithCcsRoutingHint(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return this;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"x-anchormailbox",
				CoreHelpers.GetCcsUpnHint(userName)
			} };
			this.WithExtraHttpHeaders(dictionary);
			return this;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00037CFB File Offset: 0x00035EFB
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ConfidentialClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00037D18 File Offset: 0x00035F18
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.SendX5C == null)
			{
				AbstractAcquireTokenConfidentialClientParameters parameters = this.Parameters;
				ApplicationConfiguration config = base.ServiceBundle.Config;
				parameters.SendX5C = new bool?(config != null && config.SendX5C);
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00037D67 File Offset: 0x00035F67
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			if (string.IsNullOrEmpty(this.Parameters.LongRunningOboCacheKey))
			{
				return ApiEvent.ApiIds.AcquireTokenOnBehalfOf;
			}
			if (this.Parameters.UserAssertion != null)
			{
				return ApiEvent.ApiIds.InitiateLongRunningObo;
			}
			return ApiEvent.ApiIds.AcquireTokenInLongRunningObo;
		}
	}
}
