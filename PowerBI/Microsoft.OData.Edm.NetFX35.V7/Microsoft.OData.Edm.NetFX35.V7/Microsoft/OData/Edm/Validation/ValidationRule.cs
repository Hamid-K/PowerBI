using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D8 RID: 216
	public abstract class ValidationRule
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000643 RID: 1603
		internal abstract Type ValidatedType { get; }

		// Token: 0x06000644 RID: 1604
		internal abstract void Evaluate(ValidationContext context, object item);
	}
}
