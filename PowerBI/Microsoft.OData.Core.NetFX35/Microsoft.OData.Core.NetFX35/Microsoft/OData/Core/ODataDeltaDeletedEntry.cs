using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000160 RID: 352
	public sealed class ODataDeltaDeletedEntry : ODataItem
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x00030F2A File Offset: 0x0002F12A
		public ODataDeltaDeletedEntry(string id, DeltaDeletedEntryReason reason)
		{
			this.Id = id;
			this.Reason = new DeltaDeletedEntryReason?(reason);
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00030F45 File Offset: 0x0002F145
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x00030F4D File Offset: 0x0002F14D
		public string Id { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00030F56 File Offset: 0x0002F156
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x00030F5E File Offset: 0x0002F15E
		public DeltaDeletedEntryReason? Reason { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00030F67 File Offset: 0x0002F167
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x00030F6F File Offset: 0x0002F16F
		internal ODataDeltaSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaSerializationInfo.Validate(value);
			}
		}

		// Token: 0x040005AB RID: 1451
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
