using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000053 RID: 83
	internal sealed class Int32ArrayTypeInfo : TraceLoggingTypeInfo<int[]>
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C700 File Offset: 0x0000A900
		public override void WriteData(TraceLoggingDataCollector collector, ref int[] value)
		{
			collector.AddArray(value);
		}
	}
}
