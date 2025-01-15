using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.EdmToClrConversion
{
	// Token: 0x020000D9 RID: 217
	// (Invoke) Token: 0x0600044E RID: 1102
	public delegate bool TryCreateObjectInstance(IEdmStructuredValue edmValue, Type clrType, EdmToClrConverter converter, out object objectInstance, out bool objectInstanceInitialized);
}
