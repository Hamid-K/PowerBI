using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C4 RID: 708
	[DataContract(Name = "NotExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNotExpression : QueryExpression
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0002A340 File Offset: 0x00028540
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0002A348 File Offset: 0x00028548
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x0600179C RID: 6044 RVA: 0x0002A351 File Offset: 0x00028551
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("not(");
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0002A372 File Offset: 0x00028572
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0002A37B File Offset: 0x0002857B
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0002A384 File Offset: 0x00028584
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0002A3A4 File Offset: 0x000285A4
		public override bool Equals(QueryExpression other)
		{
			QueryNotExpression queryNotExpression = other as QueryNotExpression;
			bool? flag = Util.AreEqual<QueryNotExpression>(this, queryNotExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNotExpression.Expression.Equals(this.Expression);
		}
	}
}
