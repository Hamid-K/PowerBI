using System;
using System.ComponentModel;

namespace Microsoft.OData
{
	// Token: 0x020000DB RID: 219
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00018056 File Offset: 0x00016256
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001805F File Offset: 0x0001625F
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
