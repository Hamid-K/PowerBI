using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200029D RID: 669
	[DataContract(Name = "DateSpanExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDateSpanExpression : QueryExpression
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00023F39 File Offset: 0x00022139
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x00023F41 File Offset: 0x00022141
		[DataMember(IsRequired = true, Order = 1)]
		public TimeUnit TimeUnit { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00023F4A File Offset: 0x0002214A
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x00023F52 File Offset: 0x00022152
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x0600141C RID: 5148 RVA: 0x00023F5C File Offset: 0x0002215C
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("datespan(");
			w.Write(this.TimeUnit.ToString().ToLowerInvariant());
			w.Write(", ");
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00023FB2 File Offset: 0x000221B2
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00023FBB File Offset: 0x000221BB
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00023FC4 File Offset: 0x000221C4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.TimeUnit.GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00024000 File Offset: 0x00022200
		public override bool Equals(QueryExpression other)
		{
			QueryDateSpanExpression queryDateSpanExpression = other as QueryDateSpanExpression;
			bool? flag = Util.AreEqual<QueryDateSpanExpression>(this, queryDateSpanExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDateSpanExpression.TimeUnit.Equals(this.TimeUnit) && queryDateSpanExpression.Expression.Equals(this.Expression);
		}
	}
}
