using System;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A2 RID: 162
	internal abstract class QueryOptionExpression : Expression
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00013594 File Offset: 0x00011794
		internal QueryOptionExpression(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000135A3 File Offset: 0x000117A3
		public override Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00002DF3 File Offset: 0x00000FF3
		internal virtual QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			return this;
		}

		// Token: 0x04000226 RID: 550
		private Type type;
	}
}
