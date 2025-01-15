using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C6 RID: 710
	[DataContract(Name = "OrExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryOrExpression : QueryBinaryExpression
	{
		// Token: 0x060017A8 RID: 6056 RVA: 0x0002A454 File Offset: 0x00028654
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('(');
			base.Left.WriteQueryString(w);
			w.Write(" or ");
			base.Right.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0002A489 File Offset: 0x00028689
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0002A492 File Offset: 0x00028692
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0002A49B File Offset: 0x0002869B
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0002A4B4 File Offset: 0x000286B4
		public override bool Equals(QueryExpression other)
		{
			QueryOrExpression queryOrExpression = other as QueryOrExpression;
			bool? flag = Util.AreEqual<QueryOrExpression>(this, queryOrExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.Equals(other);
		}
	}
}
