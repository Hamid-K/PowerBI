using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200015E RID: 350
	internal static class VisualCalculationReferenceableTableGenerator
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034904 File Offset: 0x00032B04
		internal static GeneratedTable Generate(GeneratedTable input, IReadOnlyList<ColumnWithExplicitName> explicitlyNamedColumns, BatchQueryGenerationNamingContext namingContext)
		{
			return RenameColumnsWithExplictNameGenerator.Generate(EnsureUniqueUnqualifiedNamesGenerator.Generate(input, true), explicitlyNamedColumns, namingContext);
		}
	}
}
