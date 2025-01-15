using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000069 RID: 105
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000CB2D File Offset: 0x0000AD2D
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CB3E File Offset: 0x0000AD3E
		public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
		{
			collector.AddScalar(value.Ticks);
		}
	}
}
