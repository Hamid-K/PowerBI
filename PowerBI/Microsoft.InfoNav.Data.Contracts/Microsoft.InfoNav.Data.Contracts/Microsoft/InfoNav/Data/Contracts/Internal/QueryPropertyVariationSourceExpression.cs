using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CC RID: 716
	public sealed class QueryPropertyVariationSourceExpression : QueryExpression
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0002A993 File Offset: 0x00028B93
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0002A99B File Offset: 0x00028B9B
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0002A9AC File Offset: 0x00028BAC
		[DataMember(IsRequired = true, Order = 2)]
		public string Property { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0002A9B5 File Offset: 0x00028BB5
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0002A9BD File Offset: 0x00028BBD
		[DataMember(IsRequired = true, Order = 2)]
		public string Name { get; set; }

		// Token: 0x060017E2 RID: 6114 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		internal override void WriteQueryString(QueryStringWriter w)
		{
			this.Expression.WriteQueryString(w);
			w.Write(".variation(");
			w.WriteIdentifierCustomerContent(this.Property);
			w.Write(", ");
			w.WriteIdentifierCustomerContent(this.Name);
			w.Write(')');
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0002AA17 File Offset: 0x00028C17
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0002AA20 File Offset: 0x00028C20
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0002AA29 File Offset: 0x00028C29
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode(), this.Property.GetHashCode(), this.Name.GetHashCode());
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0002AA5C File Offset: 0x00028C5C
		public override bool Equals(QueryExpression other)
		{
			QueryPropertyVariationSourceExpression queryPropertyVariationSourceExpression = other as QueryPropertyVariationSourceExpression;
			bool? flag = Util.AreEqual<QueryPropertyVariationSourceExpression>(this, queryPropertyVariationSourceExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryPropertyVariationSourceExpression.Expression.Equals(this.Expression) && queryPropertyVariationSourceExpression.Property.Equals(this.Property) && queryPropertyVariationSourceExpression.Name.Equals(this.Name);
		}
	}
}
