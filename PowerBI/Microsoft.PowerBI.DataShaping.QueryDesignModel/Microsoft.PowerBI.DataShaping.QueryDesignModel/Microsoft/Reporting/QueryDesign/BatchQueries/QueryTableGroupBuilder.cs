using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027A RID: 634
	internal sealed class QueryTableGroupBuilder : QueryTableGroupBuilderBase, IQueryTableGroupBuilder
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x0004C7CA File Offset: 0x0004A9CA
		internal QueryTableGroupBuilder(QueryNamingContext namingContext, IConceptualSchema schema, bool useConceptualSchema, QueryTableColumn subtotalIndicatorColumn = null)
			: base(namingContext)
		{
			this._subtotalIndicatorColumn = subtotalIndicatorColumn;
			this._keyColumns = new List<QueryTableColumn>();
			this._detailColumns = new List<QueryTableColumn>();
			this._schema = schema;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0004C800 File Offset: 0x0004AA00
		public QueryTableColumn AddGroupKey(QueryExpression key, string suggestedName)
		{
			QueryTableColumn queryTableColumn = this.CreateQueryTableColumn(key, suggestedName);
			this._keyColumns.Add(queryTableColumn);
			return queryTableColumn;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0004C824 File Offset: 0x0004AA24
		public QueryTableColumn AddGroupDetail(QueryExpression detail, string suggestedName)
		{
			QueryTableColumn queryTableColumn = this.CreateQueryTableColumn(detail, suggestedName);
			this._detailColumns.Add(queryTableColumn);
			return queryTableColumn;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0004C847 File Offset: 0x0004AA47
		public void AddContextTable(QueryExpression expression)
		{
			base.AddContextTableExpression(expression);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0004C850 File Offset: 0x0004AA50
		private QueryTableColumn CreateQueryTableColumn(QueryExpression expression, string suggestedName)
		{
			return new QueryTableColumn(this.NamingContext.CreateOrReuseNameForGroupKey(expression, null, suggestedName), expression);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0004C866 File Offset: 0x0004AA66
		public void RemoveDuplicateColumns(HashSet<QueryTableColumn> existingColumns)
		{
			QueryTableGroupBuilder.RemoveDuplicateColumns(this._keyColumns, existingColumns);
			QueryTableGroupBuilder.RemoveDuplicateColumns(this._detailColumns, existingColumns);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0004C880 File Offset: 0x0004AA80
		private static void RemoveDuplicateColumns(List<QueryTableColumn> columns, HashSet<QueryTableColumn> existingColumns)
		{
			QueryTableUtils.RemoveDuplicateKeys<QueryTableColumn>(columns, existingColumns);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0004C88C File Offset: 0x0004AA8C
		public void MoveGroupDetails(IList<QueryTableGroupBuilder> groupBuilders, int endIndex)
		{
			if (this._detailColumns.Count == 0 && base.ContextTables.IsNullOrEmpty<QueryExpression>())
			{
				return;
			}
			if (endIndex < 0)
			{
				throw new InvalidOperationException("A top level group with group details but without group keys was found.");
			}
			for (int i = 0; i < this._detailColumns.Count; i++)
			{
				groupBuilders.FindGroupBuilderForDetail(endIndex, this._detailColumns[i].Expression)._detailColumns.Add(this._detailColumns[i]);
			}
			this._detailColumns.Clear();
			if (!base.ContextTables.IsNullOrEmpty<QueryExpression>())
			{
				throw new InvalidOperationException("A group without group details or group keys still has context tables that cannot be reconciled against other groups.");
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0004C92A File Offset: 0x0004AB2A
		public override bool IsEmpty
		{
			get
			{
				return this._keyColumns.Count == 0 && this._detailColumns.Count == 0 && base.ContextTables.IsNullOrEmpty<QueryExpression>();
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0004C953 File Offset: 0x0004AB53
		public bool HasGroupKeys
		{
			get
			{
				return this._keyColumns.Count > 0;
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0004C964 File Offset: 0x0004AB64
		public bool TryGetExistingColumn(QueryExpression expression, out QueryTableColumn column)
		{
			column = this._keyColumns.Concat(this._detailColumns).FirstOrDefault((QueryTableColumn c) => c.Expression.Equals(expression));
			return column != null;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0004C9A7 File Offset: 0x0004ABA7
		public override IEnumerable<QueryTableGroupBuilder> GroupBuilders
		{
			get
			{
				yield return this;
				yield break;
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0004C9B8 File Offset: 0x0004ABB8
		public override IGroupItem ToGroupItem()
		{
			CompositeKeyGroupItem compositeKeyGroupItem = new CompositeKeyGroupItem(from k in this._keyColumns.Concat(this._detailColumns)
				select k.Expression.As(k.Name));
			if (this._subtotalIndicatorColumn == null)
			{
				Microsoft.DataShaping.Contract.RetailAssert(base.ContextTables.IsNullOrEmpty<QueryExpression>(), "Expected no context tables for the group item when we don't have totals");
				return compositeKeyGroupItem;
			}
			return new NamedRollupGroupItem(compositeKeyGroupItem, this._subtotalIndicatorColumn.Name, base.ContextTables);
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0004CA36 File Offset: 0x0004AC36
		public QueryTableColumn SubtotalIndicatorColumn
		{
			get
			{
				return this._subtotalIndicatorColumn;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0004CA3E File Offset: 0x0004AC3E
		public override bool HasAnySubtotalColumns
		{
			get
			{
				return this.SubtotalIndicatorColumn != null;
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0004CA4C File Offset: 0x0004AC4C
		public bool CanAddGroupDetail(QueryExpression candidateExpr)
		{
			if (!candidateExpr.IsModelFieldReference())
			{
				return true;
			}
			IEnumerable<EntitySet> enumerable = this.FindEntitySetReferences();
			ISet<EntitySet> set = candidateExpr.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All);
			if (this._useConceptualSchema)
			{
				IEnumerable<IConceptualEntity> enumerable2 = this.FindEntityReferences();
				return !candidateExpr.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).Except(enumerable2).Any<IConceptualEntity>();
			}
			return !set.Except(enumerable).Any<EntitySet>();
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0004CAA6 File Offset: 0x0004ACA6
		private IEnumerable<EntitySet> FindEntitySetReferences()
		{
			return this._keyColumns.SelectMany((QueryTableColumn k) => k.Expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)).Distinct<EntitySet>();
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0004CAD7 File Offset: 0x0004ACD7
		private IEnumerable<IConceptualEntity> FindEntityReferences()
		{
			return this._keyColumns.SelectMany((QueryTableColumn k) => k.Expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)).Distinct<IConceptualEntity>();
		}

		// Token: 0x04000EF2 RID: 3826
		private readonly QueryTableColumn _subtotalIndicatorColumn;

		// Token: 0x04000EF3 RID: 3827
		private readonly List<QueryTableColumn> _keyColumns;

		// Token: 0x04000EF4 RID: 3828
		private readonly List<QueryTableColumn> _detailColumns;

		// Token: 0x04000EF5 RID: 3829
		private readonly IConceptualSchema _schema;

		// Token: 0x04000EF6 RID: 3830
		private readonly bool _useConceptualSchema;
	}
}
