using System;
using System.ComponentModel;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AF RID: 687
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x0004F483 File Offset: 0x0004D683
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0004F48C File Offset: 0x0004D68C
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
