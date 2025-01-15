using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E5 RID: 229
	public abstract class DynamicTypeWrapper
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007BA RID: 1978
		public abstract Dictionary<string, object> Values { get; }

		// Token: 0x060007BB RID: 1979 RVA: 0x0001CF68 File Offset: 0x0001B168
		public bool TryGetPropertyValue(string propertyName, out object value)
		{
			return this.Values.TryGetValue(propertyName, out value);
		}
	}
}
