using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003E RID: 62
	public sealed class WritingEntityReferenceLinkArgs
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000872C File Offset: 0x0000692C
		public WritingEntityReferenceLinkArgs(ODataEntityReferenceLink entityReferenceLink, object source, object target)
		{
			Util.CheckArgumentNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<object>(target, "target");
			this.EntityReferenceLink = entityReferenceLink;
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008778 File Offset: 0x00006978
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008780 File Offset: 0x00006980
		public ODataEntityReferenceLink EntityReferenceLink { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008789 File Offset: 0x00006989
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008791 File Offset: 0x00006991
		public object Source { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000879A File Offset: 0x0000699A
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000087A2 File Offset: 0x000069A2
		public object Target { get; private set; }
	}
}
