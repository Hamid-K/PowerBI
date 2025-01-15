using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E1 RID: 737
	[DataContract(Name = "TransformOutputRoleRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformOutputRoleRefExpression : QueryExpression
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0002C09D File Offset: 0x0002A29D
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0002C0A5 File Offset: 0x0002A2A5
		[DataMember(IsRequired = true, Order = 1)]
		public string Role { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0002C0AE File Offset: 0x0002A2AE
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x0002C0B6 File Offset: 0x0002A2B6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Transform { get; set; }

		// Token: 0x06001896 RID: 6294 RVA: 0x0002C0BF File Offset: 0x0002A2BF
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteFormat("transformoutputrole('{0}')", new object[] { this.Role });
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0002C0DB File Offset: 0x0002A2DB
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0002C0ED File Offset: 0x0002A2ED
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Role.GetHashCode());
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0002C10C File Offset: 0x0002A30C
		public override bool Equals(QueryExpression other)
		{
			QueryTransformOutputRoleRefExpression queryTransformOutputRoleRefExpression = other as QueryTransformOutputRoleRefExpression;
			bool? flag = Util.AreEqual<QueryTransformOutputRoleRefExpression>(this, queryTransformOutputRoleRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryTransformOutputRoleRefExpression.Role == this.Role;
		}
	}
}
