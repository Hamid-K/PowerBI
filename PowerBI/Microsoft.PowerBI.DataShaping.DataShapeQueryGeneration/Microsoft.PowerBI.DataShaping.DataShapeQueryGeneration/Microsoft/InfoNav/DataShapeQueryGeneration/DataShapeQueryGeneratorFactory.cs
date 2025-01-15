using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000046 RID: 70
	internal class DataShapeQueryGeneratorFactory : IDataShapeQueryGeneratorFactory
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A80E File Offset: 0x00008A0E
		public static DataShapeQueryGeneratorFactory Instance { get; } = new DataShapeQueryGeneratorFactory();

		// Token: 0x06000268 RID: 616 RVA: 0x0000A815 File Offset: 0x00008A15
		private DataShapeQueryGeneratorFactory()
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A820 File Offset: 0x00008A20
		public IDataShapeGenerator CreateDataShapeGenerator(DataShapeGenerationContext context)
		{
			DataShapeGenerationErrorContext dataShapeGenerationErrorContext = new DataShapeGenerationErrorContext(context.Tracer);
			return new TelemetryDataShapeGenerator(context.TelemetryService, new ExceptionHandlingDataShapeGenerator(context.Tracer, context.Dumper, dataShapeGenerationErrorContext, new DsqGenerator(dataShapeGenerationErrorContext)));
		}
	}
}
