using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F4 RID: 500
	[DataContract]
	internal class WBCheckPointItem
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00036BEA File Offset: 0x00034DEA
		internal WBCheckPointItem(DataCacheItemKey key, InternalCacheItemVersion version, bool success)
		{
			this._key = key;
			this._version = version;
			this._success = success;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00036C07 File Offset: 0x00034E07
		public bool Success
		{
			get
			{
				return this._success;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00036C0F File Offset: 0x00034E0F
		internal DataCacheItemKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00036C17 File Offset: 0x00034E17
		internal InternalCacheItemVersion Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x04000ABF RID: 2751
		[DataMember]
		private bool _success;

		// Token: 0x04000AC0 RID: 2752
		[DataMember]
		private DataCacheItemKey _key;

		// Token: 0x04000AC1 RID: 2753
		[DataMember]
		private InternalCacheItemVersion _version;
	}
}
