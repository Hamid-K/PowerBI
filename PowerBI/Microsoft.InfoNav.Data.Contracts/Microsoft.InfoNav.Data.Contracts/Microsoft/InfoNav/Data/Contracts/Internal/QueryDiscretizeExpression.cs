using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A6 RID: 678
	[DataContract(Name = "DiscretizeExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDiscretizeExpression : QueryExpression
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000262F8 File Offset: 0x000244F8
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00026300 File Offset: 0x00024500
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00026309 File Offset: 0x00024509
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00026311 File Offset: 0x00024511
		[DataMember(IsRequired = true, Order = 2)]
		public int Count { get; set; }

		// Token: 0x060014CD RID: 5325 RVA: 0x0002631C File Offset: 0x0002451C
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("discretize(");
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(", ");
			w.Write((long)this.Count);
			w.Write(')');
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0002636E File Offset: 0x0002456E
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00026377 File Offset: 0x00024577
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00026380 File Offset: 0x00024580
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), this.Count.GetHashCode());
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000263B8 File Offset: 0x000245B8
		public override bool Equals(QueryExpression other)
		{
			QueryDiscretizeExpression queryDiscretizeExpression = other as QueryDiscretizeExpression;
			bool? flag = Util.AreEqual<QueryDiscretizeExpression>(this, queryDiscretizeExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDiscretizeExpression.Expression == this.Expression && queryDiscretizeExpression.Count == this.Count;
		}
	}
}
