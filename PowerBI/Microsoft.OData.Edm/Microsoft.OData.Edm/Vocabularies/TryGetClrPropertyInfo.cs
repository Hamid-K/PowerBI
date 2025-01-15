using System;
using System.Reflection;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010C RID: 268
	// (Invoke) Token: 0x06000773 RID: 1907
	public delegate bool TryGetClrPropertyInfo(Type clrType, string edmName, out PropertyInfo propertyInfo);
}
