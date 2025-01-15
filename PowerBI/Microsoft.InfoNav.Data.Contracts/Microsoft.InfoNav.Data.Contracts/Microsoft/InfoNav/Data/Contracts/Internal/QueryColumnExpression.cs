using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000287 RID: 647
	[DataContract(Name = "ColumnExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryColumnExpression : QueryPropertyExpression
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x000231AB File Offset: 0x000213AB
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000231B4 File Offset: 0x000213B4
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000231BD File Offset: 0x000213BD
		public override bool Equals(QueryExpression other)
		{
			return other is QueryColumnExpression && base.Equals(other);
		}
	}
}
