using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CE RID: 718
	[DataContract(Name = "ScopedEvalExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryScopedEvalExpression : QueryExpression
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0002AB77 File Offset: 0x00028D77
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0002AB7F File Offset: 0x00028D7F
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0002AB88 File Offset: 0x00028D88
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0002AB90 File Offset: 0x00028D90
		[DataMember(IsRequired = true, Order = 2)]
		public List<QueryExpressionContainer> Scope { get; set; }

		// Token: 0x060017F4 RID: 6132 RVA: 0x0002AB9C File Offset: 0x00028D9C
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("ScopedEval(");
			this.Expression.WriteQueryString(w);
			w.Write(", Scope(");
			for (int i = 0; i < this.Scope.Count; i++)
			{
				if (i > 0)
				{
					w.Write(", ");
				}
				this.Scope[i].WriteQueryString(w);
			}
			w.Write("))");
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0002AC0D File Offset: 0x00028E0D
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0002AC16 File Offset: 0x00028E16
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0002AC1F File Offset: 0x00028E1F
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode(), this.Scope.Count, Hashing.CombineHash<QueryExpressionContainer>(this.Scope, null));
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0002AC54 File Offset: 0x00028E54
		public override bool Equals(QueryExpression other)
		{
			QueryScopedEvalExpression queryScopedEvalExpression = other as QueryScopedEvalExpression;
			bool? flag = Util.AreEqual<QueryScopedEvalExpression>(this, queryScopedEvalExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryScopedEvalExpression.Expression.Equals(this.Expression) && queryScopedEvalExpression.Scope.SequenceEqualReadOnly(this.Scope);
		}
	}
}
