using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000127 RID: 295
	public interface IEdmCollectionValue : IEdmValue, IEdmElement
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600079D RID: 1949
		IEnumerable<IEdmDelayedValue> Elements { get; }
	}
}
