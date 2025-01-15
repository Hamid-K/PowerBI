using System;

namespace Microsoft.OData
{
	// Token: 0x0200004D RID: 77
	public sealed class ODataDeltaDeletedEntry : ODataItem
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public ODataDeltaDeletedEntry(string id, DeltaDeletedEntryReason reason)
		{
			this.Id = id;
			this.Reason = new DeltaDeletedEntryReason?(reason);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009AC3 File Offset: 0x00007CC3
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00009ACB File Offset: 0x00007CCB
		public string Id { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009AD4 File Offset: 0x00007CD4
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00009ADC File Offset: 0x00007CDC
		public DeltaDeletedEntryReason? Reason { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009AE5 File Offset: 0x00007CE5
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00009AED File Offset: 0x00007CED
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

		// Token: 0x04000172 RID: 370
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
