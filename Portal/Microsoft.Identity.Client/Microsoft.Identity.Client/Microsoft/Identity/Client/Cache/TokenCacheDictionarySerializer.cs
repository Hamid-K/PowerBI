using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002B0 RID: 688
	internal class TokenCacheDictionarySerializer : ITokenCacheSerializable
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0005401B File Offset: 0x0005221B
		public TokenCacheDictionarySerializer(ITokenCacheAccessor accessor)
		{
			this._accessor = accessor;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005402C File Offset: 0x0005222C
		public byte[] Serialize(IDictionary<string, JToken> unknownNodes)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			List<string> list4 = new List<string>();
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in this._accessor.GetAllAccessTokens(null, null))
			{
				list.Add(msalAccessTokenCacheItem.ToJsonString());
			}
			foreach (MsalRefreshTokenCacheItem msalRefreshTokenCacheItem in this._accessor.GetAllRefreshTokens(null, null))
			{
				list2.Add(msalRefreshTokenCacheItem.ToJsonString());
			}
			foreach (MsalIdTokenCacheItem msalIdTokenCacheItem in this._accessor.GetAllIdTokens(null, null))
			{
				list3.Add(msalIdTokenCacheItem.ToJsonString());
			}
			foreach (MsalAccountCacheItem msalAccountCacheItem in this._accessor.GetAllAccounts(null, null))
			{
				list4.Add(msalAccountCacheItem.ToJsonString());
			}
			Dictionary<string, IEnumerable<string>> dictionary = new Dictionary<string, IEnumerable<string>>();
			dictionary["access_tokens"] = list;
			dictionary["refresh_tokens"] = list2;
			dictionary["id_tokens"] = list3;
			dictionary["accounts"] = list4;
			return JsonHelper.SerializeToJson<List<KeyValuePair<string, IEnumerable<string>>>>(dictionary.ToList<KeyValuePair<string, IEnumerable<string>>>()).ToByteArray();
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000541E0 File Offset: 0x000523E0
		public IDictionary<string, JToken> Deserialize(byte[] bytes, bool clearExistingCacheData)
		{
			List<KeyValuePair<string, IEnumerable<string>>> list;
			try
			{
				list = JsonHelper.DeserializeFromJson<List<KeyValuePair<string, IEnumerable<string>>>>(bytes);
			}
			catch (Exception ex)
			{
				throw new MsalClientException("json_parse_failed", "MSAL V2 Deserialization failed to parse the cache contents. Is this possibly an earlier format needed for DeserializeMsalV3?  (See https://aka.ms/msal-net-3x-cache-breaking-change). ", ex);
			}
			Dictionary<string, IEnumerable<string>> dictionary = list.ToDictionary((KeyValuePair<string, IEnumerable<string>> x) => x.Key, (KeyValuePair<string, IEnumerable<string>> x) => x.Value);
			if (clearExistingCacheData)
			{
				this._accessor.Clear(null);
			}
			if (list == null || list.Count == 0)
			{
				return null;
			}
			IEnumerable<string> enumerable;
			if (dictionary.TryGetValue("access_tokens", out enumerable))
			{
				foreach (string text in enumerable)
				{
					this._accessor.SaveAccessToken(MsalAccessTokenCacheItem.FromJsonString(text));
				}
			}
			IEnumerable<string> enumerable2;
			if (dictionary.TryGetValue("refresh_tokens", out enumerable2))
			{
				foreach (string text2 in enumerable2)
				{
					this._accessor.SaveRefreshToken(MsalRefreshTokenCacheItem.FromJsonString(text2));
				}
			}
			IEnumerable<string> enumerable3;
			if (dictionary.TryGetValue("id_tokens", out enumerable3))
			{
				foreach (string text3 in enumerable3)
				{
					this._accessor.SaveIdToken(MsalIdTokenCacheItem.FromJsonString(text3));
				}
			}
			IEnumerable<string> enumerable4;
			if (dictionary.TryGetValue("accounts", out enumerable4))
			{
				foreach (string text4 in enumerable4)
				{
					this._accessor.SaveAccount(MsalAccountCacheItem.FromJsonString(text4));
				}
			}
			return null;
		}

		// Token: 0x04000BB2 RID: 2994
		private const string AccessTokenKey = "access_tokens";

		// Token: 0x04000BB3 RID: 2995
		private const string RefreshTokenKey = "refresh_tokens";

		// Token: 0x04000BB4 RID: 2996
		private const string IdTokenKey = "id_tokens";

		// Token: 0x04000BB5 RID: 2997
		private const string AccountKey = "accounts";

		// Token: 0x04000BB6 RID: 2998
		private readonly ITokenCacheAccessor _accessor;
	}
}
