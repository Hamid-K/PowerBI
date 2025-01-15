using System;
using System.ComponentModel;

namespace Microsoft.Data
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0000AA15 File Offset: 0x00008C15
		public ResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000AA1E File Offset: 0x00008C1E
		protected override string GetLocalizedString(string value)
		{
			return StringsHelper.GetString(value, Array.Empty<object>());
		}
	}
}
