using System;

namespace Microsoft.OData
{
	// Token: 0x0200006F RID: 111
	public sealed class ODataDeletedResource : ODataResourceBase
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000B862 File Offset: 0x00009A62
		public ODataDeletedResource()
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000B86A File Offset: 0x00009A6A
		public ODataDeletedResource(Uri id, DeltaDeletedEntryReason reason)
		{
			if (id != null)
			{
				base.Id = id;
			}
			this.Reason = new DeltaDeletedEntryReason?(reason);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000B88E File Offset: 0x00009A8E
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000B896 File Offset: 0x00009A96
		public DeltaDeletedEntryReason? Reason { get; set; }
	}
}
