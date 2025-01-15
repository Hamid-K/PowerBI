using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000147 RID: 327
	public abstract class ValidationRule
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000842 RID: 2114
		internal abstract Type ValidatedType { get; }

		// Token: 0x06000843 RID: 2115
		internal abstract void Evaluate(ValidationContext context, object item);
	}
}
