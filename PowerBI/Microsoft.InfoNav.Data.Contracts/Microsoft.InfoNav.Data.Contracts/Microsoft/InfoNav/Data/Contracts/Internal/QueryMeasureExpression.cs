using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002BD RID: 701
	[DataContract(Name = "MeasureExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryMeasureExpression : QueryPropertyExpression
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x00029DD0 File Offset: 0x00027FD0
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00029DD9 File Offset: 0x00027FD9
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00029DE2 File Offset: 0x00027FE2
		public override bool Equals(QueryExpression other)
		{
			return other is QueryMeasureExpression && base.Equals(other);
		}
	}
}
