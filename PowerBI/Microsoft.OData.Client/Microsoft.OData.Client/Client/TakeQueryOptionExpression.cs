using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A7 RID: 167
	[DebuggerDisplay("TakeQueryOptionExpression {TakeAmount}")]
	internal class TakeQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00015172 File Offset: 0x00013372
		internal TakeQueryOptionExpression(Type type, ConstantExpression takeAmount)
			: base(type)
		{
			this.takeAmount = takeAmount;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00015182 File Offset: 0x00013382
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10004;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00015189 File Offset: 0x00013389
		internal ConstantExpression TakeAmount
		{
			get
			{
				return this.takeAmount;
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015194 File Offset: 0x00013394
		internal override QueryOptionExpression ComposeMultipleSpecification(QueryOptionExpression previous)
		{
			int num = (int)this.takeAmount.Value;
			int num2 = (int)((TakeQueryOptionExpression)previous).takeAmount.Value;
			if (num >= num2)
			{
				return previous;
			}
			return this;
		}

		// Token: 0x0400023A RID: 570
		private ConstantExpression takeAmount;
	}
}
