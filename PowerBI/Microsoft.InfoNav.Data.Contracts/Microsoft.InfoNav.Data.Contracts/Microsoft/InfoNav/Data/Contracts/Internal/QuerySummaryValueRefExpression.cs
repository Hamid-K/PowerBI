using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D9 RID: 729
	[DataContract(Name = "SummaryValueRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QuerySummaryValueRefExpression : QueryExpression
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0002BB36 File Offset: 0x00029D36
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x0002BB3E File Offset: 0x00029D3E
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x06001862 RID: 6242 RVA: 0x0002BB47 File Offset: 0x00029D47
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0002BB50 File Offset: 0x00029D50
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0002BB59 File Offset: 0x00029D59
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), QueryValueComparers.SummaryValueRefComparer.GetHashCode(this.Name));
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0002BB7C File Offset: 0x00029D7C
		public override bool Equals(QueryExpression other)
		{
			QuerySummaryValueRefExpression querySummaryValueRefExpression = other as QuerySummaryValueRefExpression;
			bool? flag = Util.AreEqual<QuerySummaryValueRefExpression>(this, querySummaryValueRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryValueComparers.SummaryValueRefComparer.Equals(this.Name, querySummaryValueRefExpression.Name);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0002BBBF File Offset: 0x00029DBF
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteFormat("summaryValueRef [{0}]", new object[] { this.Name });
		}
	}
}
