using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000297 RID: 663
	[DataContract(Name = "Decade", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDecadeConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00023B6F File Offset: 0x00021D6F
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00023B77 File Offset: 0x00021D77
		[DataMember(IsRequired = true, Order = 1)]
		public int Value { get; set; }

		// Token: 0x060013F6 RID: 5110 RVA: 0x00023B80 File Offset: 0x00021D80
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("#'");
			w.WriteCustomerContent((long)this.Value);
			w.Write('#');
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00023BA2 File Offset: 0x00021DA2
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00023BAB File Offset: 0x00021DAB
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00023BB4 File Offset: 0x00021DB4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00023BCC File Offset: 0x00021DCC
		public override bool Equals(QueryExpression other)
		{
			QueryDecadeConstantExpression queryDecadeConstantExpression = other as QueryDecadeConstantExpression;
			bool? flag = Util.AreEqual<QueryDecadeConstantExpression>(this, queryDecadeConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDecadeConstantExpression.Value == this.Value;
		}
	}
}
