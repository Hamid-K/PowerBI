using System;

namespace Microsoft.OData.Edm.EdmToClrConversion
{
	// Token: 0x020000DB RID: 219
	// (Invoke) Token: 0x06000456 RID: 1110
	public delegate bool TryGetClrTypeName(IEdmModel edmModel, string edmTypeName, out string clrTypeName);
}
