using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200008E RID: 142
	internal interface IDataShapeGenerator
	{
		// Token: 0x0600058C RID: 1420
		DataShapeGenerationResult GenerateDataShapeFromCommand(DataShapeGenerationContext context, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig = null, DataReductionConfiguration dataReductionConfigForLegacyLimits = null);

		// Token: 0x0600058D RID: 1421
		DataShapeGenerationResult GenerateDataShapeFromQuery(DataShapeGenerationContext context, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationOptions generationOptions);
	}
}
