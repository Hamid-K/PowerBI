using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x020000E5 RID: 229
	public interface IEdmCollectionValue : IEdmValue, IEdmElement
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600049F RID: 1183
		IEnumerable<IEdmDelayedValue> Elements { get; }
	}
}
