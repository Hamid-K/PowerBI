using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D8 RID: 728
	[DataContract(Name = "SubqueryExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QuerySubqueryExpression : QueryExpression
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0002BA50 File Offset: 0x00029C50
		// (set) Token: 0x06001859 RID: 6233 RVA: 0x0002BA58 File Offset: 0x00029C58
		[DataMember(IsRequired = true, Order = 1)]
		public QueryDefinition Query { get; set; }

		// Token: 0x0600185A RID: 6234 RVA: 0x0002BA64 File Offset: 0x00029C64
		internal override void WriteQueryString(QueryStringWriter w)
		{
			using (w.NewIndentScope())
			{
				w.Write("{");
				w.WriteLine();
				this.Query.WriteQueryString(w, null);
				w.Write(" }");
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0002BAC0 File Offset: 0x00029CC0
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0002BAC9 File Offset: 0x00029CC9
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0002BAD2 File Offset: 0x00029CD2
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Query.GetHashCode());
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		public override bool Equals(QueryExpression other)
		{
			QuerySubqueryExpression querySubqueryExpression = other as QuerySubqueryExpression;
			bool? flag = Util.AreEqual<QuerySubqueryExpression>(this, querySubqueryExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Query.Equals(querySubqueryExpression.Query);
		}
	}
}
