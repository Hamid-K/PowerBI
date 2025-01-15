using System;
using System.ComponentModel;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CC RID: 204
	[AttributeUsage(32767)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public EntityResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000CC5D File Offset: 0x0000AE5D
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
