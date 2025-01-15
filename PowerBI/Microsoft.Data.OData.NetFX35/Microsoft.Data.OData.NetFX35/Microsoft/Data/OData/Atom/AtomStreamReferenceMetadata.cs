using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000226 RID: 550
	public sealed class AtomStreamReferenceMetadata : ODataAnnotatable
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0003DEE7 File Offset: 0x0003C0E7
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0003DEEF File Offset: 0x0003C0EF
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0003DF00 File Offset: 0x0003C100
		public AtomLinkMetadata EditLink { get; set; }
	}
}
