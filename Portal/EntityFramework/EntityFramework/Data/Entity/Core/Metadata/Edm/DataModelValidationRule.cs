using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000498 RID: 1176
	internal abstract class DataModelValidationRule
	{
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06003A03 RID: 14851
		internal abstract Type ValidatedType { get; }

		// Token: 0x06003A04 RID: 14852
		internal abstract void Evaluate(EdmModelValidationContext context, MetadataItem item);
	}
}
