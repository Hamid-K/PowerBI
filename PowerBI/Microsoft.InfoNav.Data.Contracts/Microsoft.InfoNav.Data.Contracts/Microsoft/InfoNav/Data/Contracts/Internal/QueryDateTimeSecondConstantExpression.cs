using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000291 RID: 657
	[DataContract(Name = "DateTimeSecond", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDateTimeSecondConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00023702 File Offset: 0x00021902
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0002370A File Offset: 0x0002190A
		[DataMember(IsRequired = true, Order = 1)]
		public DateTime Value { get; set; }

		// Token: 0x060013C4 RID: 5060 RVA: 0x00023713 File Offset: 0x00021913
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteFormatCustomerContent("{0:yyyy-MM-dd HH:mm:ss}", new object[] { this.Value });
			w.Write('#');
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00023744 File Offset: 0x00021944
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0002374D File Offset: 0x0002194D
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00023758 File Offset: 0x00021958
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00023784 File Offset: 0x00021984
		public override bool Equals(QueryExpression other)
		{
			QueryDateTimeSecondConstantExpression queryDateTimeSecondConstantExpression = other as QueryDateTimeSecondConstantExpression;
			bool? flag = Util.AreEqual<QueryDateTimeSecondConstantExpression>(this, queryDateTimeSecondConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDateTimeSecondConstantExpression.Value == this.Value;
		}
	}
}
