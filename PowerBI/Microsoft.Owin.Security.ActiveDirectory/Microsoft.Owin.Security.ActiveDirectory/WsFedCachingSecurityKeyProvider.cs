using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.ActiveDirectory.Properties;
using Microsoft.Owin.Security.Jwt;

namespace Microsoft.Owin.Security.ActiveDirectory
{
	// Token: 0x02000005 RID: 5
	internal class WsFedCachingSecurityKeyProvider : IIssuerSecurityKeyProvider
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002200 File Offset: 0x00000400
		public WsFedCachingSecurityKeyProvider(string metadataEndpoint, ICertificateValidator backchannelCertificateValidator, TimeSpan backchannelTimeout, HttpMessageHandler backchannelHttpHandler)
		{
			this._metadataEndpoint = metadataEndpoint;
			this._backchannelTimeout = backchannelTimeout;
			this._backchannelHttpHandler = backchannelHttpHandler ?? new WebRequestHandler();
			if (backchannelCertificateValidator != null)
			{
				WebRequestHandler webRequestHandler = this._backchannelHttpHandler as WebRequestHandler;
				if (webRequestHandler == null)
				{
					throw new InvalidOperationException(Resources.Exception_ValidatorHandlerMismatch);
				}
				webRequestHandler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(backchannelCertificateValidator.Validate);
			}
			this.RetrieveMetadata();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000228E File Offset: 0x0000048E
		public string Issuer
		{
			get
			{
				this.RefreshMetadata();
				return this._issuer;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000229C File Offset: 0x0000049C
		public IEnumerable<SecurityKey> SecurityKeys
		{
			get
			{
				this.RefreshMetadata();
				return this._keys;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022AA File Offset: 0x000004AA
		private void RefreshMetadata()
		{
			if (this._syncAfter >= DateTimeOffset.UtcNow)
			{
				return;
			}
			this._syncAfter = DateTimeOffset.UtcNow + this._refreshInterval;
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				try
				{
					this.RetrieveMetadata();
				}
				catch (Exception)
				{
				}
			}, null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022E8 File Offset: 0x000004E8
		private void RetrieveMetadata()
		{
			this._syncAfter = DateTimeOffset.UtcNow + this._refreshInterval;
			IssuerSigningKeys metaData = WsFedMetadataRetriever.GetSigningKeys(this._metadataEndpoint, this._backchannelTimeout, this._backchannelHttpHandler);
			this._issuer = metaData.Issuer;
			this._keys = metaData.Keys;
		}

		// Token: 0x04000016 RID: 22
		private readonly TimeSpan _refreshInterval = new TimeSpan(1, 0, 0, 0);

		// Token: 0x04000017 RID: 23
		private readonly string _metadataEndpoint;

		// Token: 0x04000018 RID: 24
		private readonly TimeSpan _backchannelTimeout;

		// Token: 0x04000019 RID: 25
		private readonly HttpMessageHandler _backchannelHttpHandler;

		// Token: 0x0400001A RID: 26
		private DateTimeOffset _syncAfter = new DateTimeOffset(new DateTime(2001, 1, 1));

		// Token: 0x0400001B RID: 27
		private string _issuer;

		// Token: 0x0400001C RID: 28
		private IEnumerable<SecurityKey> _keys;
	}
}
