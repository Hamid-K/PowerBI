using System;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020D RID: 525
	public interface IJsonReader
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001708 RID: 5896
		object Value { get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001709 RID: 5897
		JsonNodeType NodeType { get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600170A RID: 5898
		bool IsIeee754Compatible { get; }

		// Token: 0x0600170B RID: 5899
		bool Read();
	}
}
