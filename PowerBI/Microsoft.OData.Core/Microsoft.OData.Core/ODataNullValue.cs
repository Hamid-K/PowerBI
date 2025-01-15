using System;

namespace Microsoft.OData
{
	// Token: 0x020000E5 RID: 229
	public sealed class ODataNullValue : ODataValue
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00002393 File Offset: 0x00000593
		internal override bool IsNullValue
		{
			get
			{
				return true;
			}
		}
	}
}
