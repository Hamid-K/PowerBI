using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E4 RID: 484
	[DataContract]
	public class DataCacheItemKey
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00035E26 File Offset: 0x00034026
		public string Region
		{
			get
			{
				return this._region;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00035E2E File Offset: 0x0003402E
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00035E36 File Offset: 0x00034036
		public DataCacheItemKey(string region, string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._region = region;
			this._key = key;
			this._hashCode = DataCacheItemKey.GetHashCode(key, region);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00035E68 File Offset: 0x00034068
		public override bool Equals(object obj)
		{
			DataCacheItemKey dataCacheItemKey = obj as DataCacheItemKey;
			return dataCacheItemKey != null && DataCacheItemKey.Equals(this._key, this._region, dataCacheItemKey._key, dataCacheItemKey._region);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00035E9E File Offset: 0x0003409E
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00035EA6 File Offset: 0x000340A6
		internal static bool Equals(string key, string region, string otherKey, string otherRegion)
		{
			return string.Equals(region, otherRegion, StringComparison.Ordinal) && string.Equals(key, otherKey, StringComparison.Ordinal);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00035EBC File Offset: 0x000340BC
		internal static int GetHashCode(string key, string region)
		{
			return DataCacheItemKey.GetHashCode(Microsoft.ApplicationServer.Caching.Key.ComputeHash(key), region);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00035ECA File Offset: 0x000340CA
		internal static int GetHashCode(int keyHashCode, string region)
		{
			return keyHashCode + region.GetHashCode();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00035ED4 File Offset: 0x000340D4
		internal static DataCacheItemKey Create(AOMCacheItem item)
		{
			return new DataCacheItemKey(item.RegionName, item.Key.StringValue);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00035EEC File Offset: 0x000340EC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[] { this.Region, this.Key });
		}

		// Token: 0x04000AA5 RID: 2725
		[DataMember]
		private readonly string _region;

		// Token: 0x04000AA6 RID: 2726
		[DataMember]
		private readonly string _key;

		// Token: 0x04000AA7 RID: 2727
		[DataMember]
		private readonly int _hashCode;
	}
}
