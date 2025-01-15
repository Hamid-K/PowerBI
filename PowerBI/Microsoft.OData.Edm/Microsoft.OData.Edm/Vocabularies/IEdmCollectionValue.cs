using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000110 RID: 272
	public interface IEdmCollectionValue : IEdmValue, IEdmElement
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600077C RID: 1916
		IEnumerable<IEdmDelayedValue> Elements { get; }
	}
}
