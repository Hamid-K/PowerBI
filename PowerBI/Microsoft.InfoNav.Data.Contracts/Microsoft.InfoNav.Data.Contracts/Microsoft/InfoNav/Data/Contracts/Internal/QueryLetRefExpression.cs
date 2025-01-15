using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BA RID: 698
	[DataContract(Name = "LetRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryLetRefExpression : QueryExpression
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00029B46 File Offset: 0x00027D46
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00029B4E File Offset: 0x00027D4E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string Name { get; set; }

		// Token: 0x0600174B RID: 5963 RVA: 0x00029B57 File Offset: 0x00027D57
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00029B60 File Offset: 0x00027D60
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00029B69 File Offset: 0x00027D69
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Name, QueryNameComparer.Instance));
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00029B8C File Offset: 0x00027D8C
		public override bool Equals(QueryExpression other)
		{
			QueryLetRefExpression queryLetRefExpression = other as QueryLetRefExpression;
			bool? flag = Util.AreEqual<QueryLetRefExpression>(this, queryLetRefExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(this.Name, queryLetRefExpression.Name);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00029BCF File Offset: 0x00027DCF
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("letRef[");
			if (this.Name != null)
			{
				w.Write(this.Name);
			}
			w.Write("]");
		}
	}
}
