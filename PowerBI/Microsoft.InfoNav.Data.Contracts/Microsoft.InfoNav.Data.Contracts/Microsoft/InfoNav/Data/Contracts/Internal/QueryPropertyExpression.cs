using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CB RID: 715
	[DataContract(Name = "PropertyExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public class QueryPropertyExpression : QueryExpression
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0002A89C File Offset: 0x00028A9C
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0002A8A4 File Offset: 0x00028AA4
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0002A8AD File Offset: 0x00028AAD
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0002A8B5 File Offset: 0x00028AB5
		[DataMember(IsRequired = true, Order = 2)]
		public string Property { get; set; }

		// Token: 0x060017D6 RID: 6102 RVA: 0x0002A8BE File Offset: 0x00028ABE
		internal override void WriteQueryString(QueryStringWriter w)
		{
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			else
			{
				w.Write("null");
			}
			w.Write('.');
			w.WriteIdentifierCustomerContent(this.Property);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0002A8FB File Offset: 0x00028AFB
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0002A904 File Offset: 0x00028B04
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0002A90D File Offset: 0x00028B0D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.GetHashCode<string>(this.Property, null));
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0002A938 File Offset: 0x00028B38
		public override bool Equals(QueryExpression other)
		{
			QueryPropertyExpression queryPropertyExpression = other as QueryPropertyExpression;
			bool? flag = Util.AreEqual<QueryPropertyExpression>(this, queryPropertyExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryPropertyExpression.Expression == this.Expression && queryPropertyExpression.Property == this.Property;
		}
	}
}
