using System;
using System.ComponentModel;

namespace Microsoft.Data.Edm
{
	// Token: 0x02000241 RID: 577
	[AttributeUsage(32767)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000D48 RID: 3400 RVA: 0x0002A2BB File Offset: 0x000284BB
		public EntityResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002A2C4 File Offset: 0x000284C4
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
