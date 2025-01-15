using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200009F RID: 159
	public class DataCacheLocalCacheProperties
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00012C54 File Offset: 0x00010E54
		public DataCacheLocalCacheProperties(long objectCount, TimeSpan defaultTimeout, DataCacheLocalCacheInvalidationPolicy invalidationPolicy)
		{
			if (objectCount <= 0L)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ObjectCountZero"), "objectCount");
			}
			if (defaultTimeout.CompareTo(TimeSpan.Zero) <= 0)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "defaultTimeout");
			}
			this._isEnabled = true;
			this._objectCount = objectCount;
			this._defaultTimeout = defaultTimeout;
			this._invalidationPolicy = invalidationPolicy;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00002061 File Offset: 0x00000261
		public DataCacheLocalCacheProperties()
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00012CCB File Offset: 0x00010ECB
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012CD3 File Offset: 0x00010ED3
		public long ObjectCount
		{
			get
			{
				return this._objectCount;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00012CDB File Offset: 0x00010EDB
		public TimeSpan DefaultTimeout
		{
			get
			{
				return this._defaultTimeout;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00012CE3 File Offset: 0x00010EE3
		public DataCacheLocalCacheInvalidationPolicy InvalidationPolicy
		{
			get
			{
				return this._invalidationPolicy;
			}
		}

		// Token: 0x040002D4 RID: 724
		private bool _isEnabled;

		// Token: 0x040002D5 RID: 725
		private long _objectCount;

		// Token: 0x040002D6 RID: 726
		private TimeSpan _defaultTimeout;

		// Token: 0x040002D7 RID: 727
		private DataCacheLocalCacheInvalidationPolicy _invalidationPolicy;
	}
}
