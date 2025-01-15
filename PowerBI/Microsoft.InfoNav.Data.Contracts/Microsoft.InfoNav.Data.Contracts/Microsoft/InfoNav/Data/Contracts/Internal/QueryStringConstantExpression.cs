using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028F RID: 655
	[DataContract(Name = "String", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryStringConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00023596 File Offset: 0x00021796
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x0002359E File Offset: 0x0002179E
		[DataMember(IsRequired = true, Order = 1)]
		public string Value { get; set; }

		// Token: 0x060013B4 RID: 5044 RVA: 0x000235A7 File Offset: 0x000217A7
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('"');
			w.WriteCustomerContent(this.Value);
			w.Write('"');
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000235C5 File Offset: 0x000217C5
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000235CE File Offset: 0x000217CE
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000235D7 File Offset: 0x000217D7
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000235F4 File Offset: 0x000217F4
		public override bool Equals(QueryExpression other)
		{
			QueryStringConstantExpression queryStringConstantExpression = other as QueryStringConstantExpression;
			bool? flag = Util.AreEqual<QueryStringConstantExpression>(this, queryStringConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryStringConstantExpression.Value == this.Value;
		}
	}
}
