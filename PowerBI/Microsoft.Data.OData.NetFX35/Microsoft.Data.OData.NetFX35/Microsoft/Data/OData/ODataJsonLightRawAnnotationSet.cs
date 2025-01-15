using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FF RID: 255
	internal sealed class ODataJsonLightRawAnnotationSet
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00017C0B File Offset: 0x00015E0B
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00017C13 File Offset: 0x00015E13
		public IDictionary<string, string> Annotations
		{
			get
			{
				return this.annotations;
			}
			set
			{
				this.annotations = value;
			}
		}

		// Token: 0x040002A2 RID: 674
		private IDictionary<string, string> annotations;
	}
}
