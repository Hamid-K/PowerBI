using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027C RID: 636
	internal sealed class QueryTableRollupBuilder : QueryTableGroupBuilderBase, IQueryTableRollupBuilder
	{
		// Token: 0x06001B5B RID: 7003 RVA: 0x0004CB08 File Offset: 0x0004AD08
		internal QueryTableRollupBuilder(QueryNamingContext namingContext, IConceptualSchema schema, bool useConceptualSchema)
			: base(namingContext)
		{
			this._rollupGroupBuilders = new List<QueryTableGroupBuilder>();
			this._schema = schema;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0004CB2C File Offset: 0x0004AD2C
		public IQueryTableGroupBuilder AddRollupGroup(string subtotalIndicatorColumnName)
		{
			subtotalIndicatorColumnName = this.NamingContext.CreateAndRegisterUniqueName(subtotalIndicatorColumnName);
			QueryTableColumn queryTableColumn = BatchQdmExpressionBuilder.CreateBooleanIndicatorColumn(subtotalIndicatorColumnName);
			QueryTableGroupBuilder queryTableGroupBuilder = new QueryTableGroupBuilder(this.NamingContext, this._schema, this._useConceptualSchema, queryTableColumn);
			this._rollupGroupBuilders.Add(queryTableGroupBuilder);
			return queryTableGroupBuilder;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0004CB74 File Offset: 0x0004AD74
		public void AddContextTable(QueryExpression expression)
		{
			base.AddContextTableExpression(expression);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0004CB7D File Offset: 0x0004AD7D
		public void RemoveEmptyRollupGroups()
		{
			this._rollupGroupBuilders.RemoveDuplicatedKeysAndEmptyGroups(delegate(QueryTableGroupBuilder rollupGroupBuilder)
			{
				if (rollupGroupBuilder.IsEmpty && rollupGroupBuilder.SubtotalIndicatorColumn != null)
				{
					throw new InvalidOperationException(StringUtil.FormatInvariant("Cannot remove empty rollup groups with subtotal column '{0}' since the expected subtotals for that level will be missing.", new object[] { rollupGroupBuilder.SubtotalIndicatorColumn.Name }));
				}
			});
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0004CBA9 File Offset: 0x0004ADA9
		public override bool IsEmpty
		{
			get
			{
				return this._rollupGroupBuilders.Count == 0 && base.ContextTables.IsNullOrEmpty<QueryExpression>();
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0004CBC5 File Offset: 0x0004ADC5
		public override IEnumerable<QueryTableGroupBuilder> GroupBuilders
		{
			get
			{
				return this._rollupGroupBuilders;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0004CBCD File Offset: 0x0004ADCD
		public override bool HasAnySubtotalColumns
		{
			get
			{
				return this._rollupGroupBuilders.Any((QueryTableGroupBuilder gb) => gb.HasAnySubtotalColumns);
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0004CBF9 File Offset: 0x0004ADF9
		public override IGroupItem ToGroupItem()
		{
			return new RollupAddIsSubtotalGroupItem(this._rollupGroupBuilders.Select((QueryTableGroupBuilder gb) => (NamedRollupGroupItem)gb.ToGroupItem()).ToList<NamedRollupGroupItem>(), base.ContextTables);
		}

		// Token: 0x04000EF7 RID: 3831
		private readonly List<QueryTableGroupBuilder> _rollupGroupBuilders;

		// Token: 0x04000EF8 RID: 3832
		private readonly IConceptualSchema _schema;

		// Token: 0x04000EF9 RID: 3833
		private readonly bool _useConceptualSchema;
	}
}
