using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000286 RID: 646
	[DataContract(Name = "BinaryExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class QueryBinaryExpression : QueryExpression
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0002310E File Offset: 0x0002130E
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x00023116 File Offset: 0x00021316
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Left { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0002311F File Offset: 0x0002131F
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00023127 File Offset: 0x00021327
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Right { get; set; }

		// Token: 0x06001384 RID: 4996 RVA: 0x00023130 File Offset: 0x00021330
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Left.GetHashCode(), this.Right.GetHashCode());
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00023150 File Offset: 0x00021350
		public override bool Equals(QueryExpression other)
		{
			QueryBinaryExpression queryBinaryExpression = other as QueryBinaryExpression;
			bool? flag = Util.AreEqual<QueryBinaryExpression>(this, queryBinaryExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryBinaryExpression.Left.Equals(this.Left) && queryBinaryExpression.Right.Equals(this.Right);
		}
	}
}
