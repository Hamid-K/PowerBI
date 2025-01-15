using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000112 RID: 274
	// (Invoke) Token: 0x06000742 RID: 1858
	public delegate bool TryCreateObjectInstance(IEdmStructuredValue edmValue, Type clrType, EdmToClrConverter converter, out object objectInstance, out bool objectInstanceInitialized);
}
