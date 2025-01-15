using System;
using System.ComponentModel;

namespace Microsoft.Spatial
{
	// Token: 0x0200008D RID: 141
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600037D RID: 893 RVA: 0x000099E0 File Offset: 0x00007BE0
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000099E9 File Offset: 0x00007BE9
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
