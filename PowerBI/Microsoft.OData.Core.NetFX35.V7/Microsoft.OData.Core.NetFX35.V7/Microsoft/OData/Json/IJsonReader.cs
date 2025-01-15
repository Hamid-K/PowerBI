using System;

namespace Microsoft.OData.Json
{
	// Token: 0x020001EF RID: 495
	public interface IJsonReader
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600136B RID: 4971
		object Value { get; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600136C RID: 4972
		JsonNodeType NodeType { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600136D RID: 4973
		bool IsIeee754Compatible { get; }

		// Token: 0x0600136E RID: 4974
		bool Read();
	}
}
