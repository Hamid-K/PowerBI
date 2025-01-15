using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000742 RID: 1858
	public interface IODataComplexValueWrapper
	{
		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x0600371C RID: 14108
		string TypeName { get; }

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x0600371D RID: 14109
		IEnumerable<IODataPropertyWrapper> Properties { get; }
	}
}
