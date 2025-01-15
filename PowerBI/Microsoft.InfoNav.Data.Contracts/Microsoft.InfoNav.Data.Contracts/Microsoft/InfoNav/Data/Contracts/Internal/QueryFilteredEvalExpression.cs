using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B4 RID: 692
	[DataContract(Name = "QueryFilteredEvalExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryFilteredEvalExpression : QueryExpression
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x000290DA File Offset: 0x000272DA
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x000290E2 File Offset: 0x000272E2
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000290EB File Offset: 0x000272EB
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x000290F3 File Offset: 0x000272F3
		[DataMember(IsRequired = true, Order = 2)]
		public List<QueryFilter> Filters { get; set; }

		// Token: 0x06001706 RID: 5894 RVA: 0x000290FC File Offset: 0x000272FC
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("FilteredEval(");
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(", (");
			if (this.Filters != null)
			{
				for (int i = 0; i < this.Filters.Count; i++)
				{
					if (i > 0)
					{
						w.Write(", ");
					}
					this.Filters[i].WriteQueryString(w, null);
				}
			}
			w.Write("))");
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00029184 File Offset: 0x00027384
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0002918D File Offset: 0x0002738D
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00029196 File Offset: 0x00027396
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.CombineHash<QueryFilter>(this.Filters, null));
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000291C0 File Offset: 0x000273C0
		public override bool Equals(QueryExpression other)
		{
			bool? flag = Util.AreEqual<QueryExpression>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			QueryFilteredEvalExpression queryFilteredEvalExpression = other as QueryFilteredEvalExpression;
			return queryFilteredEvalExpression != null && queryFilteredEvalExpression.Expression == this.Expression && queryFilteredEvalExpression.Filters.SequenceEqual(this.Filters);
		}
	}
}
