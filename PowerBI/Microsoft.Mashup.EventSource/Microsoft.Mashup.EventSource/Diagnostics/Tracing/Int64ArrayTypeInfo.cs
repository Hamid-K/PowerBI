using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000055 RID: 85
	internal sealed class Int64ArrayTypeInfo : TraceLoggingTypeInfo<long[]>
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000C734 File Offset: 0x0000A934
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C745 File Offset: 0x0000A945
		public override void WriteData(TraceLoggingDataCollector collector, ref long[] value)
		{
			collector.AddArray(value);
		}
	}
}
