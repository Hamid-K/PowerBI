using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000030 RID: 48
	public interface IEdmDeltaDeletedEntityObject : IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000132 RID: 306
		// (set) Token: 0x06000133 RID: 307
		string Id { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000134 RID: 308
		// (set) Token: 0x06000135 RID: 309
		DeltaDeletedEntryReason Reason { get; set; }
	}
}
