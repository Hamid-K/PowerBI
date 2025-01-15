using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200005B RID: 91
	public abstract class CacheVersion
	{
		// Token: 0x06000186 RID: 390
		public abstract CacheVersion GetMaxVersion(CacheVersion other);

		// Token: 0x06000187 RID: 391
		public abstract CacheVersion GetMinVersion(CacheVersion other);

		// Token: 0x06000188 RID: 392 RVA: 0x00002F6D File Offset: 0x0000116D
		public static CacheVersion GetMaxVersion(CacheVersion version1, CacheVersion version2)
		{
			if (version1 != null)
			{
				return version1.GetMaxVersion(version2);
			}
			if (version2 != null)
			{
				return version2.GetMaxVersion(version1);
			}
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00002F86 File Offset: 0x00001186
		public static CacheVersion GetMinVersion(CacheVersion version1, CacheVersion version2)
		{
			if (version1 != null)
			{
				return version1.GetMinVersion(version2);
			}
			if (version2 != null)
			{
				return version2.GetMinVersion(version1);
			}
			return null;
		}
	}
}
