using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200019B RID: 411
	internal sealed class QueryIsOnOrAfterArgument
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x0003C2C9 File Offset: 0x0003A4C9
		internal QueryIsOnOrAfterArgument(QueryExpression left, QueryExpression right, SortDirection direction)
		{
			this.Left = left;
			this.Right = right;
			this.Direction = direction;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0003C2E6 File Offset: 0x0003A4E6
		public QueryExpression Left { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0003C2EE File Offset: 0x0003A4EE
		public QueryExpression Right { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0003C2F6 File Offset: 0x0003A4F6
		public SortDirection Direction { get; }

		// Token: 0x0600158B RID: 5515 RVA: 0x0003C300 File Offset: 0x0003A500
		public override bool Equals(object obj)
		{
			QueryIsOnOrAfterArgument queryIsOnOrAfterArgument = obj as QueryIsOnOrAfterArgument;
			return queryIsOnOrAfterArgument != null && (this.Left.Equals(queryIsOnOrAfterArgument.Left) && this.Right.Equals(queryIsOnOrAfterArgument.Right)) && this.Direction == queryIsOnOrAfterArgument.Direction;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0003C350 File Offset: 0x0003A550
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash(this.Left.GetHashCode(), this.Right.GetHashCode()), this.Direction.GetHashCode());
		}
	}
}
