using System;
using System.ComponentModel;

namespace System.Spatial
{
	// Token: 0x0200008C RID: 140
	[AttributeUsage(32767)]
	internal sealed class TextResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00009A70 File Offset: 0x00007C70
		public TextResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009A79 File Offset: 0x00007C79
		protected override string GetLocalizedString(string value)
		{
			return TextRes.GetString(value);
		}
	}
}
