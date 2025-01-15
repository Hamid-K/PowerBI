using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000151 RID: 337
	public sealed class ODataNullValue : ODataValue
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001C4D6 File Offset: 0x0001A6D6
		internal override bool IsNullValue
		{
			get
			{
				return true;
			}
		}
	}
}
