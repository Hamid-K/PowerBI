using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003A RID: 58
	internal class ODataNestedResourceInfoWrapper : ODataItemWrapper
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000856A File Offset: 0x0000676A
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00008572 File Offset: 0x00006772
		public ODataNestedResourceInfo NestedResourceInfo { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000857B File Offset: 0x0000677B
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00008583 File Offset: 0x00006783
		public ODataItemWrapper NestedResourceOrResourceSet { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000858C File Offset: 0x0000678C
		public override ODataItem Item
		{
			get
			{
				return this.NestedResourceInfo;
			}
		}
	}
}
