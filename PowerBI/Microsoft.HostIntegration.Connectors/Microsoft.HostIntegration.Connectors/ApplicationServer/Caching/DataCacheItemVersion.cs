using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E9 RID: 745
	public class DataCacheItemVersion : IComparable<DataCacheItemVersion>
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0005407B File Offset: 0x0005227B
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00054083 File Offset: 0x00052283
		internal InternalCacheItemVersion InternalVersion
		{
			get
			{
				return this._internalVersion;
			}
			set
			{
				this._internalVersion = value;
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0005408C File Offset: 0x0005228C
		public override bool Equals(object obj)
		{
			return !object.Equals(obj, null) && ((DataCacheItemVersion)obj)._internalVersion == this._internalVersion;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000540AF File Offset: 0x000522AF
		public static bool operator ==(DataCacheItemVersion left, DataCacheItemVersion right)
		{
			return (object.Equals(left, null) && object.Equals(right, null)) || (!object.Equals(left, null) && left.Equals(right));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000540D7 File Offset: 0x000522D7
		public static bool operator !=(DataCacheItemVersion left, DataCacheItemVersion right)
		{
			return !(left == right);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000540E3 File Offset: 0x000522E3
		public static bool operator <(DataCacheItemVersion left, DataCacheItemVersion right)
		{
			return (!object.Equals(left, null) || !object.Equals(right, null)) && (object.Equals(left, null) || left.CompareTo(right) < 0);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0005410E File Offset: 0x0005230E
		public static bool operator >(DataCacheItemVersion left, DataCacheItemVersion right)
		{
			return (!object.Equals(left, null) || !object.Equals(right, null)) && !object.Equals(left, null) && left.CompareTo(right) > 0;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00054139 File Offset: 0x00052339
		public static bool IsEmpty(DataCacheItemVersion version)
		{
			return version == null || version._internalVersion == InternalCacheItemVersion.Null;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00054156 File Offset: 0x00052356
		public int CompareTo(DataCacheItemVersion other)
		{
			if (object.Equals(other, null))
			{
				return -1;
			}
			return this._internalVersion.CompareTo(other._internalVersion);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00054174 File Offset: 0x00052374
		public override int GetHashCode()
		{
			return this._internalVersion.GetHashCode();
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00054187 File Offset: 0x00052387
		internal DataCacheItemVersion(InternalCacheItemVersion internalVersion)
		{
			this._internalVersion = internalVersion;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00054198 File Offset: 0x00052398
		internal int Size
		{
			get
			{
				return this._internalVersion.Size;
			}
		}

		// Token: 0x04000EC5 RID: 3781
		private InternalCacheItemVersion _internalVersion;
	}
}
