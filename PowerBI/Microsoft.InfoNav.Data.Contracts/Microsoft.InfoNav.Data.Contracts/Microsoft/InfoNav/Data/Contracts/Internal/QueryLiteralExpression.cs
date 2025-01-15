using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BB RID: 699
	[DataContract(Name = "LiteralExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryLiteralExpression : QueryExpression
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00029C03 File Offset: 0x00027E03
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00029C0B File Offset: 0x00027E0B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Value { get; set; }

		// Token: 0x06001753 RID: 5971 RVA: 0x00029C14 File Offset: 0x00027E14
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent(this.Value ?? "null");
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00029C2C File Offset: 0x00027E2C
		public override bool Equals(QueryExpression other)
		{
			QueryLiteralExpression queryLiteralExpression = other as QueryLiteralExpression;
			bool? flag = Util.AreEqual<QueryLiteralExpression>(this, queryLiteralExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryLiteralExpression.Value == this.Value;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00029C6A File Offset: 0x00027E6A
		public override int GetHashCode()
		{
			if (this.Value != null)
			{
				return this.Value.GetHashCode();
			}
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00029C8B File Offset: 0x00027E8B
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00029C94 File Offset: 0x00027E94
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
