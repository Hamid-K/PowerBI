using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E7 RID: 743
	[DataContract]
	internal class LocalCacheItem
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00053DD8 File Offset: 0x00051FD8
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x00053DE0 File Offset: 0x00051FE0
		[DataMember]
		public byte[][] Value { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00053DE9 File Offset: 0x00051FE9
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00053DF1 File Offset: 0x00051FF1
		[DataMember]
		public InternalCacheItemVersion Version { get; private set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00053DFA File Offset: 0x00051FFA
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00053E02 File Offset: 0x00052002
		[DataMember]
		public TimeSpan Ttl { get; private set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00053E0B File Offset: 0x0005200B
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00053E13 File Offset: 0x00052013
		[DataMember]
		public string Key { get; private set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00053E1C File Offset: 0x0005201C
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x00053E24 File Offset: 0x00052024
		public object DeserializedValue { get; set; }

		// Token: 0x06001BCA RID: 7114 RVA: 0x00053E2D File Offset: 0x0005202D
		public LocalCacheItem(string key, byte[][] value, InternalCacheItemVersion version, TimeSpan ttl)
		{
			this.Value = value;
			this.Version = version;
			this.Ttl = ttl;
			this.Key = key;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00053E52 File Offset: 0x00052052
		internal LocalCacheItem(string key, object deserializedValue, InternalCacheItemVersion version)
		{
			this.Key = key;
			this.DeserializedValue = deserializedValue;
			this.Version = version;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00053E6F File Offset: 0x0005206F
		public LocalCacheItem(string key)
		{
			this.Key = key;
		}
	}
}
