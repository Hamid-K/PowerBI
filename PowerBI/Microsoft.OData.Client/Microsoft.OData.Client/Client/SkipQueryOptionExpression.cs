using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A6 RID: 166
	[DebuggerDisplay("SkipQueryOptionExpression {SkipAmount}")]
	internal class SkipQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x000150FD File Offset: 0x000132FD
		internal SkipQueryOptionExpression(Type type, ConstantExpression skipAmount)
			: base(type)
		{
			this.skipAmount = skipAmount;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001510D File Offset: 0x0001330D
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10005;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00015114 File Offset: 0x00013314
		internal ConstantExpression SkipAmount
		{
			get
			{
				return this.skipAmount;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001511C File Offset: 0x0001331C
		internal override QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			int num = (int)this.skipAmount.Value;
			int num2 = (int)((SkipQueryOptionExpression)previous).skipAmount.Value;
			return new SkipQueryOptionExpression(this.Type, Expression.Constant(num + num2, typeof(int)));
		}

		// Token: 0x04000239 RID: 569
		private ConstantExpression skipAmount;
	}
}
