using System;
using System.ComponentModel;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200027B RID: 635
	[AttributeUsage(32767)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x0002B98C File Offset: 0x00029B8C
		public EntityResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002B995 File Offset: 0x00029B95
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
