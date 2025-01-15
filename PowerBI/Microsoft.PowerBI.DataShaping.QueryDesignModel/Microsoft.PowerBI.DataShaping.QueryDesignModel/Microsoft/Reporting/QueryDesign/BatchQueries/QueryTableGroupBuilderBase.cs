using System;
using System.Collections.Generic;
using Microsoft.DataShaping.QueryDesignModel.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000278 RID: 632
	internal abstract class QueryTableGroupBuilderBase : IGroupBuilder
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0004C789 File Offset: 0x0004A989
		protected QueryTableGroupBuilderBase(QueryNamingContext namingContext)
		{
			this.NamingContext = namingContext;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0004C798 File Offset: 0x0004A998
		internal List<QueryExpression> ContextTables
		{
			get
			{
				return this._contextTables;
			}
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0004C7A0 File Offset: 0x0004A9A0
		public void AddContextTableExpression(QueryExpression expression)
		{
			if (!this._contextTables.IsNullOrEmpty<QueryExpression>() && this._contextTables.Contains(expression))
			{
				return;
			}
			Util.AddToLazyList<QueryExpression>(ref this._contextTables, expression);
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001B3E RID: 6974
		public abstract bool IsEmpty { get; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001B3F RID: 6975
		public abstract IEnumerable<QueryTableGroupBuilder> GroupBuilders { get; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001B40 RID: 6976
		public abstract bool HasAnySubtotalColumns { get; }

		// Token: 0x06001B41 RID: 6977
		public abstract IGroupItem ToGroupItem();

		// Token: 0x04000EF0 RID: 3824
		protected readonly QueryNamingContext NamingContext;

		// Token: 0x04000EF1 RID: 3825
		private List<QueryExpression> _contextTables;
	}
}
