using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028E RID: 654
	[DataContract(Name = "Decimal", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDecimalConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000234F3 File Offset: 0x000216F3
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000234FB File Offset: 0x000216FB
		[DataMember(IsRequired = true, Order = 1)]
		public decimal Value { get; set; }

		// Token: 0x060013AC RID: 5036 RVA: 0x00023504 File Offset: 0x00021704
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent(this.Value);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00023512 File Offset: 0x00021712
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0002351B File Offset: 0x0002171B
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00023524 File Offset: 0x00021724
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00023550 File Offset: 0x00021750
		public override bool Equals(QueryExpression other)
		{
			QueryDecimalConstantExpression queryDecimalConstantExpression = other as QueryDecimalConstantExpression;
			bool? flag = Util.AreEqual<QueryDecimalConstantExpression>(this, queryDecimalConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDecimalConstantExpression.Value == this.Value;
		}
	}
}
