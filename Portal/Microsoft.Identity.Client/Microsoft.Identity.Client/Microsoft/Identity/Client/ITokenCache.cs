using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000156 RID: 342
	public interface ITokenCache
	{
		// Token: 0x060010F3 RID: 4339
		void SetBeforeAccess(TokenCacheCallback beforeAccess);

		// Token: 0x060010F4 RID: 4340
		void SetAfterAccess(TokenCacheCallback afterAccess);

		// Token: 0x060010F5 RID: 4341
		void SetBeforeWrite(TokenCacheCallback beforeWrite);

		// Token: 0x060010F6 RID: 4342
		void SetBeforeAccessAsync(Func<TokenCacheNotificationArgs, Task> beforeAccess);

		// Token: 0x060010F7 RID: 4343
		void SetAfterAccessAsync(Func<TokenCacheNotificationArgs, Task> afterAccess);

		// Token: 0x060010F8 RID: 4344
		void SetBeforeWriteAsync(Func<TokenCacheNotificationArgs, Task> beforeWrite);

		// Token: 0x060010F9 RID: 4345
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.SerializeMsalV3 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		byte[] SerializeMsalV3();

		// Token: 0x060010FA RID: 4346
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.DeserializeMsalV3 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		void DeserializeMsalV3(byte[] msalV3State, bool shouldClearExistingCache = false);

		// Token: 0x060010FB RID: 4347
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.SerializeMsalV2 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		byte[] SerializeMsalV2();

		// Token: 0x060010FC RID: 4348
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.DeserializeMsalV2 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		void DeserializeMsalV2(byte[] msalV2State);

		// Token: 0x060010FD RID: 4349
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.SerializeAdalV3 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		byte[] SerializeAdalV3();

		// Token: 0x060010FE RID: 4350
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ITokenCacheSerializer.DeserializeAdalV3 on the TokenCacheNotificationArgs in the cache callback. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		void DeserializeAdalV3(byte[] adalV3State);

		// Token: 0x060010FF RID: 4351
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		byte[] Serialize();

		// Token: 0x06001100 RID: 4352
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		void Deserialize(byte[] msalV2State);

		// Token: 0x06001101 RID: 4353
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		CacheData SerializeUnifiedAndAdalCache();

		// Token: 0x06001102 RID: 4354
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		void DeserializeUnifiedAndAdalCache(CacheData cacheData);
	}
}
