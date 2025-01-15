using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010B RID: 267
	// (Invoke) Token: 0x0600076F RID: 1903
	public delegate bool TryCreateObjectInstance(IEdmStructuredValue edmValue, Type clrType, EdmToClrConverter converter, out object objectInstance, out bool objectInstanceInitialized);
}
