using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000294 RID: 660
	[DataContract(Name = "Year", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryYearConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00023926 File Offset: 0x00021B26
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x0002392E File Offset: 0x00021B2E
		[DataMember(IsRequired = true, Order = 1)]
		public int Value { get; set; }

		// Token: 0x060013DC RID: 5084 RVA: 0x00023937 File Offset: 0x00021B37
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteCustomerContent((long)this.Value);
			w.Write('#');
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00023956 File Offset: 0x00021B56
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0002395F File Offset: 0x00021B5F
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00023968 File Offset: 0x00021B68
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00023980 File Offset: 0x00021B80
		public override bool Equals(QueryExpression other)
		{
			QueryYearConstantExpression queryYearConstantExpression = other as QueryYearConstantExpression;
			bool? flag = Util.AreEqual<QueryYearConstantExpression>(this, queryYearConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryYearConstantExpression.Value == this.Value;
		}
	}
}
