using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000295 RID: 661
	[DataContract(Name = "YearAndWeek", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryYearAndWeekConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000239C3 File Offset: 0x00021BC3
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x000239CB File Offset: 0x00021BCB
		[DataMember(IsRequired = true, Order = 1)]
		public DateTime Value { get; set; }

		// Token: 0x060013E4 RID: 5092 RVA: 0x000239D4 File Offset: 0x00021BD4
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteFormatCustomerContent("W{0:yyyy-MM-dd}", new object[] { this.Value });
			w.Write('#');
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00023A05 File Offset: 0x00021C05
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00023A0E File Offset: 0x00021C0E
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00023A18 File Offset: 0x00021C18
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00023A44 File Offset: 0x00021C44
		public override bool Equals(QueryExpression other)
		{
			QueryYearAndWeekConstantExpression queryYearAndWeekConstantExpression = other as QueryYearAndWeekConstantExpression;
			bool? flag = Util.AreEqual<QueryYearAndWeekConstantExpression>(this, queryYearAndWeekConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryYearAndWeekConstantExpression.Value == this.Value;
		}
	}
}
