using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A5 RID: 677
	internal static class CacheFallbackOperations
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x00053648 File Offset: 0x00051848
		public static void WriteAdalRefreshToken(ILoggerAdapter logger, ILegacyCachePersistence legacyCachePersistence, MsalRefreshTokenCacheItem rtItem, MsalIdTokenCacheItem idItem, string authority, string uniqueId, string scope)
		{
			try
			{
				if (rtItem == null)
				{
					logger.Info("No refresh token available. Skipping writing to ADAL legacy cache. ");
				}
				else if (!string.IsNullOrEmpty(rtItem.FamilyId))
				{
					logger.Info("Not writing FRT in ADAL legacy cache. ");
				}
				else
				{
					AdalTokenCacheKey adalTokenCacheKey = new AdalTokenCacheKey(authority, scope, rtItem.ClientId, TokenSubjectType.User, uniqueId, idItem.IdToken.PreferredUsername);
					AdalResultWrapper adalResultWrapper = new AdalResultWrapper
					{
						Result = new AdalResult
						{
							UserInfo = new AdalUserInfo
							{
								UniqueId = uniqueId,
								DisplayableId = idItem.IdToken.PreferredUsername
							}
						},
						RefreshToken = rtItem.Secret,
						RawClientInfo = rtItem.RawClientInfo,
						ResourceInResponse = scope
					};
					IDictionary<AdalTokenCacheKey, AdalResultWrapper> dictionary = AdalCacheOperations.Deserialize(logger, legacyCachePersistence.LoadCache());
					dictionary[adalTokenCacheKey] = adalResultWrapper;
					legacyCachePersistence.WriteCache(AdalCacheOperations.Serialize(logger, dictionary));
				}
			}
			catch (Exception ex)
			{
				if (!string.Equals((rtItem != null) ? rtItem.Environment : null, (idItem != null) ? idItem.Environment : null, StringComparison.OrdinalIgnoreCase))
				{
					logger.Error("Not expecting the RT and IdT to have different env when adding to legacy cache");
				}
				if (!string.Equals((rtItem != null) ? rtItem.Environment : null, new Uri(authority).Host, StringComparison.OrdinalIgnoreCase))
				{
					logger.Error("Not expecting authority to have a different env than the RT and IdT");
				}
				logger.WarningPiiWithPrefix(ex, "An error occurred while writing MSAL refresh token to the cache in ADAL format. For details please see https://aka.ms/net-cache-persistence-errors. ");
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00053794 File Offset: 0x00051994
		public static AdalUsersForMsal GetAllAdalUsersForMsal(ILoggerAdapter logger, ILegacyCachePersistence legacyCachePersistence, string clientId)
		{
			List<AdalUserForMsalEntry> userEntries = new List<AdalUserForMsalEntry>();
			try
			{
				(from p in AdalCacheOperations.Deserialize(logger, legacyCachePersistence.LoadCache())
					where p.Key.ClientId.Equals(clientId, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(p.Key.Authority)
					select p).ToList<KeyValuePair<AdalTokenCacheKey, AdalResultWrapper>>().ForEach(delegate(KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> kvp)
				{
					List<AdalUserForMsalEntry> userEntries2 = userEntries;
					string authority = kvp.Key.Authority;
					userEntries2.Add(new AdalUserForMsalEntry(clientId, authority, kvp.Value.RawClientInfo, kvp.Value.Result.UserInfo));
				});
			}
			catch (Exception ex)
			{
				logger.WarningPiiWithPrefix(ex, "An error occurred while reading accounts in ADAL format from the cache for MSAL. For details please see https://aka.ms/net-cache-persistence-errors. ");
			}
			return new AdalUsersForMsal(userEntries);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00053818 File Offset: 0x00051A18
		public static void RemoveAdalUser(ILoggerAdapter logger, ILegacyCachePersistence legacyCachePersistence, string clientId, string displayableId, string accountOrUserId)
		{
			try
			{
				IDictionary<AdalTokenCacheKey, AdalResultWrapper> dictionary = AdalCacheOperations.Deserialize(logger, legacyCachePersistence.LoadCache());
				if (!string.IsNullOrEmpty(accountOrUserId))
				{
					CacheFallbackOperations.RemoveEntriesWithMatchingId(clientId, accountOrUserId, dictionary);
				}
				CacheFallbackOperations.RemoveEntriesWithMatchingName(logger, clientId, displayableId, dictionary);
				legacyCachePersistence.WriteCache(AdalCacheOperations.Serialize(logger, dictionary));
			}
			catch (Exception ex)
			{
				logger.WarningPiiWithPrefix(ex, "An error occurred while deleting account in ADAL format from the cache. For details please see https://aka.ms/net-cache-persistence-errors. ");
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005387C File Offset: 0x00051A7C
		private static void RemoveEntriesWithMatchingName(ILoggerAdapter logger, string clientId, string displayableId, IDictionary<AdalTokenCacheKey, AdalResultWrapper> adalCache)
		{
			if (string.IsNullOrEmpty(displayableId))
			{
				logger.Error("Internal error - trying to remove an MSAL user with an empty username. Possible cache corruption. See https://aka.ms/adal_token_cache_serialization. ");
				return;
			}
			List<AdalTokenCacheKey> list = new List<AdalTokenCacheKey>();
			foreach (KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> keyValuePair in adalCache)
			{
				string displayableId2 = keyValuePair.Key.DisplayableId;
				string clientId2 = keyValuePair.Key.ClientId;
				if (string.Equals(displayableId, displayableId2, StringComparison.OrdinalIgnoreCase) && string.Equals(clientId, clientId2, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (AdalTokenCacheKey adalTokenCacheKey in list)
			{
				adalCache.Remove(adalTokenCacheKey);
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00053958 File Offset: 0x00051B58
		private static void RemoveEntriesWithMatchingId(string clientId, string accountOrUserId, IDictionary<AdalTokenCacheKey, AdalResultWrapper> adalCache)
		{
			List<AdalTokenCacheKey> list = new List<AdalTokenCacheKey>();
			foreach (KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> keyValuePair in adalCache)
			{
				string rawClientInfo = keyValuePair.Value.RawClientInfo;
				if (!string.IsNullOrEmpty(rawClientInfo))
				{
					string text = ClientInfo.CreateFromJson(rawClientInfo).ToAccountIdentifier();
					string clientId2 = keyValuePair.Key.ClientId;
					if (string.Equals(accountOrUserId, text, StringComparison.OrdinalIgnoreCase) && string.Equals(clientId, clientId2, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			foreach (AdalTokenCacheKey adalTokenCacheKey in list)
			{
				adalCache.Remove(adalTokenCacheKey);
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00053A34 File Offset: 0x00051C34
		public static MsalRefreshTokenCacheItem GetRefreshToken(ILoggerAdapter logger, ILegacyCachePersistence legacyCachePersistence, IEnumerable<string> environmentAliases, string clientId, IAccount account)
		{
			MsalRefreshTokenCacheItem msalRefreshTokenCacheItem;
			try
			{
				IEnumerable<KeyValuePair<AdalTokenCacheKey, AdalResultWrapper>> enumerable = from p in AdalCacheOperations.Deserialize(logger, legacyCachePersistence.LoadCache())
					where p.Key.ClientId.Equals(clientId, StringComparison.OrdinalIgnoreCase) && environmentAliases.Contains(new Uri(p.Key.Authority).Host)
					select p;
				bool flag = false;
				IAccount account2 = account;
				if (!string.IsNullOrEmpty((account2 != null) ? account2.Username : null))
				{
					enumerable = enumerable.Where((KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> p) => account.Username.Equals(p.Key.DisplayableId, StringComparison.OrdinalIgnoreCase));
					flag = true;
				}
				IAccount account3 = account;
				string text;
				if (account3 == null)
				{
					text = null;
				}
				else
				{
					AccountId homeAccountId = account3.HomeAccountId;
					text = ((homeAccountId != null) ? homeAccountId.ObjectId : null);
				}
				if (!string.IsNullOrEmpty(text))
				{
					enumerable = enumerable.Where((KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> p) => account.HomeAccountId.ObjectId.Equals(p.Key.UniqueId, StringComparison.OrdinalIgnoreCase)).ToList<KeyValuePair<AdalTokenCacheKey, AdalResultWrapper>>();
					flag = true;
				}
				if (!flag)
				{
					logger.Warning("Could not filter ADAL entries by either UPN or unique ID, skipping. ");
					msalRefreshTokenCacheItem = null;
				}
				else
				{
					msalRefreshTokenCacheItem = enumerable.Select((KeyValuePair<AdalTokenCacheKey, AdalResultWrapper> adalEntry) => new MsalRefreshTokenCacheItem(new Uri(adalEntry.Key.Authority).Host, adalEntry.Key.ClientId, adalEntry.Value.RefreshToken, adalEntry.Value.RawClientInfo, null, CacheFallbackOperations.GetHomeAccountId(adalEntry.Value))).FirstOrDefault<MsalRefreshTokenCacheItem>();
				}
			}
			catch (Exception ex)
			{
				logger.WarningPiiWithPrefix(ex, "An error occurred while searching for refresh tokens in ADAL format in the cache for MSAL. For details please see https://aka.ms/net-cache-persistence-errors. ");
				msalRefreshTokenCacheItem = null;
			}
			return msalRefreshTokenCacheItem;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00053B50 File Offset: 0x00051D50
		private static string GetHomeAccountId(AdalResultWrapper adalResultWrapper)
		{
			if (!string.IsNullOrEmpty(adalResultWrapper.RawClientInfo))
			{
				return ClientInfo.CreateFromJson(adalResultWrapper.RawClientInfo).ToAccountIdentifier();
			}
			return null;
		}

		// Token: 0x04000B7A RID: 2938
		internal const string DifferentEnvError = "Not expecting the RT and IdT to have different env when adding to legacy cache";

		// Token: 0x04000B7B RID: 2939
		internal const string DifferentAuthorityError = "Not expecting authority to have a different env than the RT and IdT";
	}
}
