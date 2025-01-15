using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000269 RID: 617
	public sealed class UriTemplateExpression
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004BEB9 File Offset: 0x0004A0B9
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x0004BEC1 File Offset: 0x0004A0C1
		public string LiteralText { get; internal set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0004BECA File Offset: 0x0004A0CA
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0004BED2 File Offset: 0x0004A0D2
		public IEdmTypeReference ExpectedType { get; internal set; }
	}
}
