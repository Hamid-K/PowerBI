using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000285 RID: 645
	[DataContract(Name = "BetweenExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryBetweenExpression : QueryExpression
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00022FEC File Offset: 0x000211EC
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00022FF4 File Offset: 0x000211F4
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00022FFD File Offset: 0x000211FD
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00023005 File Offset: 0x00021205
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer LowerBound { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0002300E File Offset: 0x0002120E
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00023016 File Offset: 0x00021216
		[DataMember(IsRequired = true, Order = 3)]
		public QueryExpressionContainer UpperBound { get; set; }

		// Token: 0x0600137A RID: 4986 RVA: 0x0002301F File Offset: 0x0002121F
		internal override void WriteQueryString(QueryStringWriter w)
		{
			this.Expression.WriteQueryString(w);
			w.Write(" between ");
			this.LowerBound.WriteQueryString(w);
			w.Write(" and ");
			this.UpperBound.WriteQueryString(w);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0002305B File Offset: 0x0002125B
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00023064 File Offset: 0x00021264
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0002306D File Offset: 0x0002126D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode(), this.LowerBound.GetHashCode(), this.UpperBound.GetHashCode());
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000230A0 File Offset: 0x000212A0
		public override bool Equals(QueryExpression other)
		{
			QueryBetweenExpression queryBetweenExpression = other as QueryBetweenExpression;
			bool? flag = Util.AreEqual<QueryBetweenExpression>(this, queryBetweenExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryBetweenExpression.Expression.Equals(this.Expression) && queryBetweenExpression.LowerBound.Equals(this.LowerBound) && queryBetweenExpression.UpperBound.Equals(this.UpperBound);
		}
	}
}
