using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AE RID: 1198
	internal class EdmModelValidationRule<TItem> : DataModelValidationRule<TItem> where TItem : class
	{
		// Token: 0x06003AD8 RID: 15064 RVA: 0x000C2866 File Offset: 0x000C0A66
		internal EdmModelValidationRule(Action<EdmModelValidationContext, TItem> validate)
			: base(validate)
		{
		}
	}
}
