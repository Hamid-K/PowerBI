using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000170 RID: 368
	[DebuggerDisplay("{ErrorCode}: {Message}")]
	[Serializable]
	public sealed class ODataError : ODataAnnotatable
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003149D File Offset: 0x0002F69D
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x000314A5 File Offset: 0x0002F6A5
		public string ErrorCode { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000314AE File Offset: 0x0002F6AE
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x000314B6 File Offset: 0x0002F6B6
		public string Message { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000314BF File Offset: 0x0002F6BF
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x000314C7 File Offset: 0x0002F6C7
		public string Target { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000314D0 File Offset: 0x0002F6D0
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x000314D8 File Offset: 0x0002F6D8
		public ICollection<ODataErrorDetail> Details { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x000314E1 File Offset: 0x0002F6E1
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x000314E9 File Offset: 0x0002F6E9
		public ODataInnerError InnerError { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000314F2 File Offset: 0x0002F6F2
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x000314FA File Offset: 0x0002F6FA
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
