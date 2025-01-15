using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000056 RID: 86
	[Obsolete]
	public interface IPersistentCacheConfig
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015F RID: 351
		PersistentCacheMode Mode { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000160 RID: 352
		string Directory { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000161 RID: 353
		long MaxCacheSize { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000162 RID: 354
		long TrimCacheSize { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000163 RID: 355
		long MaxEntryLength { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000164 RID: 356
		int MaxObjectCacheSize { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000165 RID: 357
		int TrimObjectCacheSize { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000166 RID: 358
		bool RefreshData { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000167 RID: 359
		bool UserSpecific { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000168 RID: 360
		bool CancelCommitsOnDispose { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000169 RID: 361
		DateTime MaxStaleness { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016A RID: 362
		CacheVersion DiskMinVersion { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600016B RID: 363
		string EncryptionCertificateThumbprint { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016C RID: 364
		int ImplementationVersion { get; }
	}
}
