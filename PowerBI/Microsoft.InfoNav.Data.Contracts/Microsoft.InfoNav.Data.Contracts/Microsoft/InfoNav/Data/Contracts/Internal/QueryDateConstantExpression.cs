using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000292 RID: 658
	[DataContract(Name = "Date", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDateConstantExpression : QueryConstantExpression
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x000237CA File Offset: 0x000219CA
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x000237D2 File Offset: 0x000219D2
		[DataMember(IsRequired = true, Order = 1)]
		public DateTime Value { get; set; }

		// Token: 0x060013CC RID: 5068 RVA: 0x000237DB File Offset: 0x000219DB
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('#');
			w.WriteFormatCustomerContent("{0:yyyy-MM-dd}", new object[] { this.Value });
			w.Write('#');
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0002380C File Offset: 0x00021A0C
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00023815 File Offset: 0x00021A15
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00023820 File Offset: 0x00021A20
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Value.GetHashCode());
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0002384C File Offset: 0x00021A4C
		public override bool Equals(QueryExpression other)
		{
			QueryDateConstantExpression queryDateConstantExpression = other as QueryDateConstantExpression;
			bool? flag = Util.AreEqual<QueryDateConstantExpression>(this, queryDateConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDateConstantExpression.Value == this.Value;
		}
	}
}
