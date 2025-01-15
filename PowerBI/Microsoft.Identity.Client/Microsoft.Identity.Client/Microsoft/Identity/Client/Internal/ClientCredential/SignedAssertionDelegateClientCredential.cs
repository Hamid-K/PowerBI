using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x0200025C RID: 604
	internal class SignedAssertionDelegateClientCredential : IClientCredential
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00050817 File Offset: 0x0004EA17
		internal Func<CancellationToken, Task<string>> _signedAssertionDelegate { get; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005081F File Offset: 0x0004EA1F
		internal Func<AssertionRequestOptions, Task<string>> _signedAssertionWithInfoDelegate { get; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00050827 File Offset: 0x0004EA27
		public AssertionType AssertionType
		{
			get
			{
				return AssertionType.ClientAssertion;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005082A File Offset: 0x0004EA2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SignedAssertionDelegateClientCredential(Func<CancellationToken, Task<string>> signedAssertionDelegate)
		{
			this._signedAssertionDelegate = signedAssertionDelegate;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00050839 File Offset: 0x0004EA39
		public SignedAssertionDelegateClientCredential(Func<AssertionRequestOptions, Task<string>> signedAssertionDelegate)
		{
			this._signedAssertionWithInfoDelegate = signedAssertionDelegate;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00050848 File Offset: 0x0004EA48
		public async Task AddConfidentialClientParametersAsync(OAuth2Client oAuth2Client, ILoggerAdapter logger, ICryptographyManager cryptographyManager, string clientId, string tokenEndpoint, bool sendX5C, bool useSha2, CancellationToken cancellationToken)
		{
			string text = await ((this._signedAssertionDelegate != null) ? this._signedAssertionDelegate(cancellationToken).ConfigureAwait(false) : this._signedAssertionWithInfoDelegate(new AssertionRequestOptions
			{
				CancellationToken = cancellationToken,
				ClientID = clientId,
				TokenEndpoint = tokenEndpoint
			}).ConfigureAwait(false));
			oAuth2Client.AddBodyParameter("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
			oAuth2Client.AddBodyParameter("client_assertion", text);
		}
	}
}
