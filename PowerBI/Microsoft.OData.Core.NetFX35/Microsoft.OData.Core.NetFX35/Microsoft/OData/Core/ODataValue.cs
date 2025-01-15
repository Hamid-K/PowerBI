using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000155 RID: 341
	public abstract class ODataValue : ODataAnnotatable
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0003056D File Offset: 0x0002E76D
		internal virtual bool IsNullValue
		{
			get
			{
				return false;
			}
		}
	}
}
