using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData.Core
{
	// Token: 0x0200016A RID: 362
	[DebuggerDisplay("{Url.OriginalString}")]
	public sealed class ODataEntityReferenceLink : ODataItem
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0003117E File Offset: 0x0002F37E
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00031186 File Offset: 0x0002F386
		public Uri Url { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003118F File Offset: 0x0002F38F
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00031197 File Offset: 0x0002F397
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
