using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A7 RID: 679
	[DataContract(Name = "EndsWithExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryEndsWithExpression : QueryBinaryExpression
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x00026410 File Offset: 0x00024610
		internal override void WriteQueryString(QueryStringWriter w)
		{
			base.Left.WriteQueryString(w);
			w.Write(" startsWith ");
			base.Right.WriteQueryString(w);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00026435 File Offset: 0x00024635
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0002643E File Offset: 0x0002463E
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00026447 File Offset: 0x00024647
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00026460 File Offset: 0x00024660
		public override bool Equals(QueryExpression other)
		{
			QueryEndsWithExpression queryEndsWithExpression = other as QueryEndsWithExpression;
			bool? flag = Util.AreEqual<QueryEndsWithExpression>(this, queryEndsWithExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.Equals(other);
		}
	}
}
