using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000040 RID: 64
	public sealed class WritingNestedResourceInfoArgs
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000087FC File Offset: 0x000069FC
		public WritingNestedResourceInfoArgs(ODataNestedResourceInfo link, object source, object target)
		{
			Util.CheckArgumentNull<ODataNestedResourceInfo>(link, "link");
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<object>(target, "target");
			this.Link = link;
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008848 File Offset: 0x00006A48
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00008850 File Offset: 0x00006A50
		public ODataNestedResourceInfo Link { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008859 File Offset: 0x00006A59
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00008861 File Offset: 0x00006A61
		public object Source { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000886A File Offset: 0x00006A6A
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008872 File Offset: 0x00006A72
		public object Target { get; private set; }
	}
}
