using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026C RID: 620
	[DataContract(Name = "CacheRegion", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CacheRegion : IEquatable<CacheRegion>
	{
		// Token: 0x060014C7 RID: 5319 RVA: 0x000408FC File Offset: 0x0003EAFC
		internal CacheRegion(string cache, string region)
		{
			this._cacheName = cache;
			this._regionName = region;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00040914 File Offset: 0x0003EB14
		public override bool Equals(object obj)
		{
			CacheRegion cacheRegion = obj as CacheRegion;
			return cacheRegion != null && this.Equals(cacheRegion);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00040934 File Offset: 0x0003EB34
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = (int)CsHash32.ComputeString(this._cacheName + this._regionName, 0U, false);
			}
			return this._hashCode;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00040964 File Offset: 0x0003EB64
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0};{1}]", new object[] { this._cacheName, this._regionName });
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0004099C File Offset: 0x0003EB9C
		public bool Equals(CacheRegion other)
		{
			bool flag = false;
			if (other != null)
			{
				if (this.GetHashCode() != other.GetHashCode())
				{
					return flag;
				}
				flag = other._cacheName.Equals(this._cacheName) && other._regionName.Equals(this._regionName);
			}
			return flag;
		}

		// Token: 0x04000C53 RID: 3155
		[DataMember]
		private string _cacheName;

		// Token: 0x04000C54 RID: 3156
		[DataMember]
		private string _regionName;

		// Token: 0x04000C55 RID: 3157
		private int _hashCode;
	}
}
