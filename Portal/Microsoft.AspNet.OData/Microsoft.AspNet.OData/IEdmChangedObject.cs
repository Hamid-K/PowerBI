using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200002E RID: 46
	public interface IEdmChangedObject : IEdmStructuredObject, IEdmObject
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000131 RID: 305
		EdmDeltaEntityKind DeltaKind { get; }
	}
}
