using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200009B RID: 155
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class LocalizedCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00024EFB File Offset: 0x000230FB
		internal LocalizedCategoryAttribute(string categoryId)
			: base(categoryId)
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00024F04 File Offset: 0x00023104
		protected override string GetLocalizedString(string value)
		{
			return SR.Keys.GetString(value);
		}
	}
}
