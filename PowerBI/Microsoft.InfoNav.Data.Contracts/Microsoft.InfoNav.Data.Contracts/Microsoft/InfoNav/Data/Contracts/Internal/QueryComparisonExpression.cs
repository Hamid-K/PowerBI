using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000288 RID: 648
	[DataContract(Name = "ComparisonExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryComparisonExpression : QueryBinaryExpression
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x000231D8 File Offset: 0x000213D8
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x000231E0 File Offset: 0x000213E0
		[DataMember(IsRequired = true, Order = 10)]
		public QueryComparisonKind ComparisonKind { get; set; }

		// Token: 0x0600138D RID: 5005 RVA: 0x000231EC File Offset: 0x000213EC
		internal override void WriteQueryString(QueryStringWriter w)
		{
			base.Left.WriteQueryString(w);
			w.Write(' ');
			switch (this.ComparisonKind)
			{
			case QueryComparisonKind.Equal:
				w.Write('=');
				break;
			case QueryComparisonKind.GreaterThan:
				w.Write('>');
				break;
			case QueryComparisonKind.GreaterThanOrEqual:
				w.Write(">=");
				break;
			case QueryComparisonKind.LessThan:
				w.Write('<');
				break;
			case QueryComparisonKind.LessThanOrEqual:
				w.Write("<=");
				break;
			default:
				w.Write("unk");
				break;
			}
			w.Write(' ');
			base.Right.WriteQueryString(w);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00023287 File Offset: 0x00021487
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00023290 File Offset: 0x00021490
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0002329C File Offset: 0x0002149C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.ComparisonKind.GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000232D4 File Offset: 0x000214D4
		public override bool Equals(QueryExpression other)
		{
			QueryComparisonExpression queryComparisonExpression = other as QueryComparisonExpression;
			bool? flag = Util.AreEqual<QueryComparisonExpression>(this, queryComparisonExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryComparisonExpression.ComparisonKind.Equals(this.ComparisonKind) && base.Equals(other);
		}
	}
}
