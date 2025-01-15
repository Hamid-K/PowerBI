using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B2 RID: 434
	public sealed class UriTemplateExpression
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0003BB4C File Offset: 0x00039D4C
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0003BB54 File Offset: 0x00039D54
		public string LiteralText { get; internal set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0003BB5D File Offset: 0x00039D5D
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x0003BB65 File Offset: 0x00039D65
		public IEdmTypeReference ExpectedType { get; internal set; }
	}
}
