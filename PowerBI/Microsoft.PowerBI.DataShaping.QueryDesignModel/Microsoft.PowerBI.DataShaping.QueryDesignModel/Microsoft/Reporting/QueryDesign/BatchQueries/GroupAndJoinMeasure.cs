using System;
using System.Globalization;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026A RID: 618
	internal sealed class GroupAndJoinMeasure
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x0004A902 File Offset: 0x00048B02
		internal GroupAndJoinMeasure(QueryTableColumn column, bool suppressJoinPredicate)
		{
			this._column = column;
			this._suppressJoinPredicate = suppressJoinPredicate;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0004A918 File Offset: 0x00048B18
		public QueryTableColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0004A920 File Offset: 0x00048B20
		public bool SuppressJoinPredicate
		{
			get
			{
				return this._suppressJoinPredicate;
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0004A928 File Offset: 0x00048B28
		public bool HasMatchingExpression(QueryExpression expression)
		{
			return this._column.Expression.Equals(expression);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0004A93B File Offset: 0x00048B3B
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}/{1})", this._column.Name, this._suppressJoinPredicate);
		}

		// Token: 0x04000ED1 RID: 3793
		private readonly QueryTableColumn _column;

		// Token: 0x04000ED2 RID: 3794
		private bool _suppressJoinPredicate;
	}
}
