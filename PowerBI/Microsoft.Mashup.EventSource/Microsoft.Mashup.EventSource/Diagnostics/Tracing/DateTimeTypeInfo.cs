using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000067 RID: 103
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo<DateTime>
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000CA61 File Offset: 0x0000AC61
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTime value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
		}
	}
}
