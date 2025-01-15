using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018A RID: 394
	internal sealed class QueryGroupAndJoinAdditionalColumn
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x0003BACF File Offset: 0x00039CCF
		internal QueryGroupAndJoinAdditionalColumn(string name, QueryExpression expression, bool suppressJoinPredicate)
		{
			this.Name = ArgumentValidation.CheckNotNull<string>(name, "name");
			this.Expression = ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			this.SuppressJoinPredicate = suppressJoinPredicate;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0003BB00 File Offset: 0x00039D00
		public string Name { get; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0003BB08 File Offset: 0x00039D08
		internal QueryExpression Expression { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0003BB10 File Offset: 0x00039D10
		public bool SuppressJoinPredicate { get; }

		// Token: 0x06001538 RID: 5432 RVA: 0x0003BB18 File Offset: 0x00039D18
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			QueryGroupAndJoinAdditionalColumn queryGroupAndJoinAdditionalColumn = obj as QueryGroupAndJoinAdditionalColumn;
			return queryGroupAndJoinAdditionalColumn != null && (this.Name == queryGroupAndJoinAdditionalColumn.Name && this.Expression == queryGroupAndJoinAdditionalColumn.Expression) && this.SuppressJoinPredicate == queryGroupAndJoinAdditionalColumn.SuppressJoinPredicate;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003BB70 File Offset: 0x00039D70
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Name.GetHashCode(), Hashing.CombineHash(this.Expression.GetHashCode(), this.SuppressJoinPredicate.GetHashCode()));
		}
	}
}
