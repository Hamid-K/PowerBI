using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x02000259 RID: 601
	internal interface IClientCredential
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600181B RID: 6171
		AssertionType AssertionType { get; }

		// Token: 0x0600181C RID: 6172
		Task AddConfidentialClientParametersAsync(OAuth2Client oAuth2Client, ILoggerAdapter logger, ICryptographyManager cryptographyManager, string clientId, string tokenEndpoint, bool sendX5C, bool useSha2, CancellationToken cancellationToken);
	}
}
