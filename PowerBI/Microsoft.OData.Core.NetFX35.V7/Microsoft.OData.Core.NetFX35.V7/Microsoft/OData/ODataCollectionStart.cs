using System;

namespace Microsoft.OData
{
	// Token: 0x02000042 RID: 66
	public sealed class ODataCollectionStart : ODataAnnotatable
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008934 File Offset: 0x00006B34
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000893C File Offset: 0x00006B3C
		public string Name { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008945 File Offset: 0x00006B45
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000894D File Offset: 0x00006B4D
		public long? Count { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008956 File Offset: 0x00006B56
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000895E File Offset: 0x00006B5E
		public Uri NextPageLink { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008967 File Offset: 0x00006B67
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000896F File Offset: 0x00006B6F
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

		// Token: 0x04000122 RID: 290
		private ODataCollectionStartSerializationInfo serializationInfo;
	}
}
