using System;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200021B RID: 539
	public sealed class ModelCapabilities
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x00043DE0 File Offset: 0x00041FE0
		internal ModelCapabilities(XElement modelCapabilitiesElem)
			: this(modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.CrossFilteringWithinTableElem, CrossFilteringWithinTableType.None), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.GroupByValidationElem, GroupByValidationType.Relaxed), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.QueryAggregateUsageElem, QueryAggregateUsageType.Encourage), modelCapabilitiesElem.GetBooleanElementOrDefault(Extensions.DiscourageCompositeModelsElem, true), modelCapabilitiesElem.GetBooleanElementOrDefault(Extensions.EncourageIsEmptyDAXFunctionalUsageElem, false), ModelCapabilities.ParseFiveStateKPIRange(modelCapabilitiesElem), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.QueryBatchingElem, QueryBatchingType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.VariablesElem, VariablesType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.InOperatorElem, InOperatorType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.VirtualColumnsElem, VirtualColumnsType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.TableConstructorElem, TableConstructorType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.MultiColumnFiltering, MultiColumnFilteringType.Unrestricted), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.DataSourceVariablesElem, DataSourceVariablesType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.ExecutionMetricsElem, ExecutionMetricsType.NotSupported), modelCapabilitiesElem.GetEnumElementOrDefault(Extensions.VisualCalculationsElem, VisualCalculationType.NotSupported), new DAXFunctions(modelCapabilitiesElem.GetElementOrNull(Extensions.DAXFunctionsElem)))
		{
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00043EB4 File Offset: 0x000420B4
		internal ModelCapabilities(CrossFilteringWithinTableType crossFilteringWithinTable, GroupByValidationType groupByValidation, QueryAggregateUsageType queryAggregateUsage, bool discourageCompositeModels, bool encourageIsEmptyDAXFunctionUsage, FiveStateKPIRangeType fiveStateKPIRange, QueryBatchingType queryBatching, VariablesType variables, InOperatorType inOperator, VirtualColumnsType virtualColumns, TableConstructorType tableConstructor, MultiColumnFilteringType multiColumnFiltering, DataSourceVariablesType dataSourceVariables, ExecutionMetricsType executionMetrics, VisualCalculationType visualCalculations, DAXFunctions daxFunctions)
		{
			this.CrossFilteringWithinTable = crossFilteringWithinTable;
			this.GroupByValidation = groupByValidation;
			this.QueryAggregateUsage = queryAggregateUsage;
			this.DiscourageCompositeModels = discourageCompositeModels;
			this.EncourageIsEmptyDAXFunctionUsage = encourageIsEmptyDAXFunctionUsage;
			this.FiveStateKPIRange = fiveStateKPIRange;
			this.QueryBatching = queryBatching;
			this.Variables = variables;
			this.InOperator = inOperator;
			this.VirtualColumns = virtualColumns;
			this.TableConstructor = tableConstructor;
			this.MultiColumnFiltering = multiColumnFiltering;
			this.DataSourceVariables = dataSourceVariables;
			this.ExecutionMetrics = executionMetrics;
			this.VisualCalculations = visualCalculations;
			this.DaxFunctions = daxFunctions;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00043F44 File Offset: 0x00042144
		private static FiveStateKPIRangeType ParseFiveStateKPIRange(XElement modelCapabilitiesElem)
		{
			if (modelCapabilitiesElem.GetInt32ElementOrDefault(Extensions.FiveStateKPIRange, 0) != 1)
			{
				return FiveStateKPIRangeType.Default;
			}
			return FiveStateKPIRangeType.Normalized;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00043F58 File Offset: 0x00042158
		internal CrossFilteringWithinTableType CrossFilteringWithinTable { get; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00043F60 File Offset: 0x00042160
		internal GroupByValidationType GroupByValidation { get; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00043F68 File Offset: 0x00042168
		internal QueryAggregateUsageType QueryAggregateUsage { get; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00043F70 File Offset: 0x00042170
		internal bool DiscourageCompositeModels { get; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00043F78 File Offset: 0x00042178
		internal bool EncourageIsEmptyDAXFunctionUsage { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00043F80 File Offset: 0x00042180
		internal FiveStateKPIRangeType FiveStateKPIRange { get; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x00043F88 File Offset: 0x00042188
		internal QueryBatchingType QueryBatching { get; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00043F90 File Offset: 0x00042190
		internal VariablesType Variables { get; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00043F98 File Offset: 0x00042198
		internal DAXFunctions DaxFunctions { get; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00043FA0 File Offset: 0x000421A0
		internal InOperatorType InOperator { get; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00043FA8 File Offset: 0x000421A8
		internal VirtualColumnsType VirtualColumns { get; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00043FB0 File Offset: 0x000421B0
		internal TableConstructorType TableConstructor { get; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00043FB8 File Offset: 0x000421B8
		internal MultiColumnFilteringType MultiColumnFiltering { get; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00043FC0 File Offset: 0x000421C0
		internal DataSourceVariablesType DataSourceVariables { get; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00043FC8 File Offset: 0x000421C8
		internal ExecutionMetricsType ExecutionMetrics { get; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00043FD0 File Offset: 0x000421D0
		internal VisualCalculationType VisualCalculations { get; }

		// Token: 0x060018F5 RID: 6389 RVA: 0x00043FD8 File Offset: 0x000421D8
		internal ModelCapabilities CreateCopy(CrossFilteringWithinTableType? crossFilteringWithinTable = null, GroupByValidationType? groupByValidation = null, QueryAggregateUsageType? queryAggregateUsage = null, bool? discourageCompositeModels = null, bool? encourageIsEmptyDAXFunctionUsage = null, FiveStateKPIRangeType? fiveStateKPIRange = null, QueryBatchingType? queryBatching = null, VariablesType? variables = null, InOperatorType? inOperator = null, VirtualColumnsType? virtualColumns = null, TableConstructorType? tableConstructor = null, MultiColumnFilteringType? multiColumnFiltering = null, DataSourceVariablesType? dataSourceVariables = null, ExecutionMetricsType? executionMetrics = null, VisualCalculationType? visualCalculations = null, DAXFunctions daxFunctions = null)
		{
			return new ModelCapabilities(crossFilteringWithinTable ?? this.CrossFilteringWithinTable, groupByValidation ?? this.GroupByValidation, queryAggregateUsage ?? this.QueryAggregateUsage, discourageCompositeModels ?? this.DiscourageCompositeModels, encourageIsEmptyDAXFunctionUsage ?? this.EncourageIsEmptyDAXFunctionUsage, fiveStateKPIRange ?? this.FiveStateKPIRange, queryBatching ?? this.QueryBatching, variables ?? this.Variables, inOperator ?? this.InOperator, virtualColumns ?? this.VirtualColumns, tableConstructor ?? this.TableConstructor, multiColumnFiltering ?? this.MultiColumnFiltering, dataSourceVariables ?? this.DataSourceVariables, executionMetrics ?? this.ExecutionMetrics, visualCalculations ?? this.VisualCalculations, daxFunctions ?? this.DaxFunctions);
		}
	}
}
