using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000290 RID: 656
	[DataContract(Name = "DateTime", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDateTimeConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0002363A File Offset: 0x0002183A
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x00023642 File Offset: 0x00021842
		[DataMember(IsRequired = true, Order = 1)]
		public DateTime Value { get; set; }

		// Token: 0x060013BC RID: 5052 RVA: 0x0002364B File Offset: 0x0002184B
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteFormatCustomerContent("{0:yyyy-MM-ddTHH:mm:ss.FFFFFFF}", new object[] { this.Value });
			w.Write('#');
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0002367C File Offset: 0x0002187C
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00023685 File Offset: 0x00021885
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00023690 File Offset: 0x00021890
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000236BC File Offset: 0x000218BC
		public override bool Equals(QueryExpression other)
		{
			QueryDateTimeConstantExpression queryDateTimeConstantExpression = other as QueryDateTimeConstantExpression;
			bool? flag = Util.AreEqual<QueryDateTimeConstantExpression>(this, queryDateTimeConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDateTimeConstantExpression.Value == this.Value;
		}
	}
}
