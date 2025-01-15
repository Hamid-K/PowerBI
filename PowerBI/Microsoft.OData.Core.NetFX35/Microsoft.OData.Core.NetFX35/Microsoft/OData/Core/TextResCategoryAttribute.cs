using System;
using System.ComponentModel;

namespace Microsoft.OData.Core
{
	// Token: 0x020002C5 RID: 709
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x00053279 File Offset: 0x00051479
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00053282 File Offset: 0x00051482
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
