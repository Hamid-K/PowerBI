using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000051 RID: 81
	internal sealed class Int16ArrayTypeInfo : TraceLoggingTypeInfo<short[]>
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public override void WriteData(TraceLoggingDataCollector collector, ref short[] value)
		{
			collector.AddArray(value);
		}
	}
}
