using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x02000257 RID: 599
	internal class CertificateAndClaimsClientCredential : IClientCredential
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00050721 File Offset: 0x0004E921
		public X509Certificate2 Certificate { get; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00050729 File Offset: 0x0004E929
		public AssertionType AssertionType
		{
			get
			{
				return AssertionType.CertificateWithoutSni;
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0005072C File Offset: 0x0004E92C
		public CertificateAndClaimsClientCredential(X509Certificate2 certificate, IDictionary<string, string> claimsToSign, bool appendDefaultClaims)
		{
			this.Certificate = certificate;
			this._claimsToSign = claimsToSign;
			this._appendDefaultClaims = appendDefaultClaims;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0005074C File Offset: 0x0004E94C
		public Task AddConfidentialClientParametersAsync(OAuth2Client oAuth2Client, ILoggerAdapter logger, ICryptographyManager cryptographyManager, string clientId, string tokenEndpoint, bool sendX5C, bool useSha2AndPss, CancellationToken cancellationToken)
		{
			string text = new JsonWebToken(cryptographyManager, clientId, tokenEndpoint, this._claimsToSign, this._appendDefaultClaims).Sign(this.Certificate, sendX5C, useSha2AndPss);
			oAuth2Client.AddBodyParameter("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
			oAuth2Client.AddBodyParameter("client_assertion", text);
			return Task.CompletedTask;
		}

		// Token: 0x04000A9C RID: 2716
		private readonly IDictionary<string, string> _claimsToSign;

		// Token: 0x04000A9D RID: 2717
		private readonly bool _appendDefaultClaims;
	}
}
