using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002B5 RID: 693
	internal class CacheSerializationContract
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00054AD6 File Offset: 0x00052CD6
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x00054ADE File Offset: 0x00052CDE
		public Dictionary<string, MsalAccessTokenCacheItem> AccessTokens { get; set; } = new Dictionary<string, MsalAccessTokenCacheItem>();

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00054AE7 File Offset: 0x00052CE7
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00054AEF File Offset: 0x00052CEF
		public Dictionary<string, MsalRefreshTokenCacheItem> RefreshTokens { get; set; } = new Dictionary<string, MsalRefreshTokenCacheItem>();

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00054AF8 File Offset: 0x00052CF8
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00054B00 File Offset: 0x00052D00
		public Dictionary<string, MsalIdTokenCacheItem> IdTokens { get; set; } = new Dictionary<string, MsalIdTokenCacheItem>();

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00054B09 File Offset: 0x00052D09
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x00054B11 File Offset: 0x00052D11
		public Dictionary<string, MsalAccountCacheItem> Accounts { get; set; } = new Dictionary<string, MsalAccountCacheItem>();

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00054B1A File Offset: 0x00052D1A
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x00054B22 File Offset: 0x00052D22
		public Dictionary<string, MsalAppMetadataCacheItem> AppMetadata { get; set; } = new Dictionary<string, MsalAppMetadataCacheItem>();

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00054B2B File Offset: 0x00052D2B
		public IDictionary<string, JToken> UnknownNodes { get; }

		// Token: 0x060019E7 RID: 6631 RVA: 0x00054B34 File Offset: 0x00052D34
		public CacheSerializationContract(IDictionary<string, JToken> unknownNodes)
		{
			this.UnknownNodes = unknownNodes ?? new Dictionary<string, JToken>();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00054B90 File Offset: 0x00052D90
		internal static CacheSerializationContract FromJsonString(string json)
		{
			JObject jobject = JObject.Parse(json);
			CacheSerializationContract cacheSerializationContract = new CacheSerializationContract(CacheSerializationContract.ExtractUnknownNodes(jobject));
			if (jobject.ContainsKey("AccessToken"))
			{
				foreach (JObject jobject2 in CacheSerializationContract.<FromJsonString>g__GetElement|25_0(jobject, "AccessToken"))
				{
					if (jobject2 != null)
					{
						MsalAccessTokenCacheItem msalAccessTokenCacheItem = MsalAccessTokenCacheItem.FromJObject(jobject2);
						cacheSerializationContract.AccessTokens[msalAccessTokenCacheItem.CacheKey] = msalAccessTokenCacheItem;
					}
				}
			}
			if (jobject.ContainsKey("RefreshToken"))
			{
				foreach (JObject jobject3 in CacheSerializationContract.<FromJsonString>g__GetElement|25_0(jobject, "RefreshToken"))
				{
					if (jobject3 != null)
					{
						MsalRefreshTokenCacheItem msalRefreshTokenCacheItem = MsalRefreshTokenCacheItem.FromJObject(jobject3);
						cacheSerializationContract.RefreshTokens[msalRefreshTokenCacheItem.CacheKey] = msalRefreshTokenCacheItem;
					}
				}
			}
			if (jobject.ContainsKey("IdToken"))
			{
				foreach (JObject jobject4 in CacheSerializationContract.<FromJsonString>g__GetElement|25_0(jobject, "IdToken"))
				{
					if (jobject4 != null)
					{
						MsalIdTokenCacheItem msalIdTokenCacheItem = MsalIdTokenCacheItem.FromJObject(jobject4);
						cacheSerializationContract.IdTokens[msalIdTokenCacheItem.CacheKey] = msalIdTokenCacheItem;
					}
				}
			}
			if (jobject.ContainsKey("Account"))
			{
				foreach (JObject jobject5 in CacheSerializationContract.<FromJsonString>g__GetElement|25_0(jobject, "Account"))
				{
					if (jobject5 != null)
					{
						MsalAccountCacheItem msalAccountCacheItem = MsalAccountCacheItem.FromJObject(jobject5);
						cacheSerializationContract.Accounts[msalAccountCacheItem.CacheKey] = msalAccountCacheItem;
					}
				}
			}
			if (jobject.ContainsKey("AppMetadata"))
			{
				foreach (JObject jobject6 in CacheSerializationContract.<FromJsonString>g__GetElement|25_0(jobject, "AppMetadata"))
				{
					if (jobject6 != null)
					{
						MsalAppMetadataCacheItem msalAppMetadataCacheItem = MsalAppMetadataCacheItem.FromJObject(jobject6);
						cacheSerializationContract.AppMetadata[msalAppMetadataCacheItem.CacheKey] = msalAppMetadataCacheItem;
					}
				}
			}
			return cacheSerializationContract;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00054DC0 File Offset: 0x00052FC0
		private static IDictionary<string, JToken> ExtractUnknownNodes(JObject root)
		{
			return root.Where((KeyValuePair<string, JToken> kvp) => !CacheSerializationContract.s_knownPropertyNames.Any((string p) => string.Equals(kvp.Key, p, StringComparison.OrdinalIgnoreCase))).ToDictionary((KeyValuePair<string, JToken> kvp) => kvp.Key, (KeyValuePair<string, JToken> kvp) => kvp.Value);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00054E38 File Offset: 0x00053038
		internal string ToJsonString()
		{
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			foreach (KeyValuePair<string, MsalAccessTokenCacheItem> keyValuePair in this.AccessTokens)
			{
				jobject2[keyValuePair.Key] = keyValuePair.Value.ToJObject();
			}
			jobject["AccessToken"] = jobject2;
			JObject jobject3 = new JObject();
			foreach (KeyValuePair<string, MsalRefreshTokenCacheItem> keyValuePair2 in this.RefreshTokens)
			{
				jobject3[keyValuePair2.Key] = keyValuePair2.Value.ToJObject();
			}
			jobject["RefreshToken"] = jobject3;
			JObject jobject4 = new JObject();
			foreach (KeyValuePair<string, MsalIdTokenCacheItem> keyValuePair3 in this.IdTokens)
			{
				jobject4[keyValuePair3.Key] = keyValuePair3.Value.ToJObject();
			}
			jobject["IdToken"] = jobject4;
			JObject jobject5 = new JObject();
			foreach (KeyValuePair<string, MsalAccountCacheItem> keyValuePair4 in this.Accounts)
			{
				jobject5[keyValuePair4.Key] = keyValuePair4.Value.ToJObject();
			}
			jobject["Account"] = jobject5;
			JObject jobject6 = new JObject();
			foreach (KeyValuePair<string, MsalAppMetadataCacheItem> keyValuePair5 in this.AppMetadata)
			{
				jobject6[keyValuePair5.Key] = keyValuePair5.Value.ToJObject();
			}
			jobject["AppMetadata"] = jobject6;
			foreach (KeyValuePair<string, JToken> keyValuePair6 in this.UnknownNodes)
			{
				jobject[keyValuePair6.Key] = keyValuePair6.Value;
			}
			return JsonConvert.SerializeObject(jobject, Formatting.None, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include
			});
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000550F5 File Offset: 0x000532F5
		[CompilerGenerated]
		internal static IEnumerable<JObject> <FromJsonString>g__GetElement|25_0(JObject root, string key)
		{
			foreach (JToken jtoken in root[key].Values())
			{
				yield return jtoken as JObject;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000BBE RID: 3006
		private static readonly IEnumerable<string> s_knownPropertyNames = new string[] { "AccessToken", "RefreshToken", "IdToken", "Account", "AppMetadata" };
	}
}
