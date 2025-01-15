using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C4 RID: 452
	internal static class DataTransformApplier
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x00040BD8 File Offset: 0x0003EDD8
		internal static PlanOperationContext ApplyDataTransforms(PlanOperationContext bodyTable, PlanDeclarationCollection declarations, DataTransformReferenceMap transformReferenceMap, IReadOnlyList<DataTransform> transforms, AggregatesInputTableCollector aggregatesInputTableCollector, bool applyTransformsInQuery, bool generateComposableQueryColumnNames)
		{
			if (transforms.IsNullOrEmpty<DataTransform>() || !applyTransformsInQuery)
			{
				return bodyTable;
			}
			if (generateComposableQueryColumnNames)
			{
				bodyTable = bodyTable.EnsureUniqueUnqualifiedNames(true);
			}
			string text = null;
			foreach (DataTransform dataTransform in transforms)
			{
				bodyTable = bodyTable.DataTransform(dataTransform);
				bodyTable = DataTransformApplier.MapReferringIdentities(bodyTable, dataTransform, transformReferenceMap);
				text = PlanNames.DataTransform(dataTransform.Id);
				bodyTable = bodyTable.DeclareIfNotDeclared(text, declarations, false, null, false);
			}
			aggregatesInputTableCollector.RegisterTransformTable(bodyTable, text);
			return bodyTable;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00040C6C File Offset: 0x0003EE6C
		private static PlanOperationContext MapReferringIdentities(PlanOperationContext input, DataTransform transform, DataTransformReferenceMap mapping)
		{
			List<Calculation> list = new List<Calculation>();
			List<PlanProjectItem> list2 = new List<PlanProjectItem>();
			list2.Add(PlanOperationBuilder.ToAllColumnsProjectItem());
			foreach (DataTransformTableColumn dataTransformTableColumn in transform.Output.Table.Columns)
			{
				DataTransformApplier.AddReferringExpressionProjections(list2, mapping, dataTransformTableColumn);
				DataTransformApplier.AddReferringCalculations(list, mapping, dataTransformTableColumn);
			}
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(input.Totals.ToTotalsMetadata(), false);
			return new PlanOperationContext(input.Table.Project(list2, false), input.RowScopes, list.ToReadOnlyList<Calculation>(), input.ShowAll, planOperationFilteringMetadata);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00040D24 File Offset: 0x0003EF24
		private static void AddReferringExpressionProjections(List<PlanProjectItem> projectItems, DataTransformReferenceMap mapping, DataTransformTableColumn column)
		{
			IReadOnlyList<Expression> referringExpressions = mapping.GetReferringExpressions(column);
			if (referringExpressions.IsNullOrEmpty<Expression>())
			{
				return;
			}
			List<ExpressionId> list = new List<ExpressionId>(referringExpressions.Count);
			foreach (Expression expression in referringExpressions)
			{
				list.Add(expression.ExpressionId.Value);
			}
			PlanTransformExistingColumnWithSameNameProjectItem planTransformExistingColumnWithSameNameProjectItem = column.Value.ToNewColumnFromExistingProjectItemWithSameName(list);
			projectItems.Add(planTransformExistingColumnWithSameNameProjectItem);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		private static void AddReferringCalculations(List<Calculation> calculations, DataTransformReferenceMap mapping, DataTransformTableColumn column)
		{
			IReadOnlyList<Calculation> referringCalculations = mapping.GetReferringCalculations(column);
			if (referringCalculations.IsNullOrEmpty<Calculation>())
			{
				return;
			}
			foreach (Calculation calculation in referringCalculations)
			{
				if (!calculations.Contains(calculation))
				{
					calculations.Add(calculation);
				}
			}
		}
	}
}
