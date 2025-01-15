using System;

namespace Microsoft.OData
{
	// Token: 0x02000079 RID: 121
	public sealed class ODataNullValue : ODataValue
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00002503 File Offset: 0x00000703
		internal override bool IsNullValue
		{
			get
			{
				return true;
			}
		}
	}
}
