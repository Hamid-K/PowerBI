using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B6 RID: 438
	public abstract class ODataItemBase
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0003B546 File Offset: 0x00039746
		protected ODataItemBase(ODataItem item)
		{
			this._item = item;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003B555 File Offset: 0x00039755
		public ODataItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0400040B RID: 1035
		private ODataItem _item;
	}
}
