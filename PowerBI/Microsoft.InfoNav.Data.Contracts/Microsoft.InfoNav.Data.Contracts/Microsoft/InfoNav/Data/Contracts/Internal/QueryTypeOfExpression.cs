using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E6 RID: 742
	[DataContract(Name = "TypeOfExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTypeOfExpression : QueryExpression
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0002C3DF File Offset: 0x0002A5DF
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0002C3E7 File Offset: 0x0002A5E7
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x060018B8 RID: 6328 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("typeof(");
			if (this.Expression == null)
			{
				w.WriteError();
			}
			else
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(')');
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0002C427 File Offset: 0x0002A627
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0002C430 File Offset: 0x0002A630
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0002C439 File Offset: 0x0002A639
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0002C458 File Offset: 0x0002A658
		public override bool Equals(QueryExpression other)
		{
			QueryTypeOfExpression queryTypeOfExpression = other as QueryTypeOfExpression;
			bool? flag = Util.AreEqual<QueryTypeOfExpression>(this, queryTypeOfExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryTypeOfExpression.Expression.Equals(this.Expression);
		}
	}
}
