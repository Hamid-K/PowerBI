using System;

namespace Microsoft.OData
{
	// Token: 0x02000065 RID: 101
	public sealed class ODataCollectionStart : ODataAnnotatable
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000A59A File Offset: 0x0000879A
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000A5A2 File Offset: 0x000087A2
		public string Name { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000A5AB File Offset: 0x000087AB
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000A5B3 File Offset: 0x000087B3
		public long? Count { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000A5BC File Offset: 0x000087BC
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public Uri NextPageLink { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000A5CD File Offset: 0x000087CD
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000A5D5 File Offset: 0x000087D5
		internal ODataCollectionStartSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataCollectionStartSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000180 RID: 384
		private ODataCollectionStartSerializationInfo serializationInfo;
	}
}
