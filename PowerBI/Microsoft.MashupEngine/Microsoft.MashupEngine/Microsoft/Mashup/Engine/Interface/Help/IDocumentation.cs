using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface.Help
{
	// Token: 0x02000140 RID: 320
	public interface IDocumentation
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000595 RID: 1429
		string Name { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000596 RID: 1430
		string Caption { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000597 RID: 1431
		string LongDescription { get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000598 RID: 1432
		IList<DocumentationExample> Examples { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000599 RID: 1433
		string ShortDescription { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600059A RID: 1434
		string Category { get; }
	}
}
