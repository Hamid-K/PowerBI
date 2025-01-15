using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x0200025A RID: 602
	internal class SecretStringClientCredential : IClientCredential
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000507AB File Offset: 0x0004E9AB
		internal string Secret { get; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000507B3 File Offset: 0x0004E9B3
		public AssertionType AssertionType
		{
			get
			{
				return AssertionType.Secret;
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000507B6 File Offset: 0x0004E9B6
		public SecretStringClientCredential(string secret)
		{
			this.Secret = secret;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000507C5 File Offset: 0x0004E9C5
		public Task AddConfidentialClientParametersAsync(OAuth2Client oAuth2Client, ILoggerAdapter logger, ICryptographyManager cryptographyManager, string clientId, string tokenEndpoint, bool sendX5C, bool useSha2, CancellationToken cancellationToken)
		{
			oAuth2Client.AddBodyParameter("client_secret", this.Secret);
			return Task.CompletedTask;
		}
	}
}
