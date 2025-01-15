using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018A RID: 394
	public sealed class ODataNullValue : ODataValue
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00034694 File Offset: 0x00032894
		internal override bool IsNullValue
		{
			get
			{
				return true;
			}
		}
	}
}
