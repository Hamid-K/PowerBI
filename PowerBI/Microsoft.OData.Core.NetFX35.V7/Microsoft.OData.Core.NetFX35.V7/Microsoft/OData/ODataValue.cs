using System;

namespace Microsoft.OData
{
	// Token: 0x020000A0 RID: 160
	public abstract class ODataValue : ODataItem
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00002500 File Offset: 0x00000700
		internal virtual bool IsNullValue
		{
			get
			{
				return false;
			}
		}
	}
}
