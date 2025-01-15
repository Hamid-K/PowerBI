using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D5 RID: 725
	[DataContract(Name = "StartsWithExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryStartsWithExpression : QueryBinaryExpression
	{
		// Token: 0x0600182F RID: 6191 RVA: 0x0002B2F8 File Offset: 0x000294F8
		internal override void WriteQueryString(QueryStringWriter w)
		{
			base.Left.WriteQueryString(w);
			w.Write(" startsWith ");
			base.Right.WriteQueryString(w);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0002B31D File Offset: 0x0002951D
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0002B326 File Offset: 0x00029526
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0002B32F File Offset: 0x0002952F
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0002B348 File Offset: 0x00029548
		public override bool Equals(QueryExpression other)
		{
			QueryStartsWithExpression queryStartsWithExpression = other as QueryStartsWithExpression;
			bool? flag = Util.AreEqual<QueryStartsWithExpression>(this, queryStartsWithExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.Equals(other);
		}
	}
}
