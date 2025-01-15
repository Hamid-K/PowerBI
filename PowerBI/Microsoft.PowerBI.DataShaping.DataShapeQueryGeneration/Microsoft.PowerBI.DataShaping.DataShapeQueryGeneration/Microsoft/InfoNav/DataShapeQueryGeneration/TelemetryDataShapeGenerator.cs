using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D3 RID: 211
	internal class TelemetryDataShapeGenerator : IDataShapeGenerator
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x0001C38C File Offset: 0x0001A58C
		public TelemetryDataShapeGenerator(ITelemetryService telemetryService, IDataShapeGenerator inner)
		{
			this.telemetryService = telemetryService;
			this.inner = inner;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		public DataShapeGenerationResult GenerateDataShapeFromCommand(DataShapeGenerationContext context, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig = null, DataReductionConfiguration dataReductionConfigForLegacyLimits = null)
		{
			return this.telemetryService.RunInActivity<DataShapeGenerationResult>(ActivityKind.DataShapeQueryGeneration, () => this.inner.GenerateDataShapeFromCommand(context, command, dataReductionConfig, dataReductionConfigForLegacyLimits));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		public DataShapeGenerationResult GenerateDataShapeFromQuery(DataShapeGenerationContext context, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationOptions generationOptions)
		{
			return this.telemetryService.RunInActivity<DataShapeGenerationResult>(ActivityKind.DataShapeQueryGeneration, () => this.inner.GenerateDataShapeFromQuery(context, resolvedQuery, generationOptions));
		}

		// Token: 0x040003E0 RID: 992
		private readonly ITelemetryService telemetryService;

		// Token: 0x040003E1 RID: 993
		private readonly IDataShapeGenerator inner;
	}
}
