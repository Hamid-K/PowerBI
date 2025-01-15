using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000293 RID: 659
	[DataContract(Name = "Number", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNumberConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00023892 File Offset: 0x00021A92
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0002389A File Offset: 0x00021A9A
		[DataMember(IsRequired = true, Order = 1)]
		public string Value { get; set; }

		// Token: 0x060013D4 RID: 5076 RVA: 0x000238A3 File Offset: 0x00021AA3
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent(this.Value);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000238B1 File Offset: 0x00021AB1
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000238BA File Offset: 0x00021ABA
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000238C3 File Offset: 0x00021AC3
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000238E0 File Offset: 0x00021AE0
		public override bool Equals(QueryExpression other)
		{
			QueryNumberConstantExpression queryNumberConstantExpression = other as QueryNumberConstantExpression;
			bool? flag = Util.AreEqual<QueryNumberConstantExpression>(this, queryNumberConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNumberConstantExpression.Value == this.Value;
		}
	}
}
