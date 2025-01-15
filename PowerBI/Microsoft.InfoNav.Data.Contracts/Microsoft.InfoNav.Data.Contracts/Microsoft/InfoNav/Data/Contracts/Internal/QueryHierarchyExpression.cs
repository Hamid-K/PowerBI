using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B7 RID: 695
	[DataContract(Name = "HierarchyExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryHierarchyExpression : QueryExpression
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00029647 File Offset: 0x00027847
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0002964F File Offset: 0x0002784F
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00029658 File Offset: 0x00027858
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00029660 File Offset: 0x00027860
		[DataMember(IsRequired = true, Order = 2)]
		public string Hierarchy { get; set; }

		// Token: 0x06001729 RID: 5929 RVA: 0x0002966C File Offset: 0x0002786C
		internal override void WriteQueryString(QueryStringWriter w)
		{
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			else
			{
				w.Write("null");
			}
			w.Write(".hierarchy(");
			w.WriteIdentifierCustomerContent(this.Hierarchy);
			w.Write(')');
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000296BF File Offset: 0x000278BF
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000296C8 File Offset: 0x000278C8
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000296D1 File Offset: 0x000278D1
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.GetHashCode<string>(this.Hierarchy, null));
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000296FC File Offset: 0x000278FC
		public override bool Equals(QueryExpression other)
		{
			QueryHierarchyExpression queryHierarchyExpression = other as QueryHierarchyExpression;
			bool? flag = Util.AreEqual<QueryHierarchyExpression>(this, queryHierarchyExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryHierarchyExpression.Expression == this.Expression && queryHierarchyExpression.Hierarchy == this.Hierarchy;
		}
	}
}
