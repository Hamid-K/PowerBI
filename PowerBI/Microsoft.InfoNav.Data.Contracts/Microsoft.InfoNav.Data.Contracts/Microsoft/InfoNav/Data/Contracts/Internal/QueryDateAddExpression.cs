using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200029A RID: 666
	[DataContract(Name = "DateAddExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDateAddExpression : QueryExpression
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00023C9C File Offset: 0x00021E9C
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00023CA4 File Offset: 0x00021EA4
		[DataMember(IsRequired = true, Order = 1)]
		public int Amount { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00023CAD File Offset: 0x00021EAD
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x00023CB5 File Offset: 0x00021EB5
		[DataMember(IsRequired = true, Order = 2)]
		public TimeUnit TimeUnit { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00023CBE File Offset: 0x00021EBE
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00023CC6 File Offset: 0x00021EC6
		[DataMember(IsRequired = true, Order = 3)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x06001408 RID: 5128 RVA: 0x00023CD0 File Offset: 0x00021ED0
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("dateadd(");
			w.WriteCustomerContent((long)this.Amount);
			w.Write(", ");
			w.Write(this.TimeUnit.ToString().ToLowerInvariant());
			w.Write(", ");
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00023D3E File Offset: 0x00021F3E
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00023D47 File Offset: 0x00021F47
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00023D50 File Offset: 0x00021F50
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Amount, this.TimeUnit.GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00023D94 File Offset: 0x00021F94
		public override bool Equals(QueryExpression other)
		{
			QueryDateAddExpression queryDateAddExpression = other as QueryDateAddExpression;
			bool? flag = Util.AreEqual<QueryDateAddExpression>(this, queryDateAddExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDateAddExpression.Amount.Equals(this.Amount) && queryDateAddExpression.TimeUnit.Equals(this.TimeUnit) && queryDateAddExpression.Expression.Equals(this.Expression);
		}
	}
}
