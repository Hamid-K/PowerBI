using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C6 RID: 710
	internal class ServerAcsSecurity
	{
		// Token: 0x06001A48 RID: 6728 RVA: 0x0004F68F File Offset: 0x0004D88F
		public ServerAcsSecurity(ServiceConfigurationManager configurationManager)
		{
			this._configurationManager = configurationManager;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0004F6A0 File Offset: 0x0004D8A0
		internal bool IsHeaderValid(string signedAuthHeader, out DateTime expiry, out ErrStatus err)
		{
			expiry = DateTime.MinValue;
			bool flag;
			try
			{
				Token token = new Token(signedAuthHeader);
				TokenValidator tokenValidator = new TokenValidator(this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.AcsHostName, this._configurationManager.AdvancedProperties.DnsDomain, new string[]
				{
					this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.SigningKey,
					this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.AdditionalSigningKey
				});
				if (!tokenValidator.ValidateEx(token, out err))
				{
					flag = false;
				}
				else if (!ServerAcsSecurity.AreClaimsValid(token))
				{
					err = ErrStatus.AUTH_HEADER_INVALID;
					flag = false;
				}
				else
				{
					expiry = token.Expiry;
					err = ErrStatus.UNINITIALIZED_ERROR;
					flag = true;
				}
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode != 19003)
				{
					throw;
				}
				err = ErrStatus.INVALID_REQUEST_BODY;
				flag = false;
			}
			catch (ArgumentException ex2)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, ArgumentException>("DistributedCache.ACS", "Unable to parse auth header: {0}. Exception: {1}", signedAuthHeader, ex2);
				}
				err = ErrStatus.INVALID_REQUEST_BODY;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0004F7C8 File Offset: 0x0004D9C8
		private static bool AreClaimsValid(Token token)
		{
			string text;
			return token.TryGetClaim("net.windows.cache.action", out text) && text.Equals("ReadWrite");
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0004F7F4 File Offset: 0x0004D9F4
		public void ValidateMessage(string requestedCacheName, string requestedCacheEndpoint, string tokenStr)
		{
			if (string.IsNullOrEmpty(tokenStr))
			{
				throw new DataCacheException("DistributedCache.ACS", 19002, "ACS Token is null/empty");
			}
			Token token = this.ValidateTokenStr(requestedCacheName, requestedCacheEndpoint, tokenStr);
			ServerAcsSecurity.ValidateClaims(token);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0004F830 File Offset: 0x0004DA30
		private static void ValidateClaims(Token token)
		{
			string text;
			if (!token.TryGetClaim("net.windows.cache.action", out text))
			{
				throw new DataCacheException("DistributedCache.ACS", 19002, "ACS Token does not have action claims");
			}
			if (!text.Equals("ReadWrite"))
			{
				throw new DataCacheException("DistributedCache.ACS", 19002, "ACS Token does not have action claims for client operations");
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0004F884 File Offset: 0x0004DA84
		private Token ValidateTokenStr(string requestedCacheName, string requestedCacheEndpoint, string tokenStr)
		{
			Token token = new Token(tokenStr);
			TokenValidator tokenValidator = new TokenValidator(this.GetTrustedIssuer(requestedCacheName), requestedCacheEndpoint, new string[]
			{
				this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.SigningKey,
				this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.AdditionalSigningKey
			});
			if (!tokenValidator.Validate(token))
			{
				throw new DataCacheException("DistributedCache.ACS", 19002, "Could not validate token");
			}
			return token;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0004F908 File Offset: 0x0004DB08
		private string GetTrustedIssuer(string requestedCacheName)
		{
			string text = requestedCacheName + "-cache";
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				text,
				this._configurationManager.AdvancedProperties.SecurityProperties.AcsSecurity.AcsHostName
			});
		}

		// Token: 0x04000E05 RID: 3589
		private const string logSource = "DistributedCache.ACS";

		// Token: 0x04000E06 RID: 3590
		private const string internalAcsEndpointSuffix = "-cache";

		// Token: 0x04000E07 RID: 3591
		private ServiceConfigurationManager _configurationManager;
	}
}
