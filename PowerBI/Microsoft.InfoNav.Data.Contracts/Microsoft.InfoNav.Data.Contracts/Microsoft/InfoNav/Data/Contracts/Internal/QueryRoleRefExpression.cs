using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CD RID: 717
	[DataContract(Name = "RoleRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryRoleRefExpression : QueryExpression
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0002AACA File Offset: 0x00028CCA
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x0002AAD2 File Offset: 0x00028CD2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Role { get; set; }

		// Token: 0x060017EA RID: 6122 RVA: 0x0002AADB File Offset: 0x00028CDB
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0002AAE4 File Offset: 0x00028CE4
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0002AAED File Offset: 0x00028CED
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Role, QueryValueComparers.RoleRefComparer));
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0002AB10 File Offset: 0x00028D10
		public override bool Equals(QueryExpression other)
		{
			QueryRoleRefExpression queryRoleRefExpression = other as QueryRoleRefExpression;
			bool? flag = Util.AreEqual<QueryRoleRefExpression>(this, queryRoleRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryValueComparers.RoleRefComparer.Equals(this.Role, queryRoleRefExpression.Role);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0002AB53 File Offset: 0x00028D53
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteFormat("roleRef[{0}]", new object[] { this.Role });
		}
	}
}
