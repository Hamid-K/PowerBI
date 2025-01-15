using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028D RID: 653
	[DataContract(Name = "Int", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryIntegerConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00023453 File Offset: 0x00021653
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0002345B File Offset: 0x0002165B
		[DataMember(IsRequired = true, Order = 1)]
		public long Value { get; set; }

		// Token: 0x060013A4 RID: 5028 RVA: 0x00023464 File Offset: 0x00021664
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent(this.Value);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00023472 File Offset: 0x00021672
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0002347B File Offset: 0x0002167B
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00023484 File Offset: 0x00021684
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000234B0 File Offset: 0x000216B0
		public override bool Equals(QueryExpression other)
		{
			QueryIntegerConstantExpression queryIntegerConstantExpression = other as QueryIntegerConstantExpression;
			bool? flag = Util.AreEqual<QueryIntegerConstantExpression>(this, queryIntegerConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryIntegerConstantExpression.Value == this.Value;
		}
	}
}
