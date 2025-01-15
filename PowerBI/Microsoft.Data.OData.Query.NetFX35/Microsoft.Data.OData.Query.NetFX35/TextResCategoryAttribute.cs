using System;
using System.ComponentModel;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CD54 File Offset: 0x0000AF54
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
