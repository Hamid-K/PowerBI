using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E5 RID: 741
	[DataContract(Name = "TransformTableRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformTableRefExpression : QueryExpression
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0002C340 File Offset: 0x0002A540
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x0002C348 File Offset: 0x0002A548
		[DataMember(IsRequired = true, Order = 1)]
		public string Source { get; set; }

		// Token: 0x060018B0 RID: 6320 RVA: 0x0002C351 File Offset: 0x0002A551
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write(this.Source);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0002C35F File Offset: 0x0002A55F
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0002C368 File Offset: 0x0002A568
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0002C371 File Offset: 0x0002A571
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), QueryNameComparer.Instance.GetHashCode(this.Source));
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0002C394 File Offset: 0x0002A594
		public override bool Equals(QueryExpression other)
		{
			QueryTransformTableRefExpression queryTransformTableRefExpression = other as QueryTransformTableRefExpression;
			bool? flag = Util.AreEqual<QueryTransformTableRefExpression>(this, queryTransformTableRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(queryTransformTableRefExpression.Source, this.Source);
		}
	}
}
