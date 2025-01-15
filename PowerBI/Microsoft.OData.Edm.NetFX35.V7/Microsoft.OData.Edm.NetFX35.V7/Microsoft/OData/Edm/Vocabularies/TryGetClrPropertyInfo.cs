using System;
using System.Reflection;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000113 RID: 275
	// (Invoke) Token: 0x06000746 RID: 1862
	public delegate bool TryGetClrPropertyInfo(Type clrType, string edmName, out PropertyInfo propertyInfo);
}
