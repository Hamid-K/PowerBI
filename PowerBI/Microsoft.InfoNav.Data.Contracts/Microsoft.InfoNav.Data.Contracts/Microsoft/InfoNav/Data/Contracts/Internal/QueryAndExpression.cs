using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200027D RID: 637
	[DataContract(Name = "AndExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryAndExpression : QueryBinaryExpression
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x00022A51 File Offset: 0x00020C51
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('(');
			base.Left.WriteQueryString(w);
			w.Write(" and ");
			base.Right.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00022A86 File Offset: 0x00020C86
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00022A8F File Offset: 0x00020C8F
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00022A98 File Offset: 0x00020C98
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public override bool Equals(QueryExpression other)
		{
			QueryAndExpression queryAndExpression = other as QueryAndExpression;
			bool? flag = Util.AreEqual<QueryAndExpression>(this, queryAndExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.Equals(other);
		}
	}
}
