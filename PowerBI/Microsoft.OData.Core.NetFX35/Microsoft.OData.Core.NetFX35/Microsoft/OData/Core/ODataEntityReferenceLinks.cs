using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x0200016B RID: 363
	public sealed class ODataEntityReferenceLinks : ODataAnnotatable
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x000311A8 File Offset: 0x0002F3A8
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x000311B0 File Offset: 0x0002F3B0
		public long? Count { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x000311B9 File Offset: 0x0002F3B9
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x000311C1 File Offset: 0x0002F3C1
		public Uri NextPageLink { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000311CA File Offset: 0x0002F3CA
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x000311D2 File Offset: 0x0002F3D2
		public IEnumerable<ODataEntityReferenceLink> Links { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000311DB File Offset: 0x0002F3DB
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x000311E3 File Offset: 0x0002F3E3
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
