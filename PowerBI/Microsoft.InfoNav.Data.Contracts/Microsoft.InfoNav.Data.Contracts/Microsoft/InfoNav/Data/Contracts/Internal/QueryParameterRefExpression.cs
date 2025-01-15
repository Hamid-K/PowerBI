using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C7 RID: 711
	[DataContract(Name = "ParameterRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryParameterRefExpression : QueryExpression
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0002A4F0 File Offset: 0x000286F0
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x0002A4F8 File Offset: 0x000286F8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Name { get; set; }

		// Token: 0x060017B0 RID: 6064 RVA: 0x0002A501 File Offset: 0x00028701
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0002A50A File Offset: 0x0002870A
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0002A513 File Offset: 0x00028713
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Name, QueryNameComparer.Instance));
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0002A538 File Offset: 0x00028738
		public override bool Equals(QueryExpression other)
		{
			QueryParameterRefExpression queryParameterRefExpression = other as QueryParameterRefExpression;
			bool? flag = Util.AreEqual<QueryParameterRefExpression>(this, queryParameterRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(this.Name, queryParameterRefExpression.Name);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0002A57B File Offset: 0x0002877B
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("parameterRef[");
			if (this.Name != null)
			{
				w.Write(this.Name);
			}
			w.Write("]");
		}
	}
}
