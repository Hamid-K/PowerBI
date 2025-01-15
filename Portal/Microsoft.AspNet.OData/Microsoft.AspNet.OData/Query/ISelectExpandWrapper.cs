using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C2 RID: 194
	public interface ISelectExpandWrapper
	{
		// Token: 0x06000682 RID: 1666
		IDictionary<string, object> ToDictionary();

		// Token: 0x06000683 RID: 1667
		IDictionary<string, object> ToDictionary(Func<IEdmModel, IEdmStructuredType, IPropertyMapper> propertyMapperProvider);
	}
}
