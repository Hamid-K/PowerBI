using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200028B RID: 651
	[DataContract(Name = "Null", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNullConstantExpression : QueryConstantExpression
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x0002333B File Offset: 0x0002153B
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteCustomerContent("null");
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00023348 File Offset: 0x00021548
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00023351 File Offset: 0x00021551
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0002335A File Offset: 0x0002155A
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00023368 File Offset: 0x00021568
		public override bool Equals(QueryExpression other)
		{
			QueryNullConstantExpression queryNullConstantExpression = other as QueryNullConstantExpression;
			bool? flag = Util.AreEqual<QueryNullConstantExpression>(this, queryNullConstantExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNullConstantExpression != null;
		}
	}
}
