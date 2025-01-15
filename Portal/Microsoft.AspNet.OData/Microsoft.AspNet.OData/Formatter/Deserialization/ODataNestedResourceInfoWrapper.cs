using System;
using System.Collections.Generic;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BD RID: 445
	public sealed class ODataNestedResourceInfoWrapper : ODataItemBase
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x0003C180 File Offset: 0x0003A380
		public ODataNestedResourceInfoWrapper(ODataNestedResourceInfo item)
			: base(item)
		{
			this.NestedItems = new List<ODataItemBase>();
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0003C194 File Offset: 0x0003A394
		public ODataNestedResourceInfo NestedResourceInfo
		{
			get
			{
				return base.Item as ODataNestedResourceInfo;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003C1A1 File Offset: 0x0003A3A1
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x0003C1A9 File Offset: 0x0003A3A9
		public IList<ODataItemBase> NestedItems { get; private set; }
	}
}
