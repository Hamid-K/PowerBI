using System;
using System.ComponentModel;

namespace Microsoft.Spatial
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00006900 File Offset: 0x00004B00
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006909 File Offset: 0x00004B09
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
