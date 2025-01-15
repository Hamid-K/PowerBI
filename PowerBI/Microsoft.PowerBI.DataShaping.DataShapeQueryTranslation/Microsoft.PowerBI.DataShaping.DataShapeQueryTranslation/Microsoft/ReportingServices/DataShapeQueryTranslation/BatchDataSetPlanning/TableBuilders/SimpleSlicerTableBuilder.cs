using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E0 RID: 480
	internal static class SimpleSlicerTableBuilder
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x00045500 File Offset: 0x00043700
		internal static PlanOperation BuildSlicerTableDeclaration(DataShape dataShape, DataShapeAnnotations annotations, PlanDeclarationCollection declarations, out FilterCondition slicerFilterCondition)
		{
			slicerFilterCondition = null;
			Filter filter = annotations.GetFilter(dataShape, null);
			if (annotations.HasComplexSlicer(dataShape) || filter == null)
			{
				return null;
			}
			slicerFilterCondition = filter.Condition;
			return new PlanOperationCreateFilterContextTable(slicerFilterCondition).DeclareIfNotDeclared(PlanNames.FilterTable(dataShape.Id, null), declarations, true, false, null, false);
		}
	}
}
