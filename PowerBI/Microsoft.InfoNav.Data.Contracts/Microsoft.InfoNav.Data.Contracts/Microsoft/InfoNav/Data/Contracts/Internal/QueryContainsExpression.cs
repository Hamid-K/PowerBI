using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000298 RID: 664
	[DataContract(Name = "ContainsExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryContainsExpression : QueryBinaryExpression
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x00023C0F File Offset: 0x00021E0F
		internal override void WriteQueryString(QueryStringWriter w)
		{
			base.Left.WriteQueryString(w);
			w.Write(" contains ");
			base.Right.WriteQueryString(w);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00023C34 File Offset: 0x00021E34
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00023C3D File Offset: 0x00021E3D
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00023C46 File Offset: 0x00021E46
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00023C60 File Offset: 0x00021E60
		public override bool Equals(QueryExpression other)
		{
			QueryContainsExpression queryContainsExpression = other as QueryContainsExpression;
			bool? flag = Util.AreEqual<QueryContainsExpression>(this, queryContainsExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.Equals(other);
		}
	}
}
