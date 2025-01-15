using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000019 RID: 25
	public sealed class AtomStreamReferenceMetadata : ODataAnnotatable
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000034E2 File Offset: 0x000016E2
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000034EA File Offset: 0x000016EA
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000034F3 File Offset: 0x000016F3
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000034FB File Offset: 0x000016FB
		public AtomLinkMetadata EditLink { get; set; }
	}
}
