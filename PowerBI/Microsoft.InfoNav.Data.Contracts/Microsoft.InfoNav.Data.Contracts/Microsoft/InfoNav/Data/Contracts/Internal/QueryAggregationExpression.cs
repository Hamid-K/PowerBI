using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200027B RID: 635
	[DataContract(Name = "AggregationExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryAggregationExpression : QueryExpression
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0002290C File Offset: 0x00020B0C
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00022914 File Offset: 0x00020B14
		[DataMember(IsRequired = true, Order = 1)]
		public QueryAggregateFunction Function { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0002291D File Offset: 0x00020B1D
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00022925 File Offset: 0x00020B25
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x0600133A RID: 4922 RVA: 0x00022930 File Offset: 0x00020B30
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write(this.Function.ToString().ToLowerInvariant());
			w.Write('(');
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00022978 File Offset: 0x00020B78
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00022981 File Offset: 0x00020B81
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0002298A File Offset: 0x00020B8A
		internal bool CanReturnStrings()
		{
			return this.Function == QueryAggregateFunction.Min || this.Function == QueryAggregateFunction.Max || this.Function == QueryAggregateFunction.SingleValue;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000229AC File Offset: 0x00020BAC
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Function.GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000229E8 File Offset: 0x00020BE8
		public override bool Equals(QueryExpression other)
		{
			QueryAggregationExpression queryAggregationExpression = other as QueryAggregationExpression;
			bool? flag = Util.AreEqual<QueryAggregationExpression>(this, queryAggregationExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryAggregationExpression.Function.Equals(this.Function) && queryAggregationExpression.Expression.Equals(this.Expression);
		}
	}
}
