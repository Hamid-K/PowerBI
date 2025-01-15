using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200001E RID: 30
	public interface ICacheManager
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000076 RID: 118
		string CacheGroup { get; }

		// Token: 0x06000077 RID: 119
		CacheManagerCacheInfo CreateCache(IRecordValue configuration);

		// Token: 0x06000078 RID: 120
		CacheManagerCacheInfo[] ListCaches();

		// Token: 0x06000079 RID: 121
		void UpdateCache(string identifier, string newIdentifier, bool? readOnly);

		// Token: 0x0600007A RID: 122
		void DeleteCache(string identifier);

		// Token: 0x0600007B RID: 123
		ICacheSet GetCache(string identifier);

		// Token: 0x0600007C RID: 124
		CacheManagerCacheInfo GetCacheFromDirectory(string directory);
	}
}
