using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x0200005E RID: 94
	[DebuggerDisplay("{ErrorCode}: {Message}")]
	[Serializable]
	public sealed class ODataError : ODataAnnotatable
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00009F58 File Offset: 0x00008158
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00009F60 File Offset: 0x00008160
		public string ErrorCode { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00009F69 File Offset: 0x00008169
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00009F71 File Offset: 0x00008171
		public string Message { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00009F7A File Offset: 0x0000817A
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00009F82 File Offset: 0x00008182
		public string Target { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009F8B File Offset: 0x0000818B
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00009F93 File Offset: 0x00008193
		public ICollection<ODataErrorDetail> Details { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009F9C File Offset: 0x0000819C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00009FA4 File Offset: 0x000081A4
		public ODataInnerError InnerError { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00009CB5 File Offset: 0x00007EB5
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We want to allow the same instance annotation collection instance to be shared across ODataLib OM instances.")]
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
