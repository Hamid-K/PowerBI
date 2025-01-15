using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200004B RID: 75
	internal sealed class DoubleTypeInfo : TraceLoggingTypeInfo<double>
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000C56D File Offset: 0x0000A76D
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000C57E File Offset: 0x0000A77E
		public override void WriteData(TraceLoggingDataCollector collector, ref double value)
		{
			collector.AddScalar(value);
		}
	}
}
