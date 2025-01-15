using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000296 RID: 662
	[DataContract(Name = "YearAndMonth", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryYearAndMonthConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00023A8A File Offset: 0x00021C8A
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00023A92 File Offset: 0x00021C92
		[DataMember(IsRequired = true, Order = 1)]
		public int Year { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00023A9B File Offset: 0x00021C9B
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00023AA3 File Offset: 0x00021CA3
		[DataMember(IsRequired = true, Order = 2)]
		public int Month { get; set; }

		// Token: 0x060013EE RID: 5102 RVA: 0x00023AAC File Offset: 0x00021CAC
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteFormatCustomerContent("{0}-{1:00}", new object[] { this.Year, this.Month });
			w.Write('#');
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00023AEB File Offset: 0x00021CEB
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00023AF4 File Offset: 0x00021CF4
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00023AFD File Offset: 0x00021CFD
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Year, this.Month);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00023B1C File Offset: 0x00021D1C
		public override bool Equals(QueryExpression other)
		{
			QueryYearAndMonthConstantExpression queryYearAndMonthConstantExpression = other as QueryYearAndMonthConstantExpression;
			bool? flag = Util.AreEqual<QueryYearAndMonthConstantExpression>(this, queryYearAndMonthConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryYearAndMonthConstantExpression.Year == this.Year && queryYearAndMonthConstantExpression.Month == this.Month;
		}
	}
}
