using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A9 RID: 681
	[DataContract(Name = "ExistsExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExistsExpression : QueryExpression
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0002649C File Offset: 0x0002469C
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x000264A4 File Offset: 0x000246A4
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x060014DB RID: 5339 RVA: 0x000264AD File Offset: 0x000246AD
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("any(");
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000264CE File Offset: 0x000246CE
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x000264D7 File Offset: 0x000246D7
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000264E0 File Offset: 0x000246E0
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00026500 File Offset: 0x00024700
		public override bool Equals(QueryExpression other)
		{
			QueryExistsExpression queryExistsExpression = other as QueryExistsExpression;
			bool? flag = Util.AreEqual<QueryExistsExpression>(this, queryExistsExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryExistsExpression.Expression.Equals(this.Expression);
		}
	}
}
