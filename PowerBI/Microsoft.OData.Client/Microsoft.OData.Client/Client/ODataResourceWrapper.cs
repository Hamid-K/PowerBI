using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003C RID: 60
	internal class ODataResourceWrapper : ODataItemWrapper
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000085C6 File Offset: 0x000067C6
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000085CE File Offset: 0x000067CE
		public ODataResource Resource { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000085D7 File Offset: 0x000067D7
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000085DF File Offset: 0x000067DF
		public IEnumerable<ODataNestedResourceInfoWrapper> NestedResourceInfoWrappers { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000085E8 File Offset: 0x000067E8
		public override ODataItem Item
		{
			get
			{
				return this.Resource;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000085F0 File Offset: 0x000067F0
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000085F8 File Offset: 0x000067F8
		public object Instance { get; set; }
	}
}
