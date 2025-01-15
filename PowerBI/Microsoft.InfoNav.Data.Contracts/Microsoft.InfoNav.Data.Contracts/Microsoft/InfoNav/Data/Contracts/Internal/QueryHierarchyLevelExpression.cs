using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B8 RID: 696
	[DataContract(Name = "HierarchyLevelExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryHierarchyLevelExpression : QueryExpression
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00029757 File Offset: 0x00027957
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x0002975F File Offset: 0x0002795F
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00029768 File Offset: 0x00027968
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x00029770 File Offset: 0x00027970
		[DataMember(IsRequired = true, Order = 2)]
		public string Level { get; set; }

		// Token: 0x06001733 RID: 5939 RVA: 0x0002977C File Offset: 0x0002797C
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
			w.Write(".level(");
			w.WriteIdentifierCustomerContent(this.Level);
			w.Write(')');
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000297CF File Offset: 0x000279CF
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000297D8 File Offset: 0x000279D8
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000297E1 File Offset: 0x000279E1
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.GetHashCode<string>(this.Level, null));
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0002980C File Offset: 0x00027A0C
		public override bool Equals(QueryExpression other)
		{
			QueryHierarchyLevelExpression queryHierarchyLevelExpression = other as QueryHierarchyLevelExpression;
			bool? flag = Util.AreEqual<QueryHierarchyLevelExpression>(this, queryHierarchyLevelExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryHierarchyLevelExpression.Expression == this.Expression && queryHierarchyLevelExpression.Level == this.Level;
		}
	}
}
