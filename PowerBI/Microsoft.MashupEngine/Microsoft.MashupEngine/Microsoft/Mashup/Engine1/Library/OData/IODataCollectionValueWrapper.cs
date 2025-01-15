using System;
using System.Collections;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000743 RID: 1859
	public interface IODataCollectionValueWrapper
	{
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x0600371E RID: 14110
		string TypeName { get; }

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x0600371F RID: 14111
		IEnumerable Items { get; }
	}
}
