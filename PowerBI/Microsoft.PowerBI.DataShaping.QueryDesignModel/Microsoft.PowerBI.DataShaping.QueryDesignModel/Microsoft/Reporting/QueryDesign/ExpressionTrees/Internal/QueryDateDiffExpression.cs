using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000173 RID: 371
	internal sealed class QueryDateDiffExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x0003AF9A File Offset: 0x0003919A
		internal QueryDateDiffExpression(ConceptualResultType conceptualResultType, QueryExpression startDate, QueryExpression endDate, TimeUnit timeUnit)
			: base(conceptualResultType)
		{
			this._startDate = startDate;
			this._endDate = endDate;
			this._timeUnit = timeUnit;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0003AFB9 File Offset: 0x000391B9
		public QueryExpression StartDate
		{
			get
			{
				return this._startDate;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0003AFC1 File Offset: 0x000391C1
		public QueryExpression EndDate
		{
			get
			{
				return this._endDate;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003AFC9 File Offset: 0x000391C9
		public TimeUnit TimeUnit
		{
			get
			{
				return this._timeUnit;
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0003AFD1 File Offset: 0x000391D1
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0003AFDC File Offset: 0x000391DC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryDateDiffExpression queryDateDiffExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryDateDiffExpression>(this, other, out flag, out queryDateDiffExpression))
			{
				return flag;
			}
			return this.StartDate.Equals(queryDateDiffExpression.StartDate) && this.EndDate.Equals(queryDateDiffExpression.EndDate) && this.TimeUnit == queryDateDiffExpression.TimeUnit;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0003B030 File Offset: 0x00039230
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.StartDate.GetHashCode(), this.EndDate.GetHashCode(), this.TimeUnit.GetHashCode());
		}

		// Token: 0x04000B30 RID: 2864
		private readonly QueryExpression _startDate;

		// Token: 0x04000B31 RID: 2865
		private readonly QueryExpression _endDate;

		// Token: 0x04000B32 RID: 2866
		private readonly TimeUnit _timeUnit;
	}
}
