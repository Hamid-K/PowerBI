using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C7 RID: 455
	internal sealed class QueryTypeSafeFloorExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x0003DF71 File Offset: 0x0003C171
		internal QueryTypeSafeFloorExpression(ConceptualResultType conceptualResultType, QueryExpression expression, double size, TimeUnit? timeUnit)
			: base(conceptualResultType)
		{
			this._expression = expression;
			this._size = size;
			this._timeUnit = timeUnit;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0003DF90 File Offset: 0x0003C190
		public QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0003DF98 File Offset: 0x0003C198
		public double Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0003DFA0 File Offset: 0x0003C1A0
		public TimeUnit? TimeUnit
		{
			get
			{
				return this._timeUnit;
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0003DFB4 File Offset: 0x0003C1B4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTypeSafeFloorExpression queryTypeSafeFloorExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTypeSafeFloorExpression>(this, other, out flag, out queryTypeSafeFloorExpression))
			{
				return flag;
			}
			if (this.Expression.Equals(queryTypeSafeFloorExpression.Expression) && this.Size.Equals(queryTypeSafeFloorExpression.Size))
			{
				TimeUnit? timeUnit = this.TimeUnit;
				TimeUnit? timeUnit2 = queryTypeSafeFloorExpression.TimeUnit;
				return (timeUnit.GetValueOrDefault() == timeUnit2.GetValueOrDefault()) & (timeUnit != null == (timeUnit2 != null));
			}
			return false;
		}

		// Token: 0x04000BF4 RID: 3060
		private readonly QueryExpression _expression;

		// Token: 0x04000BF5 RID: 3061
		private readonly double _size;

		// Token: 0x04000BF6 RID: 3062
		private readonly TimeUnit? _timeUnit;
	}
}
