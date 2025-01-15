using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000032 RID: 50
	public interface IEdmDeltaLinkBase : IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000136 RID: 310
		// (set) Token: 0x06000137 RID: 311
		Uri Source { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000138 RID: 312
		// (set) Token: 0x06000139 RID: 313
		Uri Target { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013A RID: 314
		// (set) Token: 0x0600013B RID: 315
		string Relationship { get; set; }
	}
}
