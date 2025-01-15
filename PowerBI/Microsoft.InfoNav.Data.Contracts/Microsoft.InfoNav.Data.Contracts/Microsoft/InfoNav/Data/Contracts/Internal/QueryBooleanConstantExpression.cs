using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028C RID: 652
	[DataContract(Name = "Bool", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryBooleanConstantExpression : QueryConstantExpression
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x000233A4 File Offset: 0x000215A4
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x000233AC File Offset: 0x000215AC
		[DataMember(IsRequired = true, Order = 1)]
		public bool Value { get; set; }

		// Token: 0x0600139C RID: 5020 RVA: 0x000233B5 File Offset: 0x000215B5
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent(this.Value ? "true" : "false");
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000233D1 File Offset: 0x000215D1
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000233DA File Offset: 0x000215DA
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000233E4 File Offset: 0x000215E4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00023410 File Offset: 0x00021610
		public override bool Equals(QueryExpression other)
		{
			QueryBooleanConstantExpression queryBooleanConstantExpression = other as QueryBooleanConstantExpression;
			bool? flag = Util.AreEqual<QueryBooleanConstantExpression>(this, queryBooleanConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryBooleanConstantExpression.Value == this.Value;
		}
	}
}
