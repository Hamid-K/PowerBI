using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x0200025B RID: 603
	internal class SignedAssertionClientCredential : IClientCredential
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000507DD File Offset: 0x0004E9DD
		public AssertionType AssertionType
		{
			get
			{
				return AssertionType.ClientAssertion;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000507E0 File Offset: 0x0004E9E0
		public SignedAssertionClientCredential(string signedAssertion)
		{
			this._signedAssertion = signedAssertion;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000507EF File Offset: 0x0004E9EF
		public Task AddConfidentialClientParametersAsync(OAuth2Client oAuth2Client, ILoggerAdapter logger, ICryptographyManager cryptographyManager, string clientId, string tokenEndpoint, bool sendX5C, bool useSha2, CancellationToken cancellationToken)
		{
			oAuth2Client.AddBodyParameter("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
			oAuth2Client.AddBodyParameter("client_assertion", this._signedAssertion);
			return Task.CompletedTask;
		}

		// Token: 0x04000AA0 RID: 2720
		private readonly string _signedAssertion;
	}
}
