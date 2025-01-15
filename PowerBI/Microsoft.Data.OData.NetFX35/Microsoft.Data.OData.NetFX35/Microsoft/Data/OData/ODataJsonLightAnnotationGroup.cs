using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x02000162 RID: 354
	[DebuggerDisplay("{Name}")]
	internal sealed class ODataJsonLightAnnotationGroup
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001DFC5 File Offset: 0x0001C1C5
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0001DFCD File Offset: 0x0001C1CD
		public IDictionary<string, object> Annotations
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

		// Token: 0x04000397 RID: 919
		private string name;

		// Token: 0x04000398 RID: 920
		private IDictionary<string, object> annotations;
	}
}
