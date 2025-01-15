using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200029E RID: 670
	[DataContract(Name = "DefaultValueExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDefaultValueExpression : QueryExpression
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x00024069 File Offset: 0x00022269
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("defaultValue");
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00024076 File Offset: 0x00022276
		public override bool Equals(QueryExpression other)
		{
			return other is QueryDefaultValueExpression;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00024081 File Offset: 0x00022281
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0002408E File Offset: 0x0002228E
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00024097 File Offset: 0x00022297
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000830 RID: 2096
		private const string DefaultValue = "defaultValue";
	}
}
