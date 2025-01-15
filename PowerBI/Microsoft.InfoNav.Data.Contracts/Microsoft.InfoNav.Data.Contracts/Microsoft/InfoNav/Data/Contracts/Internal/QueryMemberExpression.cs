using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BE RID: 702
	[DataContract(Name = "QueryMemberExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryMemberExpression : QueryExpression
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00029DFD File Offset: 0x00027FFD
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00029E05 File Offset: 0x00028005
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00029E0E File Offset: 0x0002800E
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x00029E16 File Offset: 0x00028016
		[DataMember(IsRequired = true, Order = 2)]
		public string Member { get; set; }

		// Token: 0x0600176B RID: 5995 RVA: 0x00029E20 File Offset: 0x00028020
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("Member(");
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(", ");
			w.Write(this.Member);
			w.Write(')');
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00029E71 File Offset: 0x00028071
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00029E7A File Offset: 0x0002807A
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00029E83 File Offset: 0x00028083
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.GetHashCode<string>(this.Member, null));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00029EB0 File Offset: 0x000280B0
		public override bool Equals(QueryExpression other)
		{
			QueryMemberExpression queryMemberExpression = other as QueryMemberExpression;
			bool? flag = Util.AreEqual<QueryMemberExpression>(this, queryMemberExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryMemberExpression.Expression == this.Expression && queryMemberExpression.Member == this.Member;
		}
	}
}
