using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C5 RID: 709
	[DataContract(Name = "NowExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNowExpression : QueryExpression
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x0002A3EA File Offset: 0x000285EA
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("now()");
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0002A3F7 File Offset: 0x000285F7
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0002A400 File Offset: 0x00028600
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0002A409 File Offset: 0x00028609
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0002A418 File Offset: 0x00028618
		public override bool Equals(QueryExpression other)
		{
			QueryNowExpression queryNowExpression = other as QueryNowExpression;
			bool? flag = Util.AreEqual<QueryNowExpression>(this, queryNowExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNowExpression != null;
		}

		// Token: 0x0400086F RID: 2159
		private const string Now = "now()";
	}
}
