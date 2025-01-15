using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200006A RID: 106
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<decimal>
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CB65 File Offset: 0x0000AD65
		public override void WriteData(TraceLoggingDataCollector collector, ref decimal value)
		{
			collector.AddScalar((double)value);
		}
	}
}
