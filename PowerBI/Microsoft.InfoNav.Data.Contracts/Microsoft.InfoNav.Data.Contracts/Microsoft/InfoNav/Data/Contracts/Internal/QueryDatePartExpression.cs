using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200029B RID: 667
	[DataContract(Name = "DatePartExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDatePartExpression : QueryExpression
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00023E13 File Offset: 0x00022013
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x00023E1B File Offset: 0x0002201B
		[DataMember(IsRequired = true, Order = 1)]
		public QueryDatePartFunction Function { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00023E24 File Offset: 0x00022024
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x00023E2C File Offset: 0x0002202C
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x06001412 RID: 5138 RVA: 0x00023E38 File Offset: 0x00022038
		internal override void WriteQueryString(QueryStringWriter w)
		{
			string text = this.Function.ToString().ToLowerInvariant();
			w.Write(text);
			w.Write('(');
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00023E82 File Offset: 0x00022082
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00023E8B File Offset: 0x0002208B
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00023E94 File Offset: 0x00022094
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Function.GetHashCode(), this.Expression.GetHashCode());
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00023ED0 File Offset: 0x000220D0
		public override bool Equals(QueryExpression other)
		{
			QueryDatePartExpression queryDatePartExpression = other as QueryDatePartExpression;
			bool? flag = Util.AreEqual<QueryDatePartExpression>(this, queryDatePartExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryDatePartExpression.Function.Equals(this.Function) && queryDatePartExpression.Expression.Equals(this.Expression);
		}
	}
}
