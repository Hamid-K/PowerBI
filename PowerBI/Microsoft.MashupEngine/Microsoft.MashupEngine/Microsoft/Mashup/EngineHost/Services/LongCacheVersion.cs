using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A0F RID: 6671
	public sealed class LongCacheVersion : CacheVersion
	{
		// Token: 0x0600A8E6 RID: 43238 RVA: 0x0022F4C0 File Offset: 0x0022D6C0
		public static CacheVersion New(long version)
		{
			if (version < 0L)
			{
				return null;
			}
			return new LongCacheVersion(version);
		}

		// Token: 0x0600A8E7 RID: 43239 RVA: 0x0022F4D0 File Offset: 0x0022D6D0
		public static long ToLong(CacheVersion version)
		{
			LongCacheVersion longCacheVersion = (LongCacheVersion)version;
			if (longCacheVersion == null)
			{
				return -1L;
			}
			return longCacheVersion.version;
		}

		// Token: 0x0600A8E8 RID: 43240 RVA: 0x0022F4F0 File Offset: 0x0022D6F0
		private LongCacheVersion(long version)
		{
			this.version = version;
		}

		// Token: 0x0600A8E9 RID: 43241 RVA: 0x0022F500 File Offset: 0x0022D700
		public override CacheVersion GetMaxVersion(CacheVersion other)
		{
			LongCacheVersion longCacheVersion = (LongCacheVersion)other;
			if (longCacheVersion != null && longCacheVersion.version > this.version)
			{
				return other;
			}
			return this;
		}

		// Token: 0x0600A8EA RID: 43242 RVA: 0x0022F528 File Offset: 0x0022D728
		public override CacheVersion GetMinVersion(CacheVersion other)
		{
			LongCacheVersion longCacheVersion = (LongCacheVersion)other;
			if (longCacheVersion != null && longCacheVersion.version < this.version)
			{
				return other;
			}
			return this;
		}

		// Token: 0x040057DB RID: 22491
		private readonly long version;
	}
}
