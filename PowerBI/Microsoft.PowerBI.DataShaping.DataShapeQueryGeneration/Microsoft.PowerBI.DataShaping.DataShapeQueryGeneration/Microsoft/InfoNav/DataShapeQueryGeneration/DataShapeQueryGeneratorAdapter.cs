using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000045 RID: 69
	internal class DataShapeQueryGeneratorAdapter : IDataShapeGenerator
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A7B1 File Offset: 0x000089B1
		public static DataShapeQueryGeneratorAdapter Instance { get; } = new DataShapeQueryGeneratorAdapter();

		// Token: 0x06000262 RID: 610 RVA: 0x0000A7B8 File Offset: 0x000089B8
		private DataShapeQueryGeneratorAdapter()
			: this(DataShapeQueryGeneratorFactory.Instance)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A7C5 File Offset: 0x000089C5
		public DataShapeQueryGeneratorAdapter(IDataShapeQueryGeneratorFactory dsqGeneratorFactory)
		{
			this._dsqGeneratorFactory = dsqGeneratorFactory;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public DataShapeGenerationResult GenerateDataShapeFromCommand(DataShapeGenerationContext context, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig = null, DataReductionConfiguration dataReductionConfigForLegacyLimits = null)
		{
			return this._dsqGeneratorFactory.CreateDataShapeGenerator(context).GenerateDataShapeFromCommand(context, command, dataReductionConfig, dataReductionConfigForLegacyLimits);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A7EC File Offset: 0x000089EC
		public DataShapeGenerationResult GenerateDataShapeFromQuery(DataShapeGenerationContext context, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationOptions generationOptions)
		{
			return this._dsqGeneratorFactory.CreateDataShapeGenerator(context).GenerateDataShapeFromQuery(context, resolvedQuery, generationOptions);
		}

		// Token: 0x0400012D RID: 301
		private readonly IDataShapeQueryGeneratorFactory _dsqGeneratorFactory;
	}
}
