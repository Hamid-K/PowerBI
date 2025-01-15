using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000272 RID: 626
	internal sealed class QueryTableColumnExpressionComparer : IEqualityComparer<QueryTableColumn>
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x0004C1DF File Offset: 0x0004A3DF
		private QueryTableColumnExpressionComparer()
		{
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0004C1E8 File Offset: 0x0004A3E8
		public bool Equals(QueryTableColumn x, QueryTableColumn y)
		{
			bool? flag = Util.AreEqual<QueryExpression>((x != null) ? x.Expression : null, (y != null) ? y.Expression : null);
			if (flag == null)
			{
				return x.Expression.Equals(y.Expression);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0004C238 File Offset: 0x0004A438
		public int GetHashCode(QueryTableColumn obj)
		{
			int? num;
			if (obj == null)
			{
				num = null;
			}
			else
			{
				QueryExpression expression = obj.Expression;
				num = ((expression != null) ? new int?(expression.GetHashCode()) : null);
			}
			int? num2 = num;
			if (num2 == null)
			{
				return -48879;
			}
			return num2.GetValueOrDefault();
		}

		// Token: 0x04000EE1 RID: 3809
		internal static readonly QueryTableColumnExpressionComparer Instance = new QueryTableColumnExpressionComparer();
	}
}
