using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200005A RID: 90
	internal sealed class DoubleArrayTypeInfo : TraceLoggingTypeInfo<double[]>
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		public override void WriteData(TraceLoggingDataCollector collector, ref double[] value)
		{
			collector.AddArray(value);
		}
	}
}
