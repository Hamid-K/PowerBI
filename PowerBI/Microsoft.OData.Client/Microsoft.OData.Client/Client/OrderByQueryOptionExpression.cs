using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A1 RID: 161
	internal class OrderByQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00013575 File Offset: 0x00011775
		internal OrderByQueryOptionExpression(Type type, List<OrderByQueryOptionExpression.Selector> selectors)
			: base(type)
		{
			this.selectors = selectors;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00013585 File Offset: 0x00011785
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10006;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001358C File Offset: 0x0001178C
		internal List<OrderByQueryOptionExpression.Selector> Selectors
		{
			get
			{
				return this.selectors;
			}
		}

		// Token: 0x04000225 RID: 549
		private List<OrderByQueryOptionExpression.Selector> selectors;

		// Token: 0x0200017F RID: 383
		internal struct Selector
		{
			// Token: 0x06000DCE RID: 3534 RVA: 0x0002F98B File Offset: 0x0002DB8B
			internal Selector(Expression e, bool descending)
			{
				this.Expression = e;
				this.Descending = descending;
			}

			// Token: 0x04000746 RID: 1862
			internal readonly Expression Expression;

			// Token: 0x04000747 RID: 1863
			internal readonly bool Descending;
		}
	}
}
