using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000274 RID: 628
	public abstract class ValidationRule
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000E1A RID: 3610
		internal abstract Type ValidatedType { get; }

		// Token: 0x06000E1B RID: 3611
		internal abstract void Evaluate(ValidationContext context, object item);
	}
}
