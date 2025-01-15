using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BC RID: 700
	[DataContract(Name = "MaxExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryMaxExpression : QueryExpression
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00029CA5 File Offset: 0x00027EA5
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00029CAD File Offset: 0x00027EAD
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00029CB6 File Offset: 0x00027EB6
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x00029CBE File Offset: 0x00027EBE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public IncludeAllTypes IncludeAllTypes { get; set; }

		// Token: 0x0600175D RID: 5981 RVA: 0x00029CC8 File Offset: 0x00027EC8
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("max(");
			this.Expression.WriteQueryString(w);
			w.Write(", ");
			w.Write(this.IncludeAllTypes.ToString().ToLowerInvariant());
			w.Write(')');
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00029D20 File Offset: 0x00027F20
		public override bool Equals(QueryExpression other)
		{
			QueryMaxExpression queryMaxExpression = other as QueryMaxExpression;
			bool? flag = Util.AreEqual<QueryMaxExpression>(this, queryMaxExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryMaxExpression.IncludeAllTypes.Equals(this.IncludeAllTypes) && queryMaxExpression.Expression == this.Expression;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00029D84 File Offset: 0x00027F84
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), this.IncludeAllTypes.GetHashCode());
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00029DB6 File Offset: 0x00027FB6
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00029DBF File Offset: 0x00027FBF
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
