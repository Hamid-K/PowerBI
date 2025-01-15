using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000268 RID: 616
	internal sealed class CalculateTableStrategy : GroupAndJoinTranslationStrategyBase
	{
		// Token: 0x06001AA4 RID: 6820 RVA: 0x0004A074 File Offset: 0x00048274
		internal CalculateTableStrategy(IConceptualModel model, IConceptualSchema schema, IReadOnlyList<QueryTableColumn> groupKeys, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> contextTables, IReadOnlyList<GroupAndJoinMeasure> measures, ExpressionReferenceNameToTableMapping referenceNameMapping, bool allowEmptyGroups, bool useConceptualSchema)
			: base(model, schema)
		{
			this._groupKeys = groupKeys;
			this._contextTables = contextTables ?? Microsoft.DataShaping.Util.EmptyReadOnlyList<global::System.ValueTuple<QueryExpression, bool>>();
			this._measures = measures;
			this._referenceNameMapping = referenceNameMapping;
			this._allowEmptyGroups = allowEmptyGroups;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0004A0C4 File Offset: 0x000482C4
		internal override QueryTable Translate(out bool hasUnconstrainedJoin, out BatchQueryConstraintTelemetry telemetry)
		{
			hasUnconstrainedJoin = false;
			telemetry = null;
			IEnumerable<KeyValuePair<string, QueryExpression>> enumerable = this._groupKeys.Select((QueryTableColumn key) => key.Expression.As(key.Name)).Evaluate<KeyValuePair<string, QueryExpression>>();
			ScanKind scanKind = (this._allowEmptyGroups ? ScanKind.InheritFilterContextIncludeBlankRow : ScanKind.InheritFilterContextExcludeBlankRow);
			QueryGroupByExpression queryGroupByExpression = enumerable.QdmGroupBy(this._useConceptualSchema, base.Schema, scanKind);
			QueryTableDefinition queryTableDefinition = new QueryTableDefinition(this._groupKeys, queryGroupByExpression, "Summarize");
			queryTableDefinition = this.AddMeasures(queryTableDefinition);
			return this.ApplyContextFilters(queryTableDefinition);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0004A14C File Offset: 0x0004834C
		private QueryTableDefinition AddMeasures(QueryTableDefinition input)
		{
			if (this._measures.Count == 0)
			{
				return input;
			}
			IReadOnlyList<QueryTableColumn> columns = input.Columns;
			List<QueryTableColumn> list = new List<QueryTableColumn>(columns.Count + this._measures.Count);
			foreach (QueryTableColumn queryTableColumn in columns)
			{
				list.Add(queryTableColumn.ToReferenceColumn());
			}
			foreach (GroupAndJoinMeasure groupAndJoinMeasure in this._measures)
			{
				list.Add(groupAndJoinMeasure.Column);
			}
			return input.Project(list, ProjectSubsetStrategy.Default);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0004A210 File Offset: 0x00048410
		private QueryTableDefinition ApplyContextFilters(QueryTableDefinition input)
		{
			if (this._contextTables.IsNullOrEmpty<global::System.ValueTuple<QueryExpression, bool>>())
			{
				return input;
			}
			IEnumerable<QueryExpression> enumerable = this._contextTables.Select(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryExpression, bool> pair) => pair.Item1);
			return input.Calculate(enumerable);
		}

		// Token: 0x04000EBF RID: 3775
		private readonly IReadOnlyList<QueryTableColumn> _groupKeys;

		// Token: 0x04000EC0 RID: 3776
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })]
		private readonly IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> _contextTables;

		// Token: 0x04000EC1 RID: 3777
		private readonly IReadOnlyList<GroupAndJoinMeasure> _measures;

		// Token: 0x04000EC2 RID: 3778
		private readonly ExpressionReferenceNameToTableMapping _referenceNameMapping;

		// Token: 0x04000EC3 RID: 3779
		private readonly bool _allowEmptyGroups;

		// Token: 0x04000EC4 RID: 3780
		private readonly bool _useConceptualSchema;
	}
}
