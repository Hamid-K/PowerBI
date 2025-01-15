using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000068 RID: 104
	internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo<DateTimeOffset>
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000CAB1 File Offset: 0x0000ACB1
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("Ticks", Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
			traceLoggingMetadataCollector.AddScalar("Offset", TraceLoggingDataType.Int64);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CADC File Offset: 0x0000ACDC
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTimeOffset value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
			collector.AddScalar(value.Offset.Ticks);
		}
	}
}
