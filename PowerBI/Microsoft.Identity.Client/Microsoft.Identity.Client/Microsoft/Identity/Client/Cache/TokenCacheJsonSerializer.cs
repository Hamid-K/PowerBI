using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002B1 RID: 689
	internal class TokenCacheJsonSerializer : ITokenCacheSerializable
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x000543DC File Offset: 0x000525DC
		public TokenCacheJsonSerializer(ITokenCacheAccessor accessor)
		{
			this._accessor = accessor;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000543EC File Offset: 0x000525EC
		public byte[] Serialize(IDictionary<string, JToken> unknownNodes)
		{
			CacheSerializationContract cacheSerializationContract = new CacheSerializationContract(unknownNodes);
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in this._accessor.GetAllAccessTokens(null, null))
			{
				cacheSerializationContract.AccessTokens[msalAccessTokenCacheItem.CacheKey] = msalAccessTokenCacheItem;
			}
			foreach (MsalRefreshTokenCacheItem msalRefreshTokenCacheItem in this._accessor.GetAllRefreshTokens(null, null))
			{
				cacheSerializationContract.RefreshTokens[msalRefreshTokenCacheItem.CacheKey] = msalRefreshTokenCacheItem;
			}
			foreach (MsalIdTokenCacheItem msalIdTokenCacheItem in this._accessor.GetAllIdTokens(null, null))
			{
				cacheSerializationContract.IdTokens[msalIdTokenCacheItem.CacheKey] = msalIdTokenCacheItem;
			}
			foreach (MsalAccountCacheItem msalAccountCacheItem in this._accessor.GetAllAccounts(null, null))
			{
				cacheSerializationContract.Accounts[msalAccountCacheItem.CacheKey] = msalAccountCacheItem;
			}
			foreach (MsalAppMetadataCacheItem msalAppMetadataCacheItem in this._accessor.GetAllAppMetadata())
			{
				cacheSerializationContract.AppMetadata[msalAppMetadataCacheItem.CacheKey] = msalAppMetadataCacheItem;
			}
			return cacheSerializationContract.ToJsonString().ToByteArray();
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000545C0 File Offset: 0x000527C0
		public IDictionary<string, JToken> Deserialize(byte[] bytes, bool clearExistingCacheData)
		{
			string text = CoreHelpers.ByteArrayToString(bytes);
			CacheSerializationContract cacheSerializationContract;
			try
			{
				cacheSerializationContract = CacheSerializationContract.FromJsonString(text);
			}
			catch (Exception ex)
			{
				string text2 = ((text.Length > 5) ? text.Substring(0, 5) : text);
				throw new MsalClientException("json_parse_failed", string.Format("MSAL deserialization failed to parse the cache contents. First characters of the cache string: {0} \r\nPossible cause: token cache encryption is used via Microsoft.Identity.Web.TokenCache and decryption fails, for example. \r\n Full details of inner exception: {1} ", text2, ex), ex);
			}
			if (clearExistingCacheData)
			{
				this._accessor.Clear(null);
			}
			if (cacheSerializationContract.AccessTokens != null)
			{
				foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in cacheSerializationContract.AccessTokens.Values)
				{
					this._accessor.SaveAccessToken(msalAccessTokenCacheItem);
				}
			}
			if (cacheSerializationContract.RefreshTokens != null)
			{
				foreach (MsalRefreshTokenCacheItem msalRefreshTokenCacheItem in cacheSerializationContract.RefreshTokens.Values)
				{
					this._accessor.SaveRefreshToken(msalRefreshTokenCacheItem);
				}
			}
			if (cacheSerializationContract.IdTokens != null)
			{
				foreach (MsalIdTokenCacheItem msalIdTokenCacheItem in cacheSerializationContract.IdTokens.Values)
				{
					this._accessor.SaveIdToken(msalIdTokenCacheItem);
				}
			}
			if (cacheSerializationContract.Accounts != null)
			{
				foreach (MsalAccountCacheItem msalAccountCacheItem in cacheSerializationContract.Accounts.Values)
				{
					this._accessor.SaveAccount(msalAccountCacheItem);
				}
			}
			if (cacheSerializationContract.AppMetadata != null)
			{
				foreach (MsalAppMetadataCacheItem msalAppMetadataCacheItem in cacheSerializationContract.AppMetadata.Values)
				{
					this._accessor.SaveAppMetadata(msalAppMetadataCacheItem);
				}
			}
			return cacheSerializationContract.UnknownNodes;
		}

		// Token: 0x04000BB7 RID: 2999
		private readonly ITokenCacheAccessor _accessor;
	}
}
