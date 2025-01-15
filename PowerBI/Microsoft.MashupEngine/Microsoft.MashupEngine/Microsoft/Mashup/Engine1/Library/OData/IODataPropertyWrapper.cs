using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000740 RID: 1856
	public interface IODataPropertyWrapper
	{
		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003717 RID: 14103
		string Name { get; }

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003718 RID: 14104
		object Value { get; }

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003719 RID: 14105
		RecordValue Annotations { get; }
	}
}
