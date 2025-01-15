using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200007D RID: 125
	public sealed class ODataEntityReferenceLinks : ODataAnnotatable
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000BB42 File Offset: 0x00009D42
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000BB4A File Offset: 0x00009D4A
		public long? Count { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000BB53 File Offset: 0x00009D53
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public Uri NextPageLink { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000BB64 File Offset: 0x00009D64
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public IEnumerable<ODataEntityReferenceLink> Links { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000035C8 File Offset: 0x000017C8
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
