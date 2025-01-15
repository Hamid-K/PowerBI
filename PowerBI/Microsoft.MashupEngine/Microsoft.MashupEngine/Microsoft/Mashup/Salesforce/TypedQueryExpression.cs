using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001F1 RID: 497
	internal struct TypedQueryExpression
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x000159E4 File Offset: 0x00013BE4
		public TypedQueryExpression(TypeValue type, QueryExpression expression)
		{
			this.type = type;
			this.expression = expression;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000159F4 File Offset: 0x00013BF4
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000159FC File Offset: 0x00013BFC
		public QueryExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040005F8 RID: 1528
		private readonly TypeValue type;

		// Token: 0x040005F9 RID: 1529
		private readonly QueryExpression expression;
	}
}
