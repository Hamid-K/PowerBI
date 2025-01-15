using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200015D RID: 349
	internal sealed class QueryAddMissingItemsExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0003A59D File Offset: 0x0003879D
		internal QueryAddMissingItemsExpression(ConceptualResultType conceptualResultType, IEnumerable<QueryExpression> showAllColumns, QueryExpressionBinding table, IEnumerable<IAddMissingItemsGroupItem> groups, IEnumerable<QueryExpression> contextTables)
			: base(conceptualResultType)
		{
			this._showAllColumns = showAllColumns.ToReadOnlyCollection<QueryExpression>();
			this._table = table;
			this._groups = groups.ToReadOnlyCollection<IAddMissingItemsGroupItem>();
			this._contextTables = contextTables.ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0003A5D3 File Offset: 0x000387D3
		public ReadOnlyCollection<QueryExpression> ShowAllColumns
		{
			get
			{
				return this._showAllColumns;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0003A5DB File Offset: 0x000387DB
		public QueryExpressionBinding Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0003A5E3 File Offset: 0x000387E3
		public ReadOnlyCollection<IAddMissingItemsGroupItem> Groups
		{
			get
			{
				return this._groups;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0003A5EB File Offset: 0x000387EB
		public ReadOnlyCollection<QueryExpression> ContextTables
		{
			get
			{
				return this._contextTables;
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0003A5F3 File Offset: 0x000387F3
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0003A608 File Offset: 0x00038808
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryAddMissingItemsExpression queryAddMissingItemsExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryAddMissingItemsExpression>(this, other, out flag, out queryAddMissingItemsExpression))
			{
				return flag;
			}
			return this.ShowAllColumns.SequenceEqual(queryAddMissingItemsExpression.ShowAllColumns) && this.Table.Equals(queryAddMissingItemsExpression.Table) && this.Groups.SequenceEqual(queryAddMissingItemsExpression.Groups) && this.ContextTables.SequenceEqual(queryAddMissingItemsExpression.ContextTables);
		}

		// Token: 0x04000AFE RID: 2814
		private readonly ReadOnlyCollection<QueryExpression> _showAllColumns;

		// Token: 0x04000AFF RID: 2815
		private readonly QueryExpressionBinding _table;

		// Token: 0x04000B00 RID: 2816
		private readonly ReadOnlyCollection<IAddMissingItemsGroupItem> _groups;

		// Token: 0x04000B01 RID: 2817
		private readonly ReadOnlyCollection<QueryExpression> _contextTables;
	}
}
