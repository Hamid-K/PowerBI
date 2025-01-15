using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200005A RID: 90
	public sealed class ODataEntityReferenceLinks : ODataAnnotatable
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00009CC6 File Offset: 0x00007EC6
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00009CCE File Offset: 0x00007ECE
		public long? Count { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00009CD7 File Offset: 0x00007ED7
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00009CDF File Offset: 0x00007EDF
		public Uri NextPageLink { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00009CE8 File Offset: 0x00007EE8
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00009CF0 File Offset: 0x00007EF0
		public IEnumerable<ODataEntityReferenceLink> Links { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00009CB5 File Offset: 0x00007EB5
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}
	}
}
