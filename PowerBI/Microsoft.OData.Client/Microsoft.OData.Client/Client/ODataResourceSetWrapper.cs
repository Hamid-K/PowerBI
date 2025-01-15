using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003B RID: 59
	internal class ODataResourceSetWrapper : ODataItemWrapper
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000859C File Offset: 0x0000679C
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000085A4 File Offset: 0x000067A4
		public ODataResourceSet ResourceSet { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000085AD File Offset: 0x000067AD
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000085B5 File Offset: 0x000067B5
		public IEnumerable<ODataResourceWrapper> Resources { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000085BE File Offset: 0x000067BE
		public override ODataItem Item
		{
			get
			{
				return this.ResourceSet;
			}
		}
	}
}
