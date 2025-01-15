using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A26 RID: 6694
	public sealed class MultiLevelCacheVersion : CacheVersion
	{
		// Token: 0x0600A955 RID: 43349 RVA: 0x002305F8 File Offset: 0x0022E7F8
		public static CacheVersion GetVersion1(CacheVersion version)
		{
			MultiLevelCacheVersion multiLevelCacheVersion = (MultiLevelCacheVersion)version;
			if (multiLevelCacheVersion == null)
			{
				return null;
			}
			return multiLevelCacheVersion.version1;
		}

		// Token: 0x0600A956 RID: 43350 RVA: 0x00230618 File Offset: 0x0022E818
		public static CacheVersion GetVersion2(CacheVersion version)
		{
			MultiLevelCacheVersion multiLevelCacheVersion = (MultiLevelCacheVersion)version;
			if (multiLevelCacheVersion == null)
			{
				return null;
			}
			return multiLevelCacheVersion.version2;
		}

		// Token: 0x0600A957 RID: 43351 RVA: 0x00230637 File Offset: 0x0022E837
		public MultiLevelCacheVersion(CacheVersion version1, CacheVersion version2)
		{
			this.version1 = version1;
			this.version2 = version2;
		}

		// Token: 0x0600A958 RID: 43352 RVA: 0x00230650 File Offset: 0x0022E850
		public override CacheVersion GetMaxVersion(CacheVersion other)
		{
			MultiLevelCacheVersion multiLevelCacheVersion = (MultiLevelCacheVersion)other;
			if (multiLevelCacheVersion == null)
			{
				return this;
			}
			CacheVersion maxVersion = CacheVersion.GetMaxVersion(this.version1, multiLevelCacheVersion.version1);
			CacheVersion maxVersion2 = CacheVersion.GetMaxVersion(this.version2, multiLevelCacheVersion.version2);
			return new MultiLevelCacheVersion(maxVersion, maxVersion2);
		}

		// Token: 0x0600A959 RID: 43353 RVA: 0x00230694 File Offset: 0x0022E894
		public override CacheVersion GetMinVersion(CacheVersion other)
		{
			MultiLevelCacheVersion multiLevelCacheVersion = (MultiLevelCacheVersion)other;
			if (multiLevelCacheVersion == null)
			{
				return this;
			}
			CacheVersion minVersion = CacheVersion.GetMinVersion(this.version1, multiLevelCacheVersion.version1);
			CacheVersion minVersion2 = CacheVersion.GetMinVersion(this.version2, multiLevelCacheVersion.version2);
			return new MultiLevelCacheVersion(minVersion, minVersion2);
		}

		// Token: 0x04005821 RID: 22561
		private readonly CacheVersion version1;

		// Token: 0x04005822 RID: 22562
		private readonly CacheVersion version2;
	}
}
