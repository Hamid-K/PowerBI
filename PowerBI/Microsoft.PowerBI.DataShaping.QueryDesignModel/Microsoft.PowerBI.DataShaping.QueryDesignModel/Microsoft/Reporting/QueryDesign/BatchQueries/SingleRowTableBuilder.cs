using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027F RID: 639
	internal sealed class SingleRowTableBuilder
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x0004CDA5 File Offset: 0x0004AFA5
		internal SingleRowTableBuilder(EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, ExpressionReferenceNameToTableMapping referenceNameMapping = null, HashSet<string> usedNames = null)
		{
			this._namingContext = new QueryNamingContext(usedNames);
			this._columns = new List<QueryTableColumn>();
			this._model = model;
			this._schema = schema;
			this._featureSwitchProvider = featureSwitchProvider;
			this._referenceNameMapping = referenceNameMapping;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0004CDE4 File Offset: 0x0004AFE4
		public QueryTableColumn AddOrReuseColumn(QueryExpression expression, string suggestedName)
		{
			QueryTableColumn queryTableColumn = this._columns.SingleOrDefault((QueryTableColumn c) => c.Expression.Equals(expression));
			if (queryTableColumn == null)
			{
				queryTableColumn = new QueryTableColumn(this._namingContext.CreateOrReuseNameForMeasure(expression, null, suggestedName), expression);
				this._columns.Add(queryTableColumn);
			}
			return queryTableColumn;
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0004CE48 File Offset: 0x0004B048
		public void AddContextTable(QueryTable contextTable, bool shouldCrossFilterGroupColumns = false)
		{
			global::System.ValueTuple<QueryTable, bool> valueTuple = new global::System.ValueTuple<QueryTable, bool>(contextTable, shouldCrossFilterGroupColumns);
			Util.AddToLazyList<global::System.ValueTuple<QueryTable, bool>>(ref this._contextTables, valueTuple);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0004CE6A File Offset: 0x0004B06A
		public void AddContextTables([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })] IEnumerable<global::System.ValueTuple<QueryTable, bool>> contextTables)
		{
			Util.AddToLazyList<global::System.ValueTuple<QueryTable, bool>>(ref this._contextTables, contextTables);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0004CE78 File Offset: 0x0004B078
		public void AddExistsFilter(QueryExistsFilter existsFilter)
		{
			Util.AddToLazyList<QueryExistsFilter>(ref this._existsFilters, existsFilter);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0004CE88 File Offset: 0x0004B088
		public QueryTable ToQueryTable()
		{
			if (this.HasAnyContextTables || this.HasAnyExistsFilters)
			{
				bool flag = this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
				GroupAndJoinTableBuilder groupAndJoinTableBuilder = new GroupAndJoinTableBuilder(this._model, this._schema, true, flag, this._referenceNameMapping, JoinPredicateBehavior.AutoPredicates, null);
				foreach (QueryTableColumn queryTableColumn in this._columns)
				{
					groupAndJoinTableBuilder.AddOrReuseColumn(queryTableColumn.Expression, queryTableColumn.Name, true);
				}
				if (this.HasAnyContextTables)
				{
					groupAndJoinTableBuilder.AddContextTables(this._contextTables);
				}
				if (this.HasAnyExistsFilters)
				{
					groupAndJoinTableBuilder.AddExistsFilters(this._existsFilters);
				}
				return groupAndJoinTableBuilder.ToQueryTable();
			}
			return QueryTableBuilder.Row(this._columns);
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0004CF60 File Offset: 0x0004B160
		internal IEnumerable<QueryTableColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0004CF68 File Offset: 0x0004B168
		private bool HasAnyContextTables
		{
			get
			{
				return this._contextTables != null && this._contextTables.Count > 0;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0004CF82 File Offset: 0x0004B182
		private bool HasAnyExistsFilters
		{
			get
			{
				return this._existsFilters != null && this._existsFilters.Count > 0;
			}
		}

		// Token: 0x04000EFB RID: 3835
		private readonly QueryNamingContext _namingContext;

		// Token: 0x04000EFC RID: 3836
		private readonly List<QueryTableColumn> _columns;

		// Token: 0x04000EFD RID: 3837
		private readonly EntityDataModel _model;

		// Token: 0x04000EFE RID: 3838
		private readonly IConceptualSchema _schema;

		// Token: 0x04000EFF RID: 3839
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x04000F00 RID: 3840
		private ExpressionReferenceNameToTableMapping _referenceNameMapping;

		// Token: 0x04000F01 RID: 3841
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })]
		private List<global::System.ValueTuple<QueryTable, bool>> _contextTables;

		// Token: 0x04000F02 RID: 3842
		private List<QueryExistsFilter> _existsFilters;
	}
}
