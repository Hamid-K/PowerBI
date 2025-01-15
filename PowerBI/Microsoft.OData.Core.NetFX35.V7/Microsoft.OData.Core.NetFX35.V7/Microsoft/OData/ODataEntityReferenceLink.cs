using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x02000059 RID: 89
	[DebuggerDisplay("{Url.OriginalString}")]
	public sealed class ODataEntityReferenceLink : ODataItem
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00009C9C File Offset: 0x00007E9C
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00009CA4 File Offset: 0x00007EA4
		public Uri Url { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00009CB5 File Offset: 0x00007EB5
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
