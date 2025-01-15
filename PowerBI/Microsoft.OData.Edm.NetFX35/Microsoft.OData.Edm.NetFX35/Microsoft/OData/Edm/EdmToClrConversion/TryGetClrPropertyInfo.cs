using System;
using System.Reflection;

namespace Microsoft.OData.Edm.EdmToClrConversion
{
	// Token: 0x020000DA RID: 218
	// (Invoke) Token: 0x06000452 RID: 1106
	public delegate bool TryGetClrPropertyInfo(Type clrType, string edmName, out PropertyInfo propertyInfo);
}
