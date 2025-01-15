using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BF RID: 703
	[DataContract(Name = "MinExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryMinExpression : QueryExpression
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00029F0B File Offset: 0x0002810B
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x00029F13 File Offset: 0x00028113
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00029F1C File Offset: 0x0002811C
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00029F24 File Offset: 0x00028124
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public IncludeAllTypes IncludeAllTypes { get; set; }

		// Token: 0x06001775 RID: 6005 RVA: 0x00029F30 File Offset: 0x00028130
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("min(");
			this.Expression.WriteQueryString(w);
			w.Write(", ");
			w.Write(this.IncludeAllTypes.ToString().ToLowerInvariant());
			w.Write(')');
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00029F88 File Offset: 0x00028188
		public override bool Equals(QueryExpression other)
		{
			QueryMinExpression queryMinExpression = other as QueryMinExpression;
			bool? flag = Util.AreEqual<QueryMinExpression>(this, queryMinExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryMinExpression.IncludeAllTypes.Equals(this.IncludeAllTypes) && queryMinExpression.Expression == this.Expression;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00029FEC File Offset: 0x000281EC
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), this.IncludeAllTypes.GetHashCode());
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0002A01E File Offset: 0x0002821E
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0002A027 File Offset: 0x00028227
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
