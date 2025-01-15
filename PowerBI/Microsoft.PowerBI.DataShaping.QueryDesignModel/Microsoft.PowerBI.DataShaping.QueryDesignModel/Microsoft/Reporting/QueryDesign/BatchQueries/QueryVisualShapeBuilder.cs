using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000274 RID: 628
	internal sealed class QueryVisualShapeBuilder
	{
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0004C538 File Offset: 0x0004A738
		public QueryTableColumn IsDensifiedColumn
		{
			get
			{
				return this._isDensifiedColumn;
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0004C540 File Offset: 0x0004A740
		public QueryTableColumn SetIsDensifiedColumnName(string name)
		{
			this._isDensifiedColumn = BatchQdmExpressionBuilder.CreateBooleanIndicatorColumn(name);
			return this._isDensifiedColumn;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0004C554 File Offset: 0x0004A754
		public QueryVisualAxisBuilder AddAxis(QueryVisualAxisName name)
		{
			QueryVisualAxisBuilder queryVisualAxisBuilder = new QueryVisualAxisBuilder(name);
			Util.AddToLazyList<QueryVisualAxisBuilder>(ref this._axisBuilders, queryVisualAxisBuilder);
			return queryVisualAxisBuilder;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0004C575 File Offset: 0x0004A775
		public QueryVisualShape ToVisualShape(Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			IReadOnlyList<QueryVisualAxis> readOnlyList = this.BuildAxes(rewriteExpression);
			QueryTableColumn isDensifiedColumn = this._isDensifiedColumn;
			return new QueryVisualShape(readOnlyList, (isDensifiedColumn != null) ? isDensifiedColumn.Name : null);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0004C598 File Offset: 0x0004A798
		private IReadOnlyList<QueryVisualAxis> BuildAxes(Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			if (this._axisBuilders == null)
			{
				return Util.EmptyReadOnlyCollection<QueryVisualAxis>();
			}
			return this._axisBuilders.CreateList((QueryVisualAxisBuilder b) => b.ToVisualAxis(rewriteExpression));
		}

		// Token: 0x04000EE7 RID: 3815
		private List<QueryVisualAxisBuilder> _axisBuilders;

		// Token: 0x04000EE8 RID: 3816
		private QueryTableColumn _isDensifiedColumn;
	}
}
