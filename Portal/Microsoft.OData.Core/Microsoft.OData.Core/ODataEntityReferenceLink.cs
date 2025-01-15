using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x0200007C RID: 124
	[DebuggerDisplay("{Url.OriginalString}")]
	public sealed class ODataEntityReferenceLink : ODataItem
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000BB31 File Offset: 0x00009D31
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000BB39 File Offset: 0x00009D39
		public Uri Url { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000035C8 File Offset: 0x000017C8
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
