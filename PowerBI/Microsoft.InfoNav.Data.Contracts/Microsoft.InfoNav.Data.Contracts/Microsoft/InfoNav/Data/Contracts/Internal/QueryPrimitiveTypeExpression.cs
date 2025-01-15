using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002CA RID: 714
	[DataContract(Name = "PrimitiveTypeExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryPrimitiveTypeExpression : QueryExpression
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0002A7C5 File Offset: 0x000289C5
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x0002A7CD File Offset: 0x000289CD
		[DataMember(IsRequired = true, Order = 1)]
		public ConceptualPrimitiveType Type { get; set; }

		// Token: 0x060017CC RID: 6092 RVA: 0x0002A7D8 File Offset: 0x000289D8
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write(this.Type.ToString());
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0002A7FF File Offset: 0x000289FF
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0002A808 File Offset: 0x00028A08
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0002A814 File Offset: 0x00028A14
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Type.GetHashCode());
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0002A848 File Offset: 0x00028A48
		public override bool Equals(QueryExpression other)
		{
			QueryPrimitiveTypeExpression queryPrimitiveTypeExpression = other as QueryPrimitiveTypeExpression;
			bool? flag = Util.AreEqual<QueryPrimitiveTypeExpression>(this, queryPrimitiveTypeExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryPrimitiveTypeExpression.Type.Equals(this.Type);
		}
	}
}
