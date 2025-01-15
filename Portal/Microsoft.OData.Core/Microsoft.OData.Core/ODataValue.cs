using System;

namespace Microsoft.OData
{
	// Token: 0x020000EA RID: 234
	public abstract class ODataValue : ODataItem
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00002390 File Offset: 0x00000590
		internal virtual bool IsNullValue
		{
			get
			{
				return false;
			}
		}
	}
}
